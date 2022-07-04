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

namespace Hckrieger.Pages.Tags
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Hckrieger.Data.ApplicationDbContext _context;

        public IndexModel(Hckrieger.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Tag> Tag { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Tags != null)
            {
                Tag = await _context.Tags.ToListAsync();
            }
        }
    }
}
