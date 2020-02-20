using AutoMapper;
using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotaMe.Sevices.Models.Owner.Projects
{
    public class ProjectsListToRemoveUserServiceModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> Users { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Project, ProjectsListToRemoveUserServiceModel>()
                .ForMember(destination => destination.Users,
                            opts => opts.MapFrom(origin => origin.Users.Select(u => u.User.UserName).ToList()));
        }
    }
}
