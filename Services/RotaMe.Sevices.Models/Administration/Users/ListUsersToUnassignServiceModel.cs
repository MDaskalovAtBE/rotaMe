using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models.Administration.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Sevices.Models.Administration.Users
{
    public class ListUsersToUnassignServiceModel : IMapFrom<RotaMeUser>
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public IList<string> Roles { get; set; }
    }
}
