using Hckrieger.Data;
using Hckrieger.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Hckrieger.Pages.Categories
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
        public Category Category { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            Category = new Category();
            if (id == null)
            {
                return Page();
            }

            Category = await db.Categories.FirstOrDefaultAsync(m => m.CategoryId == id);

            if (Category == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Category.CategoryId == 0)
                {
                    db.Categories.Add(Category);
                }
                else
                {
                    db.Categories.Update(Category);
                }
                await db.SaveChangesAsync();
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
