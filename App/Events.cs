using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App
{
    internal class Events
    {
        [Key]
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Category { get; set; }
        public List<Participation> Participant { get; set; }
        
    }
}
