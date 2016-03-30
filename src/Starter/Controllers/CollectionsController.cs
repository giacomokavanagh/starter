using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;

namespace Starter.Controllers
{
    public class CollectionsController : Controller
    {
        private ApplicationDbContext _context;

        public CollectionsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Collections
        public IActionResult Index()
        {
            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            return View(_context.Collection.ToList());
        }

        // GET: Collections/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            var model = new ViewModels.Category.CollectionDetailsViewModel();
            model.Collection = _context.Collection.Single(m => m.CollectionID == id);
            if (model.Collection == null)
            {
                return HttpNotFound();
            }

            model.Collection.Category = _context.Category.Single(t => t.CategoryID == model.Collection.CategoryID);

            model.NewSet = new Set();
            model.NewSet.CollectionID = id.Value;

            model.Collection.Sets = _context.Set.Where(t => t.CollectionID == id.Value).ToList();

            return View(model);
        }

        // GET: Collections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Collections/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Collection collection)
        {
            if (ModelState.IsValid)
            {
                _context.Collection.Add(collection);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Collection: " + collection.Name + " successfully created");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Collections",
                    action = "Details",
                    ID = collection.CollectionID
                }));
            }
            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Collections",
                action = "Details",
                ID = collection.CollectionID
            }));
        }

        // GET: Collections/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Collection collection = _context.Collection.Single(m => m.CollectionID == id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categories = new SelectList(_context.Category, "CategoryID", "Name", collection.CategoryID);
            return View(collection);
        }

        // POST: Collections/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Collection collection)
        {
            if (ModelState.IsValid)
            {
                _context.Update(collection);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Collection: " + collection.Name + " successfully edited");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Collections",
                    action = "Details",
                    ID = collection.CollectionID
                }));
            }
            return View(collection);
        }

        // GET: Collections/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Collection collection = _context.Collection.Single(m => m.CollectionID == id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            collection.Category = _context.Category.Single(t => t.CategoryID == collection.CategoryID);

            return View(collection);
        }

        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Collection collection = _context.Collection.Single(m => m.CollectionID == id);
            _context.Collection.Remove(collection);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Collection: " + collection.Name + " successfully deleted");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Collections",
                action = "Details",
                ID = collection.CollectionID
            }));
        }
    }
}
