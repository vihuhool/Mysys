using Microsoft.AspNetCore.Http;
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
        private readonly UserManager<User> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, UserManager<User> userManager,
           ILogger<HomeController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Collections;
            
                var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                if (currentUser.RoleId == "1")
                {
                    return View("AdminIndex", await applicationDbContext.ToListAsync());
                }
                else return View(await applicationDbContext.ToListAsync());
            }

        }

        [HttpPost]
        public IActionResult SetTheme(string data)
        {
            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(1);
            _logger.LogInformation(data.ToString());
            Response.Cookies.Append("theme", data, cookies);
            return Ok();
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
            Collection collection = await _context.Collections.Include(e => e.Items).Include(j => j.Tags).FirstOrDefaultAsync(i => i.Id == id);
            foreach (var item in _context.Items.Where(m => m.CollectionID == id))
            {
                if (item.Name == null) _context.Items.Remove(item);
            }
            await _context.SaveChangesAsync();

            return View(collection);
        }
    }

}
