using System;
using System.Linq;
using System.Windows.Forms;

namespace App
{
    public partial class Add : Form
    {
        public Add()
        {
            InitializeComponent();
        }

        private void Add_Load(object sender, EventArgs e)
        {
            LoadParticipation();
        }

        private void btnAddParticipant_Click(object sender, EventArgs e)
        {
            using (EventsContext db = new EventsContext())
            {
                Participation newParticipant = new Participation { Name = textParticipant.Text };
                db.Participation.Add(newParticipant);
                db.SaveChanges();
                LoadParticipation();
                MessageBox.Show("Пользователь добавлен");

            }
        }
        private void LoadParticipation()
        {
            using (EventsContext db = new EventsContext())
            {
                dataGridParticipant.DataSource = db.Participation.ToList();
            }
        }

        private void btnAddEvent_Click(object sender, EventArgs e)
        {
            using (EventsContext db = new EventsContext())
            {
                Events newEvent = new Events() { Title = textTitle.Text, Description = textDescription.Text, Date = textDate.Text, Time = textTime.Text, Category = textCategory.Text };
                db.Events.Add(newEvent);
                db.SaveChanges();
                MessageBox.Show("Событие добавлено");
            }
        }
    }
}
