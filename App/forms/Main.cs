using System;
using System.Linq;
using System.Windows.Forms;

namespace App
{
    public partial class Main : Form
    {
        /// <summary>
        /// Конструктор главной формы
        /// </summary>
        public Main()
        {
            InitializeComponent();

            //using (EventsContext db = new EventsContext())
            //{
            //    db.Database.Delete();
            //}
        }

        /// <summary>
        /// Открывает форму со списком участников для выбранного события
        /// </summary>
        private void buttonList_Click(object sender, EventArgs e)
        {
            if (dataGridEvents.Rows.Count <= 1)
            {
                MessageBox.Show("Необходимо добавить событие");
            }
            else if (dataGridEvents.CurrentRow != null)
            {
                Guid id = (Guid)dataGridEvents.CurrentRow.Cells[0].Value;
                Participants listForm = new Participants(id);
                listForm.Show();
            }
            else
            { MessageBox.Show("Необходимо выбрать событие"); }
        }
        
        /// <summary>
        /// Открывает форму редактирования для выбранного события
        /// </summary>
        private void buttonRedaction_Click(object sender, EventArgs e)
        {
            if (dataGridEvents.Rows.Count <= 1)
            {
                MessageBox.Show("Необходимо добавить события");
            }
            else if (dataGridEvents.CurrentRow != null)
            {
                Guid id = (Guid)dataGridEvents.CurrentRow.Cells[0].Value;
                Redaction redaction = new Redaction(id, this);
                redaction.Show();
            }
            else
            { MessageBox.Show("Необходимо выбрать событие"); }
        }

        /// <summary>
        /// Открывает форму добавления нового события
        /// </summary>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Add add = new Add(this);
            add.Show();
        }

        /// <summary>
        /// Обработчик загрузки главной формы, загружает события из БД
        /// </summary>
        private void Main_Load(object sender, EventArgs e)
        {
            LoadEvents();
        }

        /// <summary>
        /// Загружает список событий из БД и отображает их в таблице
        /// </summary>
        public void LoadEvents()
        {
            using (var db = new EventsContext())
            {
                dataGridEvents.Rows.Clear();
                foreach (var events in db.Events)
                {
                    dataGridEvents.Rows.Add(events.EventId, events.Title, events.Date, events.Time, events.Category, events.Description);
                }
            }
        }

        /// <summary>
        /// Обработчик кнопки удаления, удаляет выбранное событие
        /// </summary>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridEvents.Rows.Count <= 1)
            {
                MessageBox.Show("Необходимо добавить событие");
            }
            else if (dataGridEvents.CurrentRow != null)
            {
                Guid id = (Guid)dataGridEvents.CurrentRow.Cells[0].Value;
                DeleteEvent(id);
                MessageBox.Show("Событие удалено");
                LoadEvents();
            }
            else
            {
                MessageBox.Show("Выберите событие");
            }
        }

        /// <summary>
        /// Удаляет событие из БД по ID
        /// </summary>
        /// <param name="id">Идентификатор события</param>
        public void DeleteEvent(Guid id)
        {
            using (var db = new EventsContext())
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
        // сотрировки ( не доделаны)
        //private void radioButton1_CheckedChanged(object sender, EventArgs e)
        //{
        //    this.dataGridEvents.Columns["EventId"].SortMode = DataGridViewColumnSortMode.Automatic;
        //    LoadEvents();
        //}

        //private void radioButton2_CheckedChanged(object sender, EventArgs e)
        //{
        //    this.dataGridEvents.Columns["Date"].SortMode = DataGridViewColumnSortMode.Automatic;
        //    LoadEvents();
        //}

        //private void radioButton3_CheckedChanged(object sender, EventArgs e)
        //{
        //    this.dataGridEvents.Columns["Category"].SortMode = DataGridViewColumnSortMode.Automatic;
        //    LoadEvents();
        //}
    }
}
