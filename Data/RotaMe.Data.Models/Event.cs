using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RotaMe.Data.Models
{
    public class Event : BaseModel<int>
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        public string Slug { get; set; }


        [Required]
        public string CreatorId { get; set; }

        public RotaMeUser Creator { get; set; }


        [Required]
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }


        public ICollection<EventNeed> EventNeeds { get; set; } = new List<EventNeed>();
        public ICollection<Availability> Availabilities { get; set; } = new List<Availability>();
    }
}