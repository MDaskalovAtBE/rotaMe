using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models.Owner.Events;
using RotaMe.Web.ViewModels.Owner.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.ViewModels.Owner.Events
{
    public class EventDetailsViewModel : IMapFrom<EventDetailsServiceModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public IList<EventEventNeedsListViewModel> EventNeeds { get; set; }
        public IList<EventUsersListViewModel> Users { get; set; }
        public IList<EventAvailabilitiesListViewModel> Availabilities { get; set; }
    }
}
