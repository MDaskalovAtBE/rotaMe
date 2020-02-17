using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models.Owner.Projects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.ViewModels.Owner.Projects
{
    public class ProjectsListToAddUserViewModel : IMapFrom<ProjectsListToAddUserServiceModel>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Selected { get; set; }
    }
}
