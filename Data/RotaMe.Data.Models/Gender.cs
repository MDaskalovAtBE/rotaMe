using System.ComponentModel.DataAnnotations;

namespace RotaMe.Data.Models
{
    public class Gender : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }
    }
}