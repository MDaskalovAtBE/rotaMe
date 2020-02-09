using Microsoft.AspNetCore.Identity;
using RotaMe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Sevices.Models
{
    public class ListRolesToAssignServiceModel : IMapFrom<IdentityRole>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
