using Hckrieger.Data;
using Hckrieger.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hckrieger.Pages.Posts
{
    [Authorize]
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext db;


        public UpsertModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public PostVM PostVM { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            PostVM = new PostVM();

            if (id == null)
            {
                return Page();
            }

            PostVM.Post = await db.Posts.FirstOrDefaultAsync(m => m.PostId == id);

            if (PostVM.Post == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (PostVM.Post.PostId == 0)
                {
                    db.Posts.Add(PostVM.Post);
                }
                else
                {
                    db.Posts.Update(PostVM.Post);
                }

                await db.SaveChangesAsync();

            }

            return RedirectToPage("Index");
        }
    }
}
