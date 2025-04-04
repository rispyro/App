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
            Add add = new Add();
            add.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadEvents();
        }
        private void LoadEvents()
        {
            using (EventsContext db = new EventsContext())
            {
                dataGridView1.DataSource = db.Events.ToList();
            }
        }
    }
}
