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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Participants listForm = new Participants();
            listForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Redaction redaction = new Redaction();
            redaction.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add add = new Add();
            add.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }
        private void LoadUsers()
        {
            using (var db = new EventsContext())
            {
                dataGridView1.DataSource = db.Events.ToList();
            }
        }
    }
}
