using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RotaMe.Web.InputModels.Administration.Roles
{
    public class RoleCreateInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
