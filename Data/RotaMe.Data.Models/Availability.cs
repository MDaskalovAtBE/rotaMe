using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RotaMe.Data.Models
{
    public class Availability : BaseModel<int>
    {
        [Required]
        public string UserId { get; set; }
        public RotaMeUser User { get; set; }


        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool Assigned { get; set; }
    }
}
