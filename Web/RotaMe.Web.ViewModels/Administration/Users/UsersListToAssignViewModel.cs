using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.ViewModels.Administration.Users
{
    public class UsersListToAssignViewModel : IMapFrom<ListUsersToAssignServiceModel>
    {
        public string Id { get; set; }

        public string Username { get; set; }
    }
}
