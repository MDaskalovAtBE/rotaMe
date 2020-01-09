using System.ComponentModel.DataAnnotations;

namespace RotaMe.Data.Models
{
    public class BaseModel<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}
