using AutoMapper;
using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models.Owner.Events;
using RotaMe.Sevices.Models.Owner.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotaMe.Sevices.Models.Owner.Projects
{
    public class ProjectDetailsServiceModel : IMapFrom<Project>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }

        public string Image { get; set; }

        public IList<ProjectUsersListServiceModel> Users { get; set; }
        public IList<ProjectEventsListServiceModel> Events { get; set; }

    }
}
