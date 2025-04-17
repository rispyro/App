using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace App
{
    public partial class Redaction : Form
    {
        /// <summary>
        /// ID редактируемого события
        /// </summary>
        public Guid ID;

        /// <summary>
        /// Ссылка на главную форму для обновления данных
        /// </summary>
        public Main Main;

        /// <summary>
        /// Список участников, привязанных к событию
        /// </summary>
        private List<Participation> participations = new List<Participation>();

        /// <summary>
        /// Конструктор формы редактирования события
        /// </summary>
        /// <param name="id">ID события</param>
        /// <param name="main">Ссылка на главную форму</param>
        public Redaction(Guid id, Main main)
        {
            InitializeComponent();
            ID = id;
            Main = main;
        }

        /// <summary>
        /// Загрузка данных о событии и его участниках при открытии формы
        /// </summary>
        private void Redaction_Load(object sender, EventArgs e)
        {
            using (var db = new EventsContext())
            {
                var events = db.Events.Find(ID);
                if (events == null)
                {
                    MessageBox.Show("Событие не найдено");
                    return;
                }
                if (!DateTime.TryParse(events.Date, out DateTime date))
                {
                    MessageBox.Show("Дата некорректна");
                }
                textTitle.Text = events.Title;
                textDescription.Text = events.Description;
                dateTimePicker1.Value = date;
                textTime.Text = events.Time;
                textCategory.Text = events.Category;

                var allParticipants = db.Participation.ToList();

                foreach (Participation p in allParticipants)
                {
                    if (p.EventId == ID)
                    {
                        participations.Add(p);
                    }
                }
                
                LoadParticipation();
            }
        }

        /// <summary>
        /// Обработчик кнопки "Обновить событие"
        /// </summary>
        private void btnUpdateEvent_Click(object sender, EventArgs e)
        {
            try
            {
                Events updateEvent = new Events()
                {
                    Title = textTitle.Text,
                    Description = textDescription.Text,
                    Date =  dateTimePicker1.Value.ToShortDateString(),
                    Time = textTime.Text,
                    Category = textCategory.Text
                };

                UpdateEvent(updateEvent, ID);
                Main.LoadEvents();
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show(ae.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Обновляет информацию о событии в базе данных
        /// </summary>
        /// <param name="updateEvent">Обновлённое событие</param>
        /// <param name="id">ID события</param>
        public void UpdateEvent(Events updateEvent, Guid id)
        {
            if (string.IsNullOrWhiteSpace(textTitle.Text) ||
                string.IsNullOrWhiteSpace(textDescription.Text) ||
                string.IsNullOrWhiteSpace(textTime.Text) ||
                string.IsNullOrWhiteSpace(textCategory.Text))
            {
                MessageBox.Show("Событие не найдено");
            }

            using (var db = new EventsContext())
            {
                db.Events.Remove(db.Events.Find(id));
                db.Events.Add(updateEvent);
                db.SaveChanges();
            }

            MessageBox.Show("Информация о событии обновлена");
            Main.LoadEvents();
        }

        /// <summary>
        /// Обработчик кнопки удаления
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewParticipant.CurrentRow == null)
            {
                MessageBox.Show("Выберите участника для удаления");
                return;
            }
            int index = dataGridViewParticipant.CurrentRow.Index;
            participations.RemoveAt(index);
            LoadParticipation();
            MessageBox.Show("Участник удалён");

        }

        /// <summary>
        /// Обработчик кнопки добавления
        /// </summary>
        private void btnAddPaticipant_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textParticipant.Text))
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = new EventsContext())
            {
                var participation = new Participation()
                {
                    Name = textParticipant.Text,
                    EventId = ID
                };

                participations.Add(participation);
                db.SaveChanges();
                LoadParticipation();
                Main.LoadEvents();
            }
        }

        /// <summary>
        /// Загружает участников события в таблицу
        /// </summary>
        public void LoadParticipation()
        {
            dataGridViewParticipant.Rows.Clear();
            foreach (var participant in participations)
            {
                dataGridViewParticipant.Rows.Add( participant.EventId, participant.Name, participant.ParticipationId);
            }
        }
    }
}
