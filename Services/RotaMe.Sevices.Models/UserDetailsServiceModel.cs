using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Sevices.Models
{
    public class UserDetailsServiceModel : IMapFrom<RotaMeUser>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserName { get; set; }
        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime BirthDay { get; set; }

        public UserGenderServiceModel Gender { get; set; }

        public bool LockoutEnabled { get; set; }

        public IList<string> Roles { get; set; }

        public DateTime LastLoggedIn { get; set; }
        public string Avatar { get; set; }
    }
}
