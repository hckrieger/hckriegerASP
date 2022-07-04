using Hckrieger.Data;
using Hckrieger.Models;
using Hckrieger.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hckrieger.Pages.Posts
{
    [Authorize]
    public class ManageTagsModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public ManageTagsModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public PostTagVM PostTagVM { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            PostTagVM = new PostTagVM
            {
                PostTagList = await db.PostTags.Include(u => u.Tag)
                                               .Include(u => u.Post)
                                               .Where(u => u.PostId == id).ToListAsync(),
                PostTag = new PostTag()
                {
                    PostId = id
                },
                Post = await db.Posts.FirstOrDefaultAsync(u => u.PostId == id)
            };

            List<int> tempListOfAssignedTags = PostTagVM.PostTagList.Select(i => i.TagId).ToList();
            var tagList = db.Tags.Where(u => !tempListOfAssignedTags.Contains(u.TagId)).OrderBy(m => m.Name);

            PostTagVM.TagList = tagList.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.TagId.ToString()
            });


            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (PostTagVM.PostTag.PostId != 0 && PostTagVM.PostTag.TagId != 0)
            {
                db.PostTags.Add(PostTagVM.PostTag);
                await db.SaveChangesAsync();
            }

            return RedirectToPage(new { @id = PostTagVM.PostTag.PostId });
        }

        public async Task<IActionResult> OnPostDelete(int tagId)
        {
            int postId = PostTagVM.Post.PostId;

            if (postId == 0)
            {
                return NotFound();
            }

            PostTag PostTagToRemove = await db.PostTags.FirstOrDefaultAsync(u => u.PostId == postId && u.TagId == tagId);

            db.PostTags.Remove(PostTagToRemove);
            await db.SaveChangesAsync();

            return RedirectToPage(new { @id = postId });
        }
    }
}
