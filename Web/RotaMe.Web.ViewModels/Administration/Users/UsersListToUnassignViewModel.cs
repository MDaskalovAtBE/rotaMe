using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models.Administration.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.ViewModels.Administration.Users
{
    public class UsersListToUnassignViewModel : IMapFrom<ListUsersToUnassignServiceModel>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Roles { get; set; }
    }
}
