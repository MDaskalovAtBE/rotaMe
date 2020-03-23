using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RotaMe.Data.Models
{
    public class EventNeed : BaseModel<int>
    {
        [Required]
        public int EventId { get; set; }

        public Event Event { get; set; }

        public DateTime Date { get; set; }


        [Required]
        public int MinimalUsers { get; set; }

        [Required]
        public int MaximumUsers { get; set; }


        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
