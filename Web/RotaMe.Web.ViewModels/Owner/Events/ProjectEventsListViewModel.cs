using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models.Owner.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.ViewModels.Owner.Events
{
    public class ProjectEventsListViewModel : IMapFrom<ProjectEventsListServiceModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public int EventNeeds { get; set; }
        public int Users { get; set; }
        public int Availabilities { get; set; }
    }
}
