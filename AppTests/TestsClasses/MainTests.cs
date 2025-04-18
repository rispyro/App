using Microsoft.VisualStudio.TestTools.UnitTesting;
using App;
using System;
using System.Windows.Forms;

namespace AppTests
{
    [TestClass]
    public class MainTests
    {
        [TestMethod]
        public void Main_DeleteEvent()
        {
            Main main = new Main();
            Add add = new Add(main);

            Events events = new Events()
            {
                EventId = Guid.NewGuid(),
                Title = "ы",
                Description = "ы",
                Date = "ы",
                Time = "12:30",
                Category = "ы"
            };

            int RowsCount = main.dataGridEvents.Rows.Count;

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

        [TestMethod]
        public void Main_Delete()
        {
            Main main = new Main();
            int RowsCount = main.dataGridEvents.Rows.Count;

            main.DeleteEvent(Guid.NewGuid());

            Assert.AreEqual(RowsCount, main.dataGridEvents.RowCount);
        }

        [TestMethod]
        public void Main_Redact()
        {
            Main main = new Main();
            
        }
    }
}
