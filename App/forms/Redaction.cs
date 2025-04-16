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
        public int ID;

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
        public Redaction(int id, Main main)
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
            Events events = new Events();
            using (EventsContext db = new EventsContext())
            {
                events = db.Events.Find(ID);

                textTitle.Text = events.Title;
                textDescription.Text = events.Description;
                textDate.Text = events.Date;
                textTime.Text = events.Time;
                textCategory.Text = events.Category;

                dataGridViewParticipant.DataSource = db.Participation.ToList();
                db.SaveChanges();

                foreach (Participation participant in db.Participation.ToList())
                {
                    if (participant.EventId == ID)
                    {
                        participations.Add(participant);
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
                    Date = textDate.Text,
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
        public void UpdateEvent(Events updateEvent, int id)
        {
            if (string.IsNullOrWhiteSpace(textTitle.Text) ||
                string.IsNullOrWhiteSpace(textDescription.Text) ||
                string.IsNullOrWhiteSpace(textDate.Text) ||
                string.IsNullOrWhiteSpace(textTime.Text) ||
                string.IsNullOrWhiteSpace(textCategory.Text))
            {
                throw new ArgumentException("Не все поля леса!");
            }

            using (EventsContext db = new EventsContext())
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
            if (index >= 0 && index < participations.Count)
            {
                participations.RemoveAt(index);
                LoadParticipation();
                MessageBox.Show("Участник удалён");
            }
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

            using (EventsContext db = new EventsContext())
            {
                Participation participation = new Participation()
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
            dataGridViewParticipant.DataSource = participations.ToList();
            dataGridViewParticipant.Columns["ParticipationID"].Visible = false;
            dataGridViewParticipant.Columns["EventID"].Visible = false;
        }
    }
}
