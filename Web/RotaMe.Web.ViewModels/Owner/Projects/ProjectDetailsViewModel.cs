using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models.Owner.Projects;
using RotaMe.Web.ViewModels.Owner.Events;
using RotaMe.Web.ViewModels.Owner.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.ViewModels.Owner.Projects
{
    public class ProjectDetailsViewModel : IMapFrom<ProjectDetailsServiceModel>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public IList<ProjectUsersListViewModel> Users { get; set; }
        public IList<ProjectEventsListViewModel> Events { get; set; }
    }
}
