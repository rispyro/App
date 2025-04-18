using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace App
{
    public partial class Add : Form
    {
        /// <summary>
        /// Ссылка на главное окно приложения
        /// </summary>
        public Main Main;

        /// <summary>
        /// Список участников текущего события
        /// </summary>
        private List<Participation> participations = new List<Participation>();

        /// <summary>
        /// Конструктор формы добавления, принимает главное окно
        /// </summary>
        /// <param name="main">Главная форма</param>
        public Add(Main main)
        {
            InitializeComponent();
            Main = main;
        }

        /// <summary>
        /// Обработчик загрузки формы
        /// </summary>
        private void Add_Load(object sender, EventArgs e)
        {
            LoadParticipation();
        }

        /// <summary>
        /// Обработчик кнопки добавления участника, добавляет участника в БД
        /// </summary>
        private void btnAddParticipant_Click(object sender, EventArgs e)
        {
            try
            {
                Participation newParticipant = new Participation { Name = textParticipant.Text };
                AddNewParticipation(newParticipant);

            }
            catch (Exception ae)
            {
                MessageBox.Show(ae.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Добавляет нового участника в список
        /// </summary>
        /// <param name="participant">Объект участника</param>
        public void AddNewParticipation(Participation participant)
        {
            if (string.IsNullOrEmpty(participant.Name))
            {
                throw new Exception("Не все поля заполнены!");
            }
            foreach (Participation existingParticipant in participations)
            {
                if (existingParticipant.Name == participant.Name)
                {
                    MessageBox.Show("Участник уже добавлен в это событие!");
                    return;
                }
            }
            participant.ParticipationId = Guid.NewGuid();
            participations.Add(participant);
            LoadParticipation();
            MessageBox.Show("Пользователь добавлен");
        }

        /// <summary>
        /// Загружает текущих участников в таблицу
        /// </summary>
        private void LoadParticipation()
        {
            dataGridParticipant.Rows.Clear();
            foreach (var participant in participations)
            {
                dataGridParticipant.Rows.Add(participant.EventId, participant.Name, participant.ParticipationId);
            }
        }

        /// <summary>
        /// Обработчик кнопки добавления события
        /// </summary>
        private void btnAddEvent_Click(object sender, EventArgs e)
        {
            try
            {
                Events newEvent = new Events()
                {
                    Title = textTitle.Text,
                    Description = textDescription.Text,
                    Date = dateTimePicker1.Value.ToShortDateString(),
                    Time = textTime.Text,
                    Category = textCategory.Text
                };

                if (AddNewEvent(newEvent))
                {
                    Main.LoadEvents();
                    MessageBox.Show("Событие добавлено");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        /// <summary>
        /// Добавляет новое событие и сохраняет участников, связанных с ним
        /// </summary>
        /// <param name="newEvent">Объект события</param>
        public bool AddNewEvent(Events newEvent)
        {
            if (string.IsNullOrWhiteSpace(newEvent.Title) || string.IsNullOrWhiteSpace(newEvent.Description) || string.IsNullOrWhiteSpace(newEvent.Time) ||
                string.IsNullOrWhiteSpace(newEvent.Category))
            {
                MessageBox.Show("Не все поля заполнены!");
                return false;
            }
            if (!CorrectTime(newEvent.Time))
            {
                MessageBox.Show("Некорректный формат времени. Нобходимо использовать формат ЧЧ:ММ (например, 14:30).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            using (var db = new EventsContext())
            {
                if (ExistingEvent(newEvent))
                {
                    MessageBox.Show("Такое событие уже существует!");
                    return false;
                }
                db.Events.Add(newEvent);
                db.SaveChanges();
                foreach (var participant in participations)
                {
                    if (ExistingParticipant(participant, newEvent.EventId))
                    {
                        MessageBox.Show("Участик уже участвует в этом событии!");
                        return false;
                    }
                    participant.EventId = newEvent.EventId;
                    db.Participation.Add(participant);
                }
                db.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Обработчик кнопки удаления участника
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridParticipant.CurrentRow == null)
            {
                MessageBox.Show("Выберите участника для удаления");
                return;
            }

            int index = dataGridParticipant.CurrentRow.Index;
            participations.RemoveAt(index);
            LoadParticipation();
            MessageBox.Show("Участник удалён");

        }

        /// <summary>
        /// Удаляет участника из БД по его ID
        /// </summary>
        /// <param name="id">Идентификатор участника</param>
        public void DeletePart(Guid id)
        {
            using (var db = new EventsContext())
            {
                Participation participant = db.Participation.Find(id);
                if (participant != null)
                {
                    db.Participation.Remove(participant);
                    db.SaveChanges();
                }
            }
        }
        /// <summary>
        /// Метод проверяет корректность времени
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
                    if (existingEvent.Title == events.Title && existingEvent.Date == events.Date && existingEvent.Time == events.Time && existingEvent.Category == events.Category)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Проверка на существующего участника
        /// </summary>
        /// <param name="participant"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        private bool ExistingParticipant(Participation participant, Guid eventId)
        {
            using (var db = new EventsContext())
            {
                var participationInEvent = db.Participation.ToList();
                foreach (var existingPartisipant in participationInEvent)
                {
                    if (existingPartisipant.EventId == eventId && existingPartisipant.Name == participant.Name)
                    {  return true; }
                }
            }
            return false;
        }
    }
}
