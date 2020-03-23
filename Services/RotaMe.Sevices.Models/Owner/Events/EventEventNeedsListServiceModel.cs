using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Sevices.Models.Owner.Events
{
    public class EventEventNeedsListServiceModel : IMapFrom<EventNeed>
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public DateTime Date { get; set; }
        public int MinimalUsers { get; set; }
        public int MaximumUsers { get; set; }
    }
}
