﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App
{
    public class Events
    {
        [Key]
        public Guid EventId { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Category { get; set; }
        public List<Participation> Participant { get; set; }
        
    }
}
