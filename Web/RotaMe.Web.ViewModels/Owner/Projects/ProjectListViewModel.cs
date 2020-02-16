using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.ViewModels.Owner.Projects
{
    public class ProjectListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Slug { get; set; }


        public string Description { get; set; }

        public string Image { get; set; }

        public int UsersCount { get; set; }
    }
}
