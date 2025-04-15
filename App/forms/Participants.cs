using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace App
{
    public partial class Participants : Form
    {
        public int ID;
        private List<Participation> participations = new List<Participation>();
        public Participants(int id)
        {
            ID= id;
            InitializeComponent();
        }

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
        public void LoadParticipation()
        {
            dataGridViewParticipant.DataSource = participations.ToList();
            dataGridViewParticipant.Columns["ParticipationID"].Visible = false;
            dataGridViewParticipant.Columns["EventID"].Visible = false;
        }
    }
}
