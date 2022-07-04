using System.ComponentModel.DataAnnotations.Schema;

namespace Hckrieger.Models
{
    public class PostCategory
    {
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post? Post { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
