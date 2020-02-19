using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RotaMe.Web.InputModels.Owner.Events
{
    public class EventCreateInputModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public string Slug { get; set; }


        public int ProjectId { get; set; }


        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
