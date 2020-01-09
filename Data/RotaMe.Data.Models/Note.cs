using System.ComponentModel.DataAnnotations;

namespace RotaMe.Data.Models
{
    public class Note : BaseModel<int>
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        public string Description { get; set; }
    }
}