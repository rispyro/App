using System.Data.Entity;

namespace App
{
    public class EventsContext : DbContext
    {
        public EventsContext() : base("EventContext") { }
        public DbSet<Events> Events { get; set; }
        public DbSet<Participation> Participation { get; set; }

    }
}
