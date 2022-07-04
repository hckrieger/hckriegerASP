using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Hckrieger.Models
{
    public class Tag
    {
        public int TagId { get; set; }

        [Required]
        public string? Name { get; set; }


        public ICollection<PostTag>? PostTag { get; set; }
    }
}
