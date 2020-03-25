using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Sevices.Models.Owner.Events
{
    public class EventNeedEditServiceModel
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public int MinimalUsers { get; set; }

        public int MaximumUsers { get; set; }
    }
}
