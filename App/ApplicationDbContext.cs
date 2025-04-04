using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    internal class EventsContext : DbContext
    {
        public EventsContext() : base("EventContext") { }
        public DbSet<Events> Events { get; set; }
        public DbSet<Participation> Participation { get; set; }

    }
}
