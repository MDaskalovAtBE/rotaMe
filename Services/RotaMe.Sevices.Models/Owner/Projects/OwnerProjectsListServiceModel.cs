using AutoMapper;
using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotaMe.Sevices.Models.Owner.Projects
{
    public class OwnerProjectsListServiceModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Slug { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int UsersCount { get; set; }


        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Project, OwnerProjectsListServiceModel>()
                .ForMember(destination => destination.UsersCount,
                            opts => opts.MapFrom(origin => origin.Users.Count()));
        }
    }
}
