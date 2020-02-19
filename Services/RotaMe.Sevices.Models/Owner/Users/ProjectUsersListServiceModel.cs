using AutoMapper;
using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Sevices.Models.Owner.Users
{
    public class ProjectUsersListServiceModel : IMapFrom<UserProject>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string Avatar { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public DateTime LastLoggedIn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<UserProject, ProjectUsersListServiceModel>()
                .ForMember(destination => destination.Id,
                            opts => opts.MapFrom(origin => origin.User.Id))
                .ForMember(destination => destination.Avatar,
                            opts => opts.MapFrom(origin => origin.User.Avatar))
                .ForMember(destination => destination.Username,
                            opts => opts.MapFrom(origin => origin.User.UserName))
                .ForMember(destination => destination.FullName,
                            opts => opts.MapFrom(origin => origin.User.FirstName + " " + origin.User.LastName))
                .ForMember(destination => destination.LastLoggedIn,
                            opts => opts.MapFrom(origin => origin.User.LastLoggedIn));
        }
    }
}
