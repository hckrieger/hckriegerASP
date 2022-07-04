using Hckrieger.Data;
using Hckrieger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Hckrieger.Pages.Posts
{
    public class ArchivesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ArchivesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Post> Posts { get; set; }

        public async Task OnGetAsync()
        {
            Posts = await _context.Posts
                .OrderByDescending(m => m.PostId)
                .Where(m => m.Visible)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
