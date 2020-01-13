using AutoMapper;
using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.ViewModels.Administration.Users
{
    public class UsersListViewModel : IMapFrom<ListUserServiceModel>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GenderName { get; set; }
        public DateTime LastLoggedIn { get; set; }

        public bool LockoutEnabled { get; set; }
        public string Avatar { get; set; }

        public IList<string> Roles { get; set; }
    }
}
