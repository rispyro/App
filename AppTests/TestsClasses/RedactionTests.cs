using System;
using App;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppTests
{
    [TestClass]
    public class RedactionTests
    {
        [TestMethod]
        public void Redaction_UpdateEvent()
        {
            Main main = new Main();
            Add add = new Add(main);

            Guid id = Guid.NewGuid();
            Events events = new Events()
            {
                EventId = id,
                Title = "ы",
                Description = "ы",
                Date = "ы",
                Time = "12:30",
                Category = "ы"
            };

            add.AddNewEvent(events);
            main.LoadEvents();

            Redaction redact = new Redaction(id, main);
            Events updEvent = new Events()
            {
                EventId = Guid.NewGuid(),
                Title = "ы",
                Description = "ы",
                Date = "ы",
                Time = "12:30",
                Category = "ы"
            };
            updEvent.Title = "ssd";
            redact.UpdateEvent(updEvent, id);

            bool IsAInBd = false;
            bool IsBInBd = true;
            using (var db = new EventsContext())
            {
                if (db.Events.Find(id) != null)
                {
                    IsAInBd = !IsAInBd;
                }
                if (db.Events.Find(updEvent.EventId) != null)
                {
                    IsBInBd = !IsBInBd;
                }
            }

            Assert.AreEqual(false, IsBInBd && !IsBInBd);
        }
        [TestMethod]
        public void Redaction_UpdateEventWithError()
        {
            Main main = new Main();
            Add add = new Add(main);

            Guid id = Guid.NewGuid();
            Events events = new Events()
            {
                EventId = id,
                Title = "ы",
                Description = "ы",
                Date = "ы",
                Time = "12:30",
                Category = "ы"
            };

            add.AddNewEvent(events);
            main.LoadEvents();

            Redaction redact = new Redaction(id, main);
            Events updEvent = new Events()
            {
                EventId = Guid.NewGuid(),
                Title = "ы",
                Description = "ы",
                Date = "ы",
                Time = "12:30",
                Category = ""
            };
            redact.UpdateEvent(updEvent, id);

            bool ans = true;

            using (var db = new EventsContext())
            {
                if (db.Events.Find(updEvent.EventId) == null)
                {
                    ans = !ans;
                }
            }

            Assert.AreEqual(false, ans);
        }
    }
}
