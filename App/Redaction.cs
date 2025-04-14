using System;
using System.Windows.Forms;

namespace App
{
    public partial class Redaction : Form
    {
        public int ID;
        public Main Main;
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
                db.Events.Remove(events);
                db.SaveChanges();
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
                db.Events.Add(updateEvent);
                db.SaveChanges();
                MessageBox.Show("Событие отредактировано");
            }
            Main.LoadEvents();
        }
    }
}
