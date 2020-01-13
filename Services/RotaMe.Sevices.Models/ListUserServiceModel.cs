using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using System;
using System.Collections.Generic;

namespace RotaMe.Sevices.Models
{
    public class ListUserServiceModel : IMapFrom<RotaMeUser>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public UserGenderServiceModel Gender { get; set; }

        public bool LockoutEnabled { get; set; }

        public List<string> Roles { get; set; }

        public DateTime LastLoggedIn { get; set; }
        public string Avatar { get; set; }

    }
}
