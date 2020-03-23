using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models.Owner.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.ViewModels.Owner.Events
{
    public class EventEventNeedsListViewModel : IMapFrom<EventEventNeedsListServiceModel>
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public DateTime Date { get; set; }
        public int MinimalUsers { get; set; }
        public int MaximumUsers { get; set; }
    }
}
