using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Sevices.Models
{
    public class ListRoleUserServiceModel : IMapFrom<RotaMeUser>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastLoggedIn { get; set; }

        public bool LockoutEnabled { get; set; }
        public string Avatar { get; set; }
    }
}
