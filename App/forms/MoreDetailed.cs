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
        public int ID;

        /// <summary>
        /// Список участников, связанных с выбранным событием
        /// </summary>
        private List<Participation> participations = new List<Participation>();

        /// <summary>
        /// Конструктор формы участников, принимает ID события
        /// </summary>
        /// <param name="id">Идентификатор события</param>
        public Participants(int id)
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
            using (EventsContext db = new EventsContext())
            {
                events = db.Events.Find(ID);
                textDescription.Text = events.Description;
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
        /// Загружает список участников текущего события в таблицу.
        /// </summary>
        public void LoadParticipation()
        {
            dataGridViewParticipant.DataSource = participations.ToList();
            dataGridViewParticipant.Columns["ParticipationID"].Visible = false;
            dataGridViewParticipant.Columns["EventID"].Visible = false;
        }
    }
}
