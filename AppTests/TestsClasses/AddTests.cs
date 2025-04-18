using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using App;

namespace AppTests
{
    [TestClass]
    public class AddTests
    {
        [TestMethod]
        public void Add_AddNewParticioation_EmptyArgument()
        {
            Add add = new Add(new Main());
            Participation participation = new Participation();

            Assert.ThrowsException<Exception>(() => add.AddNewParticipation(participation));
        }
        [TestMethod]
        public void Add_AddNewEvent_EmptyArgument()
        {
            Add add = new Add(new Main());
            Events ev = new Events() { EventId = Guid.NewGuid()};
            bool actual = true;
            using (var bd = new EventsContext())
            {
                if (bd.Events.Find(ev.EventId) == null)
                {
                    actual = !actual;
                }
            }
            Assert.AreEqual(false, actual);
        }
        [TestMethod]
        public void DeletePart()
        {
            Add add = new Add(new Main());
            Participation part = new Participation() { Name = "Вася" };
            int RowsCount = add.dataGridParticipant.Rows.Count;
            add.AddNewParticipation(part);

            Guid id = (Guid)add.dataGridParticipant.Rows[add.dataGridParticipant.Rows.Count - 2].Cells[0].Value;

            add.DeletePart(id);

            bool curr = true;
            using (EventsContext db = new EventsContext())
            {
                if (db.Participation.Find(id) == null)
                {
                    curr = !curr;
                }
            }

            Assert.AreEqual(false, curr);
        }
    }
}
