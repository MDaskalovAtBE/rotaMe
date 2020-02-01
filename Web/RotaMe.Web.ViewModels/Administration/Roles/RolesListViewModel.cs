using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.ViewModels.Administration.Roles
{
    public class RolesListViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int UsersCount { get; set; }

        public string CardIcon { get; set; }
    }
}
