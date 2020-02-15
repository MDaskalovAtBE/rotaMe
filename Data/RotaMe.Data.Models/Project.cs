using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RotaMe.Data.Models
{
    public class Project : BaseModel<int>
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        public string Slug { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public RotaMeUser Owner { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }


        public ICollection<UserProject> Users { get; set; } = new List<UserProject>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
