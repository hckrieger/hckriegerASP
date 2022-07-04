using System.ComponentModel.DataAnnotations;

namespace Hckrieger.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<PostCategory>? PostCategory { get; set; }
    }
}
