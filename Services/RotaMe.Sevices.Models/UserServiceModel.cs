using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Sevices.Models
{
    public class UserServiceModel : IMapFrom<RotaMeUser>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderServiceModel Gender { get; set; }
        public DateTime LastLoggedIn { get; set; }
        public string Avatar { get; set; }
    }
}
