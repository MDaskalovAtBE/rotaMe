using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.ViewModels.Owner.Projects
{
    public class ProjectsListToRemoveUserViewModel 
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Users { get; set; }

        public bool Selected { get; set; }
    }
}
