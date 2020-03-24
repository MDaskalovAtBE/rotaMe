using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RotaMe.Web.InputModels.Owner.Events
{
    public class EventNeedCreateInputModel
    {
        [Required]
        public int EventId { get; set; }

        public string Date { get; set; }

        [Required]
        [Display(Name = "Minimal Users")]
        public int MinimalUsers { get; set; }

        [Required]
        [Display(Name = "Maximal Users")]
        public int MaximumUsers { get; set; }
    }
}
