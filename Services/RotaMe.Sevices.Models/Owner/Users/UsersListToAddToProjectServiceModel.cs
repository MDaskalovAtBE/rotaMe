using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Sevices.Models.Owner.Users
{
    public class UsersListToAddToProjectServiceModel : IMapFrom<RotaMeUser>
    {
        public string Id { get; set; }

        public string Username { get; set; }
    }
}
