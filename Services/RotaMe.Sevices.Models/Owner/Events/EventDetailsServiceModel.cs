using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models.Owner.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Sevices.Models.Owner.Events
{
    public class EventDetailsServiceModel : IMapFrom<Event>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public IList<EventEventNeedsListServiceModel> EventNeeds { get; set; }
        public IList<EventUsersListServiceModel> Users { get; set; }
        public IList<EventAvailabilitiesListServiceModel> Availabilities { get; set; }
    }
}
