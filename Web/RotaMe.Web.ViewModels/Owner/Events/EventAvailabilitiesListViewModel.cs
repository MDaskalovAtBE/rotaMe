using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models.Owner.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.ViewModels.Owner.Events
{
    public class EventAvailabilitiesListViewModel : IMapFrom<EventAvailabilitiesListServiceModel>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int EventId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool Assigned { get; set; }
    }
}
