using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hckrieger.Models
{
    public class Post
    {
        public int PostId { get; set; }

        [Required]
        public string? Title { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:f}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Published")]
        public DateTime DatePublished { get; set; }

        public ICollection<PostCategory>? PostCategory { get; set; }

        public ICollection<PostTag>? PostTag { get; set; }

        [Required]
        public string? Content { get; set; }

        [DefaultValue(false)]
        public bool Visible { get; set; }


    }
}
