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
                    MessageBox.Show("Событие не найдено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!DateTime.TryParse(events.Date, out DateTime date))
                {
                    MessageBox.Show("Дата некорректна", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                textTitle.Text = events.Title;
                textDescription.Text = events.Description;
                dateTimePicker1.Value = date;
                textTime.Text = events.Time;
                textCategory.Text = events.Category;

                var allParticipants = db.Participation.ToList();

                foreach (var p in allParticipants)
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
                var updateEvent = new Events()
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
            catch (Exception ae)
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
                MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!CorrectTime(updateEvent.Time))
            {
                MessageBox.Show("Некорректный формат времени. Нобходимо использовать формат ЧЧ:ММ (например, 14:30).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var db = new EventsContext())
            {
                if (ExistingEvent(updateEvent))
                {
                    MessageBox.Show("Такое событие уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                db.Events.Remove(db.Events.Find(id));
                db.Events.Add(updateEvent);
                db.SaveChanges();
            }

            MessageBox.Show("Информация о событии обновлена", "Удачно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Main.LoadEvents();
            this.Close();
        }

        /// <summary>
        /// Обработчик кнопки удаления
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewParticipant.CurrentRow == null)
            {
                MessageBox.Show("Выберите участника для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            int index = dataGridViewParticipant.CurrentRow.Index;
            participations.RemoveAt(index);
            LoadParticipation();
            MessageBox.Show("Участник удалён", "Удачно!", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    EventId = ID,
                    ParticipationId = Guid.NewGuid()
                };
                db.Participation.Add(participation);
                db.SaveChanges();
                participations.Add(participation);
                LoadParticipation();
                Main.LoadEvents();
                MessageBox.Show("Участник добавлен", "Удачно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// <summary>
        /// Проверка на корректность времени
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool CorrectTime(string time)
        {
            if (DateTime.TryParse(time, out DateTime dt) && time == dt.ToShortTimeString())
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Проверка на существующее событие
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        private bool ExistingEvent(Events events)
        {
            using (var db = new EventsContext())
            {
                foreach (var existingEvent in db.Events)
                {
                    if (existingEvent.Title == events.Title && existingEvent.Date == events.Date && existingEvent.Time == events.Time && existingEvent.Category == events.Category && existingEvent.Description == events.Description)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
    }
}
