using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RotaMe.Web.InputModels.Owner.Events
{
    public class EventNeedEditInputModel
    {
        [Required]
        public int NeedId { get; set; }

        public string NeedDate { get; set; }

        [Required]
        [Display(Name = "Minimal Users")]
        public int MinUsers { get; set; }

        [Required]
        [Display(Name = "Maximal Users")]
        public int MaxUsers { get; set; }
    }
}
