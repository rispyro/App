using System;
using System.Linq;
using System.Windows.Forms;

namespace App
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            //using (EventsContext db = new EventsContext())
            //{
            //    db.Database.Delete();
            //}
        }

        private void buttonList_Click(object sender, EventArgs e)
        {
            Participants listForm = new Participants();
            listForm.Show();
        }

        private void buttonRedaction_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridEvents.CurrentRow.Cells[0].Value;
            Redaction redaction = new Redaction(id, this);
            redaction.Show();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Add add = new Add(this);
            add.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadEvents();
        }
        public void LoadEvents()
        {
            using (EventsContext db = new EventsContext())
            {
                dataGridEvents.DataSource = db.Events.ToList();
                dataGridEvents.Columns["EventID"].Visible = false;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridEvents.CurrentRow != null)
            {
                int id = (int)dataGridEvents.CurrentRow.Cells[0].Value;
                DeleteEvent(id);                
                MessageBox.Show("Событие удалено");
                LoadEvents();
            }
            else if (dataGridEvents.Rows.Count == 0)
            {
                MessageBox.Show("Сначала добавьте события");
            }
            else
            {
                MessageBox.Show("Выберите событие");
            }
        }

        public void DeleteEvent(int id)
        {
            using (EventsContext db = new EventsContext())
            {
                Events events = db.Events.Find(id);
                if (events != null)
                {
                    db.Events.Remove(events);
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("События не существует!");
                }
            }
        }
    }
}
