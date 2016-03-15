using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;

namespace Starter.Controllers
{
    public class AreasController : Controller
    {
        private ApplicationDbContext _context;

        public AreasController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Areas
        public IActionResult Index()
        {
            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            return View(_context.Area.ToList());
        }

        // GET: Areas/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            var model = new ViewModels.Platform.AreaDetailsViewModel();
            model.Area = _context.Area.Single(m => m.AreaID == id);
            if (model.Area == null)
            {
                return HttpNotFound();
            }

            model.Area.Platform = _context.Platform.Single(t => t.PlatformID == model.Area.PlatformID);

            model.NewComponent = new Component();
            model.NewComponent.AreaID = id.Value;

            model.Components = _context.Component.Where(t => t.AreaID == id.Value).ToList();
            return View(model);
        }

        // GET: Areas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Areas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Area area)
        {
            if (ModelState.IsValid)
            {
                _context.Area.Add(area);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Area: " + area.Name + " successfully created");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Areas",
                    action = "Details",
                    ID = area.AreaID
                }));
            }
            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Areas",
                action = "Details",
                ID = area.AreaID
            }));
        }

        // GET: Areas/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Area area = _context.Area.Single(m => m.AreaID == id);
            if (area == null)
            {
                return HttpNotFound();
            }
            ViewBag.Platforms = new SelectList(_context.Platform, "PlatformID", "Name", area.PlatformID);
            return View(area);
        }

        // POST: Areas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Area area)
        {
            if (ModelState.IsValid)
            {
                _context.Update(area);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Area: " + area.Name + " successfully edited");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Areas",
                    action = "Details",
                    ID = area.AreaID
                }));
            }
            return View(area);
        }

        // GET: Areas/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Area area = _context.Area.Single(m => m.AreaID == id);
            if (area == null)
            {
                return HttpNotFound();
            }

            return View(area);
        }

        // POST: Areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Area area = _context.Area.Single(m => m.AreaID == id);
            _context.Area.Remove(area);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Area: " + area.Name + " successfully deleted");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Areas",
                action = "Details",
                ID = area.AreaID
            }));
        }
    }
}
