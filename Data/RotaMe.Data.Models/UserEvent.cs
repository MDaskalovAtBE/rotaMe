using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Data.Models
{
    public class UserEvent
    {
        public string UserId { get; set; }
        public RotaMeUser User { get; set; }


        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
