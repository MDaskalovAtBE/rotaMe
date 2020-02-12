using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RotaMe.Web.InputModels.Administration.Users
{
    public class UserEditInputModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Birth Day")]
        public string BirthDay { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        public IFormFile Avatar { get; set; }
    }
}
