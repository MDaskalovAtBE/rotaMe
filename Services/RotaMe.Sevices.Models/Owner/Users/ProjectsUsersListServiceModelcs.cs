using AutoMapper;
using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Sevices.Models.Owner.Users
{
    public class ProjectsUsersListServiceModel : IMapFrom<UserProject>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string Avatar { get; set; }
        public string FullName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<UserProject, ProjectsUsersListServiceModel>()
                .ForMember(destination => destination.FullName,
                            opts => opts.MapFrom(origin => origin.User.FirstName + " " + origin.User.LastName))
                .ForMember(destination => destination.Avatar,
                            opts => opts.MapFrom(origin => origin.User.Avatar))
                .ForMember(destination => destination.Id,
                            opts => opts.MapFrom(origin => origin.User.Id));
        }
    }
}
