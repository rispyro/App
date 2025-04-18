using System;

namespace App
{
    /// <summary>
    /// Представляет участника, привязанного к определённому событию
    /// </summary>
    public class Participation
    {
        /// <summary>
        /// Уникальный идентификатор участника
        /// </summary>
        public Guid ParticipationId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Имя участника
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор события, к которому привязан участник
        /// </summary>
        public Guid EventId { get; set; }
    }
}
