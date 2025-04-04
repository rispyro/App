using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            LoadUsers();
        }

        private void btnAddParticipant_Click(object sender, EventArgs e)
        {
            using (EventsContext db = new EventsContext())
            {
                
                Participation newParticipant = new Participation{Name = textParticipant.Text};
                
                db.Participation.Add(newParticipant);
                LoadUsers();
                db.SaveChanges();
                MessageBox.Show("Пользователь добавлен");

            }
        }
        private void LoadUsers()
        {
            using (var db = new EventsContext())
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
                LoadUsers();
                db.SaveChanges();
                MessageBox.Show("Пользователь добавлен");
            }
        }
    }
}
