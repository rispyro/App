using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace App
{
    public partial class Add : Form
    {
        public Main Main;
        private List<Participation> participations = new List<Participation>();
        public Add(Main main)
        {
            InitializeComponent();
            Main = main;

            //using (EventsContext db = new EventsContext())
            //{
            //    db.Database.Delete();
            //}
        }

        private void Add_Load(object sender, EventArgs e)
        {
            LoadParticipation();
        }

        private void btnAddParticipant_Click(object sender, EventArgs e)
        {
            try
            {                
                Participation newParticipant = new Participation { Name = textParticipant.Text };
                AddNewParticipation(newParticipant);
                
                MessageBox.Show("Пользователь добавлен");
            }
            catch
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
        public void AddNewParticipation(Participation participant)
        {
            if (string.IsNullOrEmpty(participant.Name))
            {
                throw new ArgumentException("Не все поля заполнены!");
            }

            participations.Add(participant);
            LoadParticipation();
        }
        private void LoadParticipation()
        {
            dataGridParticipant.DataSource = participations.ToList();
            dataGridParticipant.Columns["ParticipationID"].Visible = false;
            dataGridParticipant.Columns["EventID"].Visible = false;
        }

        private void btnAddEvent_Click(object sender, EventArgs e)
        {
            try
            {
                Events newEvent = new Events() { Title = textTitle.Text, Description = textDescription.Text, Date = textDate.Text, Time = textTime.Text, Category = textCategory.Text };
                AddNewEvent(newEvent);
                Main.LoadEvents();
                MessageBox.Show("Событие добавлено");
            }
            catch
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void AddNewEvent(Events newEvent)
        {
            if (string.IsNullOrWhiteSpace(newEvent.Title) || string.IsNullOrWhiteSpace(newEvent.Description) || string.IsNullOrWhiteSpace(newEvent.Date) || string.IsNullOrWhiteSpace(newEvent.Time) || string.IsNullOrWhiteSpace(newEvent.Category))
            {
                throw new ArgumentException("Не все поля поля!");
            }
            using (EventsContext db = new EventsContext())
            {
                db.Events.Add(newEvent);
                db.SaveChanges();                

                foreach (var participant in participations)
                {
                    participant.EventId = newEvent.EventId;
                    db.Participation.Add(participant);
                }
                db.SaveChanges();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridParticipant.CurrentRow != null)
            {
                int id = (int)dataGridParticipant.CurrentRow.Cells[0].Value;
                DeletePart(id);
                LoadParticipation();
                MessageBox.Show("Участник удалён");
            }
            else
            {
                MessageBox.Show("Выберите участника");
            }
        }

        public void DeletePart(int id)
        {
            using (EventsContext db = new EventsContext())
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
