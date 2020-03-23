using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RotaMe.Web.InputModels.Owner.Events
{
    public class EventEditInputModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }


        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
