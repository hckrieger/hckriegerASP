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
    public class ManageCategoriesModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public ManageCategoriesModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public PostCategoryVM PostCategoryVM { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            PostCategoryVM = new PostCategoryVM
            {
                PostCategoryList = await db.PostCategories.Include(u => u.Category)
                                               .Include(u => u.Post)
                                               .Where(u => u.PostId == id).ToListAsync(),
                PostCategory = new PostCategory()
                {
                    PostId = id
                },
                Post = await db.Posts.FirstOrDefaultAsync(u => u.PostId == id)
            };

            List<int> tempListOfAssignedCategories = PostCategoryVM.PostCategoryList.Select(i => i.CategoryId).ToList();
            var categoryList = db.Categories.Where(u => !tempListOfAssignedCategories.Contains(u.CategoryId)).OrderBy(m => m.Name);

            PostCategoryVM.CategoryList = categoryList.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.CategoryId.ToString()
            });


            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (PostCategoryVM.PostCategory.PostId != 0 && PostCategoryVM.PostCategory.CategoryId != 0)
            {
                db.PostCategories.Add(PostCategoryVM.PostCategory);
                await db.SaveChangesAsync();
            }

            return RedirectToPage(new { @id = PostCategoryVM.PostCategory.PostId });
        }

        public async Task<IActionResult> OnPostDelete(int categoryId)
        {
            int postId = PostCategoryVM.Post.PostId;

            if (postId == 0)
            {
                return NotFound();
            }

            PostCategory PostCategoryToRemove = await db.PostCategories.FirstOrDefaultAsync(u => u.PostId == postId && u.CategoryId == categoryId);

            db.PostCategories.Remove(PostCategoryToRemove);
            await db.SaveChangesAsync();

            return RedirectToPage(new { @id = postId });
        }
    }
}
