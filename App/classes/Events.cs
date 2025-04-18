
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App
{
    /// <summary>
    /// Представляет событие, содержащее основную информацию и список участников.
    /// </summary>
    public class Events
    {
        /// <summary>
        /// Уникальный идентификатор события
        /// </summary>
        [Key]
        public Guid EventId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Название события
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание события
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата проведения события (в строковом формате)
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Время проведения события (в строковом формате)
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Категория события (например, конференция, вебинар и т.д.)
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Список участников, привязанных к событию
        /// </summary>
        public List<Participation> Participant { get; set; }

    }
}
