using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.InputModels.Administration.Roles
{
    public class RoleUnassignFromUserInputModel
    {
        public string UserId { get; set; }

        public string RoleName { get; set; }
    }
}
