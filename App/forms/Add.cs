using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace App
{
    public partial class Add : Form
    {
        private Main Main;
        private List<Participation> participations = new List<Participation>();
        public Add(Main main)
        {
            InitializeComponent();
            Main = main;
        }

        private void Add_Load(object sender, EventArgs e)
        {
            LoadParticipation();
        }

        private void btnAddParticipant_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textParticipant.Text))
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (EventsContext db = new EventsContext())
            {
                Participation newParticipant = new Participation { Name = textParticipant.Text };
                participations.Add(newParticipant);
                db.SaveChanges();
                LoadParticipation();
                MessageBox.Show("Пользователь добавлен");

            }
        }
        private void LoadParticipation()
        {
            dataGridParticipant.DataSource = participations.ToList();
            dataGridParticipant.Columns["ParticipationID"].Visible = false;
            dataGridParticipant.Columns["EventID"].Visible = false;
        }

        private void btnAddEvent_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textTitle.Text) || string.IsNullOrWhiteSpace(textDescription.Text) || string.IsNullOrWhiteSpace(textDate.Text) ||string.IsNullOrWhiteSpace(textTime.Text) ||string.IsNullOrWhiteSpace(textCategory.Text))
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (EventsContext db = new EventsContext())
            {
                Events newEvent = new Events() { Title = textTitle.Text, Description = textDescription.Text, Date = textDate.Text, Time = textTime.Text, Category = textCategory.Text };

                db.Events.Add(newEvent);
                db.SaveChanges();
                MessageBox.Show("Событие добавлено");


                foreach (var participant in participations)
                {
                    participant.EventId = newEvent.EventId;
                    db.Participation.Add(participant);
                }

                db.SaveChanges();
            }
            Main.LoadEvents();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridParticipant.CurrentRow != null)
            {
                int id = (int)dataGridParticipant.CurrentRow.Cells[0].Value;

                using (EventsContext db = new EventsContext())
                {
                    Participation participant = db.Participation.Find(id);
                    if (participant != null)
                    {
                        db.Participation.Remove(participant);
                        db.SaveChanges();
                        MessageBox.Show("Участник удалён");
                    }
                }
                LoadParticipation();
            }
            else
            {
                MessageBox.Show("Выберите участника");
            }
        }
    }
}
