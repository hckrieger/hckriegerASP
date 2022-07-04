using Hckrieger.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hckrieger.ViewModels
{
    public class PostTagVM
    {
        public PostTag PostTag { get; set; }
        public Post Post { get; set; }

        public IEnumerable<PostTag> PostTagList { get; set; }
        public IEnumerable<SelectListItem> TagList { get; set; }
    }
}
