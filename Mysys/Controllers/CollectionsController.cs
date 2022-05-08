using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mysys.Data;
using Mysys.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using static Mysys.Models.Collection;

namespace Mysys.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<CollectionsController> _logger;
        public static class GlobalVariables
        {
            public static int myid { get; set; }
        }
        public CollectionsController(ApplicationDbContext context, UserManager<IdentityUser> userManager,
            ILogger<CollectionsController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Collections
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Collections.Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Collections/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)

            {
                
                return NotFound();
            }

            var collection = await _context.Collections
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            GlobalVariables.myid = id;
            if (collection == null)
            {
                return NotFound();
            }

            return View(collection);
        }

        // GET: Collections/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "Id", "Id");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Theme,ImageURL,AdditionalTextFields,AdditionalDateTimeFields,AdditionalBoolFields,UserID")] Collection collection,
            IFormCollection document)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                string fieldTypes = document["fieldType"].ToString();
               

                int textFields = Regex.Matches(fieldTypes, "TextField").Count;
                int dateTimeFields = Regex.Matches(fieldTypes, "DateTimeField").Count;
                int boolFields = Regex.Matches(fieldTypes, "BoolField").Count;

                collection.UserID = await _userManager.GetUserIdAsync(currentUser);
                collection.AdditionalTextFields = textFields;
                collection.AdditionalDateTimeFields = dateTimeFields;
                collection.AdditionalBoolFields = boolFields;
                

                _context.Add(collection);
                await _context.SaveChangesAsync();
                string fieldNames = document["fieldName"].ToString();

                string[] subs1 = fieldTypes.Split(',');
                string[] subs2 = fieldNames.Split(',');

                var add = new Add();
                for (int i = 0; i < subs1.Length; i++)
                {
                    add = new Add();
                    add.CollectionID = collection.Id;
                    add.Field = subs2[i];
                    add.FieldType = subs1[i];
                    _context.Add(add);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(collection);
        }

        public IActionResult AddItems()
        {

            return View();
        }

        // GET: Collections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collections.FindAsync(id);
            if (collection == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "Id", "Id", collection.UserID);
            return View(collection);
        }

        // POST: Collections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Theme,ImageURL,AdditionalTextFields,AdditionalDateTimeFields,AdditionalBoolFields,UserID")] Collection collection)
        {
            if (id != collection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionExists(collection.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "Id", "Id", collection.UserID);
            return View(collection);
        }

        // GET: Collections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collections
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collection == null)
            {
                return NotFound();
            }

            return View(collection);
        }

        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collection = await _context.Collections.FindAsync(id);
            _context.Collections.Remove(collection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
       
        public async Task<IActionResult> CreateItem(int id)
        {
            id = GlobalVariables.myid;
            _logger.LogInformation(id.ToString());
            var item = new Item();
            item.CollectionID = id;
            _context.Items.Add(item);

            await _context.SaveChangesAsync();
          
       foreach(var add in _context.Adds.Where(m=>m.CollectionID==id))
            {
                if (add.FieldType== "TextField")
                {
                    var text = new TextField();
                    text.ItemId = item.Id;
                    text.Name = add.Field;
                    _context.TextFields.Add(text);
                }
                if (add.FieldType == "BoolField")
                {
                    var text = new BoolField();
                    text.ItemId = item.Id;
                    text.Name = add.Field;
                    _context.BoolFields.Add(text);
                }
                if (add.FieldType == "DateTimeField")
                {
                    var text = new DateTimeField();
                    text.ItemId = item.Id;
                    text.Name = add.Field;
                    _context.DateTimeFields.Add(text);
                }
            }
            await _context.SaveChangesAsync();


            return View();
        }

        private bool CollectionExists(int id)
        {
            return _context.Collections.Any(e => e.Id == id);
        }
    }
}
