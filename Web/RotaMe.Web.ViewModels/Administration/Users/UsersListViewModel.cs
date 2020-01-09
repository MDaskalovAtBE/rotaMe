using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.ViewModels.Administration.Users
{
    public class UsersListViewModel : IMapFrom<UserServiceModel>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GenderName { get; set; }
        public DateTime LastLoggedIn { get; set; }
        public string Avatar { get; set; }

        public List<UsersListRoleViewModel> Roles { get; set; }
    }
}
