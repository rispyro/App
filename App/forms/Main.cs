using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using App.classes.sorting;

namespace App
{
    public partial class Main : Form
    {
        private string currentSort = "";
        /// <summary>
        /// Конструктор главной формы
        /// </summary>
        public Main()
        {
            InitializeComponent();
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            radioButton3.CheckedChanged += radioButton3_CheckedChanged;
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
                List<Events> eventList = db.Events.ToList();
                if (currentSort == "Title")
                {
                    eventList.Sort(new SortingByTitle());
                }
                else if (currentSort == "Date")
                {
                    eventList.Sort(new SortingByDate());
                }
                else if (currentSort == "Category")
                {
                    eventList.Sort(new SortingByCategory());
                }
                foreach (var events in eventList)
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
        /// <summary>
        /// Обработчик кнопки сортировки "По порядку"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                currentSort = "Title";
                LoadEvents();
            }
        }
        /// <summary>
        /// Обработчик кнопки сортировки "По дате"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                currentSort = "Date";
                LoadEvents();
            }
        }
        /// <summary>
        /// Обработчик кнопки сортировки "По категории"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            { 
                currentSort = "Category";
                LoadEvents();
            }
        }

        private void dataGridEvents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
