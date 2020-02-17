using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models.Owner.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.ViewModels.Owner.Users
{
    public class UsersListToAddToProjectViewModel : IMapFrom<UsersListToAddToProjectServiceModel>
    {
        public string Id { get; set; }

        public string Username { get; set; }
    }
}
