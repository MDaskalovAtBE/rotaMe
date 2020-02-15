using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RotaMe.Web.InputModels.Owner.Projects
{
    public class ProjectCreateInputModel
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        public string Slug { get; set; }

        public string OwnerId { get; set; }


        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
