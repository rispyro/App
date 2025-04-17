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

            //using (EventsContext db = new EventsContext())
            //{
            //    db.Database.Delete();
            //}
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

                MessageBox.Show("Пользователь добавлен");
            }
            catch (ArgumentException ae)
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
                throw new ArgumentException("Не все поля заполнены!");
            }

            participations.Add(participant);
            LoadParticipation();
        }

        /// <summary>
        /// Загружает текущих участников в таблицу
        /// </summary>
        private void LoadParticipation()
        {
            dataGridParticipant.Rows.Clear();
            foreach (var participant in participations)
            {
                dataGridParticipant.Rows.Add(participant.EventId,participant.Name, participant.ParticipationId);
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

                AddNewEvent(newEvent);
                Main.LoadEvents();
                MessageBox.Show("Событие добавлено");
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
        public void AddNewEvent(Events newEvent)
        {
            if (string.IsNullOrWhiteSpace(newEvent.Title) || string.IsNullOrWhiteSpace(newEvent.Description) || string.IsNullOrWhiteSpace(newEvent.Time) ||
                string.IsNullOrWhiteSpace(newEvent.Category))
            {
                MessageBox.Show("Не все поля заполнены!");
            }

            using (var db = new EventsContext())
            {
                db.Events.Add(newEvent);

                foreach (var participant in participations)
                {
                    participant.EventId = newEvent.EventId;
                    db.Participation.Add(participant);
                }

                db.SaveChanges();
            }
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
    }
}
