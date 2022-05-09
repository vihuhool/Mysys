using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mysys.Data;
using Mysys.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mysys.Controllers
{
   

        public class HomeController : Controller
        {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager,
           ILogger<HomeController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
            {
          
            return View(_context.Collections);
        }

            public IActionResult Privacy()
            {
                return View();
            }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        public async Task<IActionResult> DetailsItem(int? id)
        {
            if (id != null)
            {

                Item user = await _context.Items.Include(p => p.TextFields)
                    .Include(p => p.DateTimeFields)
                    .Include(p => p.BoolFields)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id != null)
            {
                

                System.Collections.Generic.List<Item> items = new System.Collections.Generic.List<Item>();
                foreach (var item in _context.Items.Where(m => m.CollectionID == id))
                {
                    if (item.Name == null) _context.Items.Remove(item);
                    else items.Add(item);
                }
                await _context.SaveChangesAsync();

                return View(items);
            }
            return NotFound();
        }
    }

}
