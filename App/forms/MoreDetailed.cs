using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace App
{
    public partial class Participants : Form
    {
        /// <summary>
        /// Идентификатор события, участники которого отображаются
        /// </summary>
        public Guid ID;

        /// <summary>
        /// Список участников, связанных с выбранным событием
        /// </summary>
        private List<Participation> participations = new List<Participation>();

        /// <summary>
        /// Конструктор формы участников, принимает ID события
        /// </summary>
        /// <param name="id">Идентификатор события</param>
        public Participants(Guid id)
        {
            ID = id;
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик загрузки формы, загружает событие и его участников
        /// </summary>
        private void Participants_Load(object sender, EventArgs e)
        {

            Events events = new Events();
            using (var db = new EventsContext())
            {
                events = db.Events.Find(ID);
                if (events == null)
                {
                    MessageBox.Show("Событие не найдено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                textDescription.Text = events.Description;

                foreach (var participant in db.Participation.ToList())
                {
                    if (participant.EventId == ID)
                    {
                        participations.Add(participant);
                    }
                }
                
            }
            LoadParticipation();
        }

        /// <summary>
        /// Загружает список участников текущего события в таблицу.
        /// </summary>
        public void LoadParticipation()
        {
            dataGridViewParticipant.Rows.Clear();
            foreach (var participant in participations)
            {
                dataGridViewParticipant.Rows.Add(participant.EventId, participant.Name, participant.ParticipationId);
            }
        }
    }
}
