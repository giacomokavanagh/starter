using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;

namespace Starter.Controllers
{
    public class ComponentsController : Controller
    {
        private ApplicationDbContext _context;

        public ComponentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Components
        public IActionResult Index()
        {
            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            return View(_context.Component.ToList());
        }

        // GET: Components/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            var model = new ViewModels.Platform.ComponentDetailsViewModel();
            model.Component = _context.Component.Single(t => t.ComponentID == id);
            if (model.Component == null)
            {
                return HttpNotFound();
            }

            model.Component.Area = _context.Area.Single(m => m.AreaID == model.Component.AreaID);
            model.Component.Area.Platform = _context.Platform.Single(t => t.PlatformID == 
                model.Component.Area.PlatformID);


            model.NewProcess = new Process();
            model.NewProcess.ComponentID = id.Value;

            model.Processes = _context.Process.Where(t => t.ComponentID == id.Value).ToList();
            return View(model);
        }

        // GET: Components/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Components/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Component component)
        {
            if (ModelState.IsValid)
            {
                _context.Component.Add(component);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Component: " + component.Name + " successfully created");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Components",
                    action = "Details",
                    ID = component.ComponentID
                }));
            }
            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Components",
                action = "Details",
                ID = component.ComponentID
            }));
        }

        // GET: Components/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Component component = _context.Component.Single(m => m.ComponentID == id);
            if (component == null)
            {
                return HttpNotFound();
            }
            ViewBag.Areas = new SelectList(_context.Area, "AreaID", "Name", component.AreaID);
            return View(component);
        }

        // POST: Components/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Component component)
        {
            if (ModelState.IsValid)
            {
                _context.Update(component);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Component: " + component.Name + " successfully edited");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Components",
                    action = "Details",
                    ID = component.ComponentID
                }));
            }
            return View(component);
        }

        // GET: Components/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Component component = _context.Component.Single(m => m.ComponentID == id);
            if (component == null)
            {
                return HttpNotFound();
            }

            return View(component);
        }

        // POST: Components/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Component component = _context.Component.Single(m => m.ComponentID == id);
            _context.Component.Remove(component);

            HttpContext.Session.SetString("Message", "Component: " + component.Name + " successfully deleted");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Components",
                action = "Details",
                ID = component.ComponentID
            }));
        }
    }
}
