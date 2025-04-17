using Microsoft.VisualStudio.TestTools.UnitTesting;
using App;
using System;

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
                Title = "ы",
                Description = "ы",
                Date = "ы",
                Time = "ы",
                Category = "ы"
            };
            int RowsCount = main.dataGridEvents.Rows.Count;
            add.AddNewEvent(events);
            main.LoadEvents();
            Guid id = (Guid)main.dataGridEvents.Rows[main.dataGridEvents.Rows.Count - 1].Cells[0].Value;

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
    }
}
