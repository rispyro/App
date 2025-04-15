using System;
using System.Linq;
using System.Windows.Forms;

namespace App
{
    public partial class Redaction : Form
    {
        public int ID;
        public Main Main;
        public Redaction(int id, Main main)
        {
            InitializeComponent();
            ID = id;
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
                dataGridParticipant.DataSource = db.Participation.ToList();
                db.SaveChanges();
            }
        }

        private void btnUpdateEvent_Click(object sender, EventArgs e)
        {
            try
            {
                Events updateEvent = new Events() { Title = textTitle.Text, Description = textDescription.Text, Date = textDate.Text, Time = textTime.Text, Category = textCategory.Text };
                UpdateEvent(updateEvent, ID);

                Main.LoadEvents();
                MessageBox.Show("Событие отредактировано");
            }
            catch
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }            
        }
        public void UpdateEvent(Events updateEvent, int id)
        {
            if (string.IsNullOrWhiteSpace(textTitle.Text) || string.IsNullOrWhiteSpace(textDescription.Text) || string.IsNullOrWhiteSpace(textDate.Text) || string.IsNullOrWhiteSpace(textTime.Text) || string.IsNullOrWhiteSpace(textCategory.Text))
            {
                throw new ArgumentException("Не все поля леса!");
            }
            using (EventsContext db = new EventsContext())
            {
                db.Events.Remove(db.Events.Find(id));
                db.Events.Add(updateEvent);
                db.SaveChanges();
                
            }
        }
    }
}
