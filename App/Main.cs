﻿using System;
using System.Linq;
using System.Windows.Forms;

namespace App
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            

        }

        private void buttonList_Click(object sender, EventArgs e)
        {
            Participants listForm = new Participants();
            listForm.Show();
        }

        private void buttonRedaction_Click(object sender, EventArgs e)
        {
            Redaction redaction = new Redaction();
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
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridEvents.CurrentRow != null)
            {
                int id = (int)dataGridEvents.CurrentRow.Cells[0].Value;

                using (EventsContext db = new EventsContext())
                {
                    Events events = db.Events.Find(id);
                    if (events != null)
                    {
                        db.Events.Remove(events);
                        db.SaveChanges();
                        MessageBox.Show("Событие удалено");
                    }
                }
                LoadEvents();
            }
            else
            {
                MessageBox.Show("Выберите событие");
            }
        }

    }
}
