using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;

namespace Starter.Controllers
{
    public class SetsController : Controller
    {
        private ApplicationDbContext _context;

        public SetsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Sets
        public IActionResult Index()
        {
            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            return View(_context.Component.ToList());
        }

        // GET: Sets/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            var model = new ViewModels.Category.SetDetailsViewModel();
            model.Set = _context.Set.Single(t => t.SetID == id);
            if (model.Set == null)
            {
                return HttpNotFound();
            }

            model.Set.Collection = _context.Collection.Single(m => m.CollectionID == model.Set.CollectionID);
            model.Set.Collection.Category = _context.Category.Single(t => t.CategoryID ==
                model.Set.Collection.CategoryID);

            model.NewProcedure = new Procedure();
            model.NewProcedure.SetID = id.Value;

            model.Set.Procedures = _context.Procedure.Where(t => t.SetID == id.Value).ToList();

            return View(model);
        }

        // GET: Sets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Set set)
        {
            if (ModelState.IsValid)
            {
                _context.Set.Add(set);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Set: " + set.Name + " successfully created");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Sets",
                    action = "Details",
                    ID = set.SetID
                }));
            }
            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Sets",
                action = "Details",
                ID = set.SetID
            }));
        }

        // GET: Sets/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Set set = _context.Set.Single(m => m.SetID == id);
            if (set == null)
            {
                return HttpNotFound();
            }
            ViewBag.Collections = new SelectList(_context.Collection, "CollectionID", "Name", set.CollectionID);
            return View(set);
        }

        // POST: Sets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Set set)
        {
            if (ModelState.IsValid)
            {
                _context.Update(set);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Set: " + set.Name + " successfully edited");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Sets",
                    action = "Details",
                    ID = set.SetID
                }));
            }
            return View(set);
        }

        // GET: Sets/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Set set = _context.Set.Single(m => m.SetID == id);
            if (set == null)
            {
                return HttpNotFound();
            }

            return View(set);
        }

        // POST: Sets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Set set = _context.Set.Single(m => m.SetID == id);
            _context.Set.Remove(set);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Set: " + set.Name + " successfully deleted");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Collections",
                action = "Details",
                ID = set.CollectionID
            }));
        }
    }
}
