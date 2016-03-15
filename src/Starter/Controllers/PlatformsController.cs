using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using Microsoft.AspNet.Http;
using Starter.ViewModels.Platform;
using Microsoft.AspNet.Routing;

namespace Starter.Controllers
{
    public class PlatformsController : Controller
    {
        private ApplicationDbContext _context;

        public PlatformsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Platforms
        public IActionResult Index()
        {
            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            var model = new PlatformIndexViewModel();
            model.NewPlatform = new Platform();
            model.Platforms = _context.Platform.ToList();

            return View(model);
        }

        // GET: Platforms/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            var model = new PlatformDetailsViewModel();
            model.Platform = _context.Platform.Single(m => m.PlatformID == id);

            if (model.Platform == null)
            {
                return HttpNotFound();
            }

            model.NewArea = new Area();
            model.NewArea.PlatformID = id.Value;
            model.Areas = _context.Area.Where(l => l.PlatformID == id).ToList();

            return View(model);
        }

        // GET: Platforms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Platforms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Platform platform)
        {
            if (ModelState.IsValid)
            {
                _context.Platform.Add(platform);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Platform: " + platform.Name + " successfully created");

                return RedirectToAction("Index");
            }
            return View(platform);
        }

        // GET: Platforms/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Platform platform = _context.Platform.Single(m => m.PlatformID == id);
            if (platform == null)
            {
                return HttpNotFound();
            }
            return View(platform);
        }

        // POST: Platforms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Platform platform)
        {
            if (ModelState.IsValid)
            {
                _context.Update(platform);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Platform: " + platform.Name + " successfully edited");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Platforms",
                    action = "Details",
                    ID = platform.PlatformID
                }));
            }
            return View(platform);
        }

        // GET: Platforms/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Platform platform = _context.Platform.Single(m => m.PlatformID == id);
            if (platform == null)
            {
                return HttpNotFound();
            }

            return View(platform);
        }

        // POST: Platforms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Platform platform = _context.Platform.Single(m => m.PlatformID == id);
            _context.Platform.Remove(platform);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Platform: " + platform.Name + " successfully deleted");

            return RedirectToAction("Index");
        }
    }
}
