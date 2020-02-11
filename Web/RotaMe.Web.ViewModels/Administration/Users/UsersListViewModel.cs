using AutoMapper;
using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.ViewModels.Administration.Users
{
    public class UsersListViewModel : IMapFrom<ListUserServiceModel>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GenderName { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }
        public DateTime LastLoggedIn { get; set; }

        public bool LockoutEnabled { get; set; }
        public string Avatar { get; set; }

        public IList<string> Roles { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<ListUserServiceModel, UsersListViewModel>()
                .ForMember(destination => destination.Age,
                            opts => opts.MapFrom(origin => CalculateAge(origin.BirthDay)));
        }

        private static int CalculateAge(DateTime birthDay)
        {
            int years = DateTime.Now.Year - birthDay.Year;

            if ((birthDay.Month > DateTime.Now.Month) || (birthDay.Month == DateTime.Now.Month && birthDay.Day > DateTime.Now.Day))
                years--;

            return years;
        }

    }
}
