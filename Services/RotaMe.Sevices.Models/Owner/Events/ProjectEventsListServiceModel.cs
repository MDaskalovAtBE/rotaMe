using AutoMapper;
using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotaMe.Sevices.Models.Owner.Events
{
    public class ProjectEventsListServiceModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public int EventNeeds { get; set; }
        public int Users { get; set; }
        public int Availabilities { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Event, ProjectEventsListServiceModel>()
                .ForMember(destination => destination.EventNeeds,
                            opts => opts.MapFrom(origin => origin.EventNeeds.Count()))
                .ForMember(destination => destination.Users,
                            opts => opts.MapFrom(origin => origin.Users.Count()))
                .ForMember(destination => destination.Availabilities,
                            opts => opts.MapFrom(origin => origin.Availabilities.Count()));
        }
    }
}
