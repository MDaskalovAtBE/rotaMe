using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Sevices.Models.Owner.Events
{
    public class EventCreateServiceModel
    {
        public string Name { get; set; }

        public string Slug { get; set; }

        public int ProjectId { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
    }
}
