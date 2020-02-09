using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.ViewModels.Administration.Roles
{
    public class RolesListToAssignViewModel : IMapFrom<ListRolesToAssignServiceModel>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
