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
using Microsoft.AspNetCore.Authorization;
using Firebase.Storage;
using System.Threading;
using Firebase.Auth;
using System.IO;
using HeyRed.MarkdownSharp;

namespace Mysys.Controllers
{
    [Authorize]
    public class CollectionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.User> _userManager;
        private readonly ILogger<CollectionsController> _logger;

        private readonly string apiKey =  "AIzaSyAcj9ybqBveF5DwSxpryQoerbarAI9fk4w";
        private readonly string storageBucket = "mysys-e8594.appspot.com";
        private readonly string login = "kirillmoiseev76@gmail.com";
        private readonly string password =  "Zaraza_11";
      
        public CollectionsController(ApplicationDbContext context, UserManager<Models.User> userManager,
            ILogger<CollectionsController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var fileUpload = file;
            if (fileUpload.Length > 0)
            {
                Stream stream = fileUpload.OpenReadStream();

                var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                var signIn = await auth.SignInWithEmailAndPasswordAsync(login, password);

                var cancellation = new CancellationTokenSource();

                var upload = new FirebaseStorage(
                storageBucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(signIn.FirebaseToken),
                    ThrowOnCancel = true
                }
                )
                .Child($"images")
                .Child($"{DateTime.Now}".Replace("/", "."))
                .PutAsync(stream, cancellation.Token);

                string link = await upload;
                var currentUser = await _userManager.GetUserAsync(User);
                
                TempData["imageurl"+currentUser.Id] = link;
                
                _logger.LogInformation("imageurl" + currentUser.Id);
                return Ok();
            }
            return BadRequest();
        }

        // GET: Collections
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            _logger.LogInformation(currentUser.Id.ToString());
            var applicationDbContext = _context.Collections.Include(c => c.User).Where(i=>i.UserID==currentUser.Id);
            
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Collections/Details/5
        public async Task<IActionResult> Details(int id)
        {
          
              

            // var items= _context.Items.Include(c => c.Collection).Where(m => m.CollectionID == id);
            Collection collection = await _context.Collections.Include(e => e.Items).Include(j=>j.Tags).FirstOrDefaultAsync(i=>i.Id==id);
            foreach (var item in _context.Items.Where(m => m.CollectionID == id))
            {
                if (item.Name == null) _context.Items.Remove(item);
            }
            await _context.SaveChangesAsync();

            return  View(collection);
            
            
        }

        // GET: Collections/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Set<Models.User>(), "Id", "Id");
            var currentUser = _userManager.GetUserAsync(User);
            TempData["imageurl" + currentUser.Id] = null;
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
              
                if (TempData["imageurl"+currentUser.Id] != null) collection.ImageURL = TempData["imageurl"+currentUser.Id].ToString();

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
            ViewData["UserID"] = new SelectList(_context.Set<Models.User>(), "Id", "Id", collection.UserID);
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
            ViewData["UserID"] = new SelectList(_context.Set<Models.User>(), "Id", "Id", collection.UserID);
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
            _logger.LogInformation(id.ToString());
            // id = GlobalVariables.myid;
            var currentUser = await _userManager.GetUserAsync(User);
            TempData["imageurl"+currentUser.Id] = null;
            var item = new Item();
            item.CollectionID = id;
            
            
            item.UserID = await _userManager.GetUserIdAsync(currentUser);
            item.UserName= await _userManager.GetUserNameAsync(currentUser);
            //_logger.LogInformation(item.ToString());

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
            //_logger.LogInformation(item.Id.ToString());
           
            return PartialView(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItem([Bind("Id,ImageURL,Name,CollectionID,UserID,UserName")] Item newitem,
            IFormCollection document)
        {
            
            if (ModelState.IsValid)
            {
                _logger.LogInformation(newitem.Id.ToString());
                string[] textval = document["Text"];
                string[] boolval = document["Bool"];
                string[] dateval = document["Date"];
                string tags = document["Tags"];
                

                int i = 0;
                foreach (var add in _context.TextFields.Where(m => m.ItemId == newitem.Id))
                {
                    add.Content = textval[i];
                    i++;
                }
                i = 0;
                foreach (var add in _context.BoolFields.Where(m => m.ItemId == newitem.Id))
                {

                    if(boolval[i]=="false")
                    add.Content =false;
                    else add.Content = true;
                    i++;
                }
                i = 0;
                foreach (var add in _context.DateTimeFields.Where(m => m.ItemId == newitem.Id))
                {
                    add.Content = DateTime.Parse(dateval[i]);
                    i++;
                }
                i = 0;
                var currentUser = await _userManager.GetUserAsync(User);
                var item = new Tag();
                item.ItemId = newitem.Id;
                item.CollectionId = newitem.CollectionID;
                item.Content = tags;
                _context.Tags.Add(item);


                _logger.LogInformation("imageurl"+currentUser.Id);
                if (TempData["imageurl"+currentUser.Id] != null) newitem.ImageURL = TempData["imageurl"+currentUser.Id].ToString();

                _context.Update(newitem);
                  await _context.SaveChangesAsync();


                Collection collection = await _context.Collections.Include(e => e.Items).FirstOrDefaultAsync(i => i.Id == newitem.CollectionID);
                return RedirectToAction("Details", collection);

            }
            return View();
        }
       

        [HttpGet]
        [ActionName("DeleteItem")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Item user = await _context.Items.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return PartialView(user);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteItem(int? id)
        {
            //_logger.LogInformation(id.ToString());
            var ans = await _context.Items.FirstOrDefaultAsync(p => p.Id == id);
            
            
            if (id != null)
            {
                Item user = await _context.Items.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                {
                   _context.Items.Remove(user);
                    await _context.SaveChangesAsync();

                    var items = _context.Items.Where(c => c.CollectionID == ans.CollectionID);
                    Collection collection = await _context.Collections.Include(e => e.Items).FirstOrDefaultAsync(i => i.Id == ans.CollectionID);

                    return RedirectToAction("Details", collection);
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> DetailsItem(int? id)
        {
            if (id != null)
            {

                Item user = await _context.Items.Include(p=>p.TextFields)
                    .Include(p=>p.DateTimeFields)
                    .Include(p=>p.BoolFields)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }

        private bool CollectionExists(int id)
        {
            return _context.Collections.Any(e => e.Id == id);
        }
    }
}
