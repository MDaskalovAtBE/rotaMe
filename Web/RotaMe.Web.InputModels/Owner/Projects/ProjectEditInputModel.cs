using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RotaMe.Web.InputModels.Owner.Projects
{
    public class ProjectEditInputModel
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        public string Slug { get; set; }


        [Required]
        [MinLength(50)]
        public string Description { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
