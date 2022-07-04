using Hckrieger.Data;
using Hckrieger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Hckrieger.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public IndexModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IList<Post> Posts { get; set; }

        public IList<Post> TotalPosts { get; set; }

        public async Task OnGetAsync()
        {


            Posts = await db.Posts
                .OrderByDescending(m => m.PostId)
                .Where(m => m.Visible)
                .Take(4)
                .AsNoTracking()
                .ToListAsync();

            TotalPosts = await db.Posts
                .OrderByDescending(m => m.PostId)
                .Where(m => m.Visible)
                .AsNoTracking()
                .ToListAsync();

        }
    }
}