using System;

namespace App
{
    public class Participation
    {
        public Guid ParticipationId { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public Guid EventId { get; set; }
    }
}
