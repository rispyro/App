using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using App.classes.sorting;

namespace App
{
    public partial class Main : Form
    {
        private string currentSort = "Title";
        /// <summary>
        /// Конструктор главной формы
        /// </summary>
        public Main()
        {
            InitializeComponent();
            radioButtonName.CheckedChanged += radioButtonName_CheckedChanged;
            radioButtonDate.CheckedChanged += radioButtonDate_CheckedChanged;
            radioButtonCategory.CheckedChanged += radioButtonCategory_CheckedChanged;
        }

        /// <summary>
        /// Открывает форму со списком участников для выбранного события
        /// </summary>
        private void buttonList_Click(object sender, EventArgs e)
        {
            if (dataGridEvents.Rows.Count == 0)
            {
                MessageBox.Show("Необходимо добавить событие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (dataGridEvents.CurrentRow != null)
            {
                Guid id = (Guid)dataGridEvents.CurrentRow.Cells[0].Value;
                Participants listForm = new Participants(id);
                listForm.Show();
            }
            else
            { MessageBox.Show("Необходимо выбрать событие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        
        /// <summary>
        /// Открывает форму редактирования для выбранного события
        /// </summary>
        private void buttonRedaction_Click(object sender, EventArgs e)
        {
            if (dataGridEvents.Rows.Count == 0)
            {
                MessageBox.Show("Необходимо добавить события", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (dataGridEvents.CurrentRow != null)
            {
                Guid id = (Guid)dataGridEvents.CurrentRow.Cells[0].Value;
                Redaction redaction = new Redaction(id, this);
                redaction.Show();
            }
            else
            { MessageBox.Show("Необходимо выбрать событие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
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
            if (dataGridEvents.Rows.Count == 0)
            {
                MessageBox.Show("Необходимо добавить событие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (dataGridEvents.CurrentRow != null)
            {
                Guid id = (Guid)dataGridEvents.CurrentRow.Cells[0].Value;
                DeleteEvent(id);
                MessageBox.Show("Событие удалено", "Удачно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadEvents();
            }
            else
            {
                MessageBox.Show("Выберите событие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                var events = db.Events.Find(id);
                if (events != null)
                {
                    db.Events.Remove(events);
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("События не существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        /// <summary>
        /// Обработчик кнопки сортировки "По порядку"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonName_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonName.Checked)
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
        private void radioButtonDate_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDate.Checked)
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
        private void radioButtonCategory_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCategory.Checked)
            { 
                currentSort = "Category";
                LoadEvents();
            }
        }
        /// <summary>
        /// Обработчик кнопки сохранения отчёта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonReport_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text Files | *.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            { 
                return;
            }    

            List<string> list = new List<string>();
            using (var db = new EventsContext())
            {
                foreach (DataGridViewRow row in this.dataGridEvents.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        Guid id = (Guid)row.Cells[0].Value;
                        Events ev = db.Events.Find(id);
                        string strEv = $"{ev.Title} | {ev.Description} | {ev.Date} | {ev.Time} | {ev.Category}";
                        var parts = new List<string>();
                        foreach (var part in db.Participation)
                        {
                            if (part.EventId == id)
                            {
                                parts.Add(part.Name);
                            }
                        }
                        strEv += " | " + string.Join(",", parts);
                        list.Add(strEv);
                    }
                }
            }
            File.AppendAllLines(saveFileDialog1.FileName, list);
            MessageBox.Show("Отчет сохранен!", "Удачно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
