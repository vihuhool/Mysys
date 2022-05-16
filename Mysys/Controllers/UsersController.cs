using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Mysys.Data;
using Mysys.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyAuthApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;

        public UsersController(ILogger<UsersController> logger,
            UserManager<User> userManager, ApplicationDbContext context, SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var validated = await Validate();
            var currentuser = await _userManager.GetUserAsync(User);
            if (currentuser.RoleId == "1")
            {
                if (!validated)
                {
                    return Redirect("Index");
                }
                return View();
            }
            else
            {
                
                return RedirectToRoute(new { action = "Index", controller = "Home", area = "" });
            }
        }

        public async Task<bool> Validate()
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            _logger.LogInformation("check started");
            if (user is null)
            {
                _logger.LogInformation("user is deleted");
                await _signInManager.SignOutAsync();
                return false;
            }
            else if (user.Status)
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation("user is blocked");
                return false;
            }
            _logger.LogInformation("check ended");
            return true;
        }

        [HttpPost]
        public async Task <IActionResult> Submit(IFormCollection fc)
        {
            string action = fc["action"];
            string[] Ids = fc["user"].ToString().Split(",");

            _logger.LogInformation(Ids.ToString() + "   " + action);

            switch (action)
            {
                case "Delete":
                    return Delete(Ids);
                case "Block":
                    return await Block(Ids);
                case "Unblock":
                    return await Unblock(Ids);
                case "Admin":
                    return await Admin(Ids);
                case "deAdmin":
                    return await  deAdmin(Ids);
            }
            return Redirect("Index");
        }

        [HttpPost]
        public ActionResult Delete(string[] Ids)
        {
            foreach (var Id in Ids)
            {
                if (Id == "on") { continue; }
                foreach (var user in _context.Users.ToList())
                {
                    if (user.Id == Id)
                    {
                        _context.Remove(user);
                        _context.SaveChanges();
                    }
                }
            }

            return Redirect("Index");
        }
        [HttpPost]
        public async Task <IActionResult> Admin(string[] Ids)
        {
            foreach (var Id in Ids)
            {
                if (Id == "on") { continue; }
                foreach (var user in _context.Users.ToList())
                {
                    if (user.Id == Id)
                    {
                        var curr = await _userManager.FindByIdAsync(Id); 
                       curr.RoleId = "1";
                        _context.SaveChanges();
                    }
                }
            }

            return Redirect("Index");
        }
        public async Task <IActionResult> deAdmin(string[] Ids)
        {
            foreach (var Id in Ids)
            {
                if (Id == "on") { continue; }
                foreach (var user in _context.Users.ToList())
                {
                    if (user.Id == Id)
                    {
                        var curr = await _userManager.FindByIdAsync(Id);
                        curr.RoleId = null;
                        _context.SaveChanges();
                    }
                }
            }

            return Redirect("Index");
        }
        [HttpPost]
        public async Task  <ActionResult> Block(string[] Ids)
        {
            foreach (var Id in Ids)
            {
                if (Id == "on") { continue; }
                foreach (var user in _context.Users.ToList())
                {
                    if (user.Id == Id)
                    {
                        var cur = await _userManager.FindByIdAsync(Id);
                        cur.Status = true;
                        _context.Update(user);
                        _context.SaveChanges();
                    }
                }
            }

            return Redirect("Index");
        }

        [HttpPost]
        public async Task  <ActionResult> Unblock(string[] Ids)
        {

            foreach (var Id in Ids)
            {
                if (Id == "on") { continue; }
                foreach (var user in _context.Users.ToList())
                {
                    if (user.Id == Id)
                    {
                        var cur = await _userManager.FindByIdAsync(Id);
                        cur.Status = false;
                        _context.Update(user);
                        _context.SaveChanges();
                    }
                }
            }
            return Redirect("Index");
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



    }
}