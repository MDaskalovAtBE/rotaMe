using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Sevices.Models
{
    public class UserCreateServiceModel
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string Avatar { get; set; }

        public string Password { get; set; }

    }
}
