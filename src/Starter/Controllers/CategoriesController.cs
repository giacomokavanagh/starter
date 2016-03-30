using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using Microsoft.AspNet.Http;
using Starter.ViewModels.Category;
using Microsoft.AspNet.Routing;

namespace Starter.Controllers
{
    public class CategoriesController : Controller
    {
        private ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Categories
        public IActionResult Index()
        {
            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            var model = new CategoryIndexViewModel();
            model.NewCategory = new Category();
            model.Categories = _context.Category.ToList();

            return View(model);
        }

        // GET: Categories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            var model = new CategoryDetailsViewModel();
            model.Category = _context.Category.Single(m => m.CategoryID == id);

            if (model.Category == null)
            {
                return HttpNotFound();
            }

            model.NewCollection = new Collection();
            model.NewCollection.CategoryID = id.Value;
            model.Category.Collections = _context.Collection.Where(l => l.CollectionID == id).ToList();

            return View(model);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Category.Add(category);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Category: " + category.Name + " successfully created");

                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Category category = _context.Category.Single(m => m.CategoryID == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Update(category);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Category: " + category.Name + " successfully edited");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Categories",
                    action = "Details",
                    ID = category.CategoryID
                }));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Category category = _context.Category.Single(m => m.CategoryID == id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Category category = _context.Category.Single(m => m.CategoryID == id);
            _context.Category.Remove(category);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Category: " + category.Name + " successfully deleted");

            return RedirectToAction("Index");
        }
    }
}
