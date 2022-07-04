using Hckrieger.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hckrieger.ViewModels
{
    public class PostCategoryVM
    {
        public PostCategory PostCategory { get; set; }
        public Post Post { get; set; }

        public IEnumerable<PostCategory> PostCategoryList { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
