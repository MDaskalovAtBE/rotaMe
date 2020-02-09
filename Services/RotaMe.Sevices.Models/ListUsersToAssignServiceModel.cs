using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Sevices.Models
{
    public class ListUsersToAssignServiceModel : IMapFrom<RotaMeUser>
    {
        public string Id { get; set; }
        public string Username { get; set; }
    }
}
