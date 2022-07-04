using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hckrieger.Data;
using Hckrieger.Models;
using Microsoft.AspNetCore.Authorization;

namespace Hckrieger.Pages.Posts
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Post> Post { get; set; }

        public async Task OnGetAsync()
        {
            Post = await _context.Posts
                .OrderByDescending(m => m.PostId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostVisibility(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            if (!post.Visible)
            {
                post.Visible = true;
            } else
            {
                post.Visible = false;
            }

            _context.Update(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");   
        }
    }
}
