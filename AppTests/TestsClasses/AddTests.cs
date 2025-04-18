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

            add.AddNewParticipation(participation);

            Assert.AreEqual(null, new EventsContext().Participation.Find(participation.ParticipationId));
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

            Guid id = (Guid)add.dataGridParticipant.Rows[add.dataGridParticipant.Rows.Count - 1].Cells[0].Value;

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
        [TestMethod]
        public void Add_AddNewPart()
        {
            Add add = new Add(new Main());

            Participation newPart = new Participation() { Name = "Василий" };
            add.AddNewParticipation(newPart);

            bool ans = true;

            if (add.dataGridParticipant.Rows[0].Cells[1].Value.ToString() != newPart.Name)
            {
                ans = !ans;
            }

            Assert.AreEqual(true, ans);
        }
        [TestMethod]
        public void Add_AddNWithUncorrectTime()
        {
            Add add = new Add(new Main());

            Events events = new Events()
            {
                EventId = Guid.NewGuid(),
                Title = "ы1",
                Description = "ы",
                Date = "ы",
                Time = "00:00",
                Category = "ы"
            };

            add.AddNewEvent(events);

            bool ans = false;
            using (var db = new EventsContext())
            {
                if (db.Events.Find(events.EventId) == null)
                {
                    ans = !ans;
                }
            }

            Assert.AreEqual(true, ans);
        }
    }
}
