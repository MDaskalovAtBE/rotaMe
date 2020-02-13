using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Sevices.Models.Administration.Users
{
    public class UserEditServiceModel
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string BirthDay { get; set; }

        public string Gender { get; set; }

        public string Avatar { get; set; }
    }
}
