using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace App
{
    public partial class Redaction : Form
    {
        public int ID;
        public Main Main;
        private List<Participation> participations = new List<Participation>();

        public Redaction(int id, Main main)
        {
            ID = id;
            InitializeComponent();
            Main = main;
        }

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

        private void btnUpdateEvent_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textTitle.Text) || string.IsNullOrWhiteSpace(textDescription.Text) || string.IsNullOrWhiteSpace(textDate.Text) || string.IsNullOrWhiteSpace(textTime.Text) || string.IsNullOrWhiteSpace(textCategory.Text))
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (EventsContext db = new EventsContext())
            {
                Events updateEvent = new Events() { Title = textTitle.Text, Description = textDescription.Text, Date = textDate.Text, Time = textTime.Text, Category = textCategory.Text };
                db.Events.Remove(db.Events.Find(ID));
                db.Events.Add(updateEvent);

                db.SaveChanges();
            }
            MessageBox.Show("Информация о событии обновлена");
            Main.LoadEvents();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           
        }

        private void btnAddPaticipant_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textParticipant.Text))
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (EventsContext db = new EventsContext())
            {
                Participation participation = new Participation() { Name = textParticipant.Text, EventId=ID };
                participations.Add(participation);
                db.SaveChanges();
                LoadParticipation();
                Main.LoadEvents();
            }
        }
        public void LoadParticipation()
        {
            dataGridViewParticipant.DataSource = participations.ToList();
            dataGridViewParticipant.Columns["ParticipationID"].Visible = false;
            dataGridViewParticipant.Columns["EventID"].Visible = false;
        }
    }
}
