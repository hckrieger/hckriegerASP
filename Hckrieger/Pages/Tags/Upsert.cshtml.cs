using Hckrieger.Data;
using Hckrieger.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Hckrieger.Pages.Tags
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
        public Tag Tag { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            Tag = new Tag();

            if (id == null)
            {
                return Page();
            }

            Tag = await db.Tags.FirstOrDefaultAsync(u => u.TagId == id);

            if (Tag == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {

            if (ModelState.IsValid)
            {
                if (Tag.TagId == 0)
                {
                    db.Tags.Add(Tag);
                }
                else
                {
                    db.Tags.Update(Tag);
                }

                await db.SaveChangesAsync();
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
