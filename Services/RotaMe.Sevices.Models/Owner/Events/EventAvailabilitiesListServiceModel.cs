using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Sevices.Models.Owner.Events
{
    public class EventAvailabilitiesListServiceModel : IMapFrom<Availability>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int EventId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool Assigned { get; set; }
    }
}
