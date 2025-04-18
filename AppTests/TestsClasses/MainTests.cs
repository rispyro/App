using Microsoft.VisualStudio.TestTools.UnitTesting;
using App;
using System;
using System.Windows.Forms;

namespace AppTests
{
    [TestClass]
    public class MainTests
    {
        /// <summary>
        /// Тест: удаление события
        /// </summary>
        [TestMethod]
        public void Main_DeleteEvent()
        {
            Main main = new Main();
            Add add = new Add(main);

            Events events = new Events()
            {
                EventId = Guid.NewGuid(),
                Title = "ы2",
                Description = "ы",
                Date = "ы",
                Time = "12:30",
                Category = "ы"
            };

            add.AddNewEvent(events);
            main.LoadEvents();

            Guid id = (Guid)main.dataGridEvents.Rows[main.dataGridEvents.Rows.Count - 2].Cells[0].Value;
            main.DeleteEvent(id);

            bool curr = true;
            using (EventsContext db = new EventsContext())
            {
                if (db.Events.Find(id) == null)
                {
                    curr = !curr;
                }
            }

            Assert.AreEqual(false, curr);
        }
        /// <summary>
        /// Тест: Неудаление несуществующего события
        /// </summary>

        [TestMethod]
        public void Main_DeleteNonExistEvent()
        {
            Main main = new Main();
            int RowsCount = main.dataGridEvents.Rows.Count;

            main.DeleteEvent(Guid.NewGuid());

            Assert.AreEqual(RowsCount, main.dataGridEvents.RowCount);
        }
        /// <summary>
        /// Тест: добавление события
        /// </summary>

        [TestMethod]
        public void Main_AddEvent()
        {
            Main main = new Main();
            Add add = new Add(main);
            Events events = new Events()
            {
                EventId = Guid.NewGuid(),
                Title = "ы1",
                Description = "ы",
                Date = "ы",
                Time = "12:30",
                Category = "ы"
            };

            add.AddNewEvent(events);
            main.LoadEvents();

            int rowId = main.dataGridEvents.Rows.Count-2;

            DataGridViewRow row = main.dataGridEvents.Rows[rowId];

            bool ans = false;

            if (events.EventId == (Guid)row.Cells[0].Value && events.Title == row.Cells[1].Value.ToString() && events.Date == (string)row.Cells[2].Value &&
                events.Date == row.Cells[3].Value.ToString() && events.Time == row.Cells[4].Value.ToString() && events.Category == row.Cells[5].Value.ToString())
            {
                ans = !ans;
            }

            Assert.AreEqual(false, ans);
        }
    }
}