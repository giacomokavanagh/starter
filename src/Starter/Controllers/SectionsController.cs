using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Http;

namespace Starter.Controllers
{
    public class SectionsController : Controller
    {
        private ApplicationDbContext _context;

        public SectionsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Sections
        public IActionResult Index()
        {
            var applicationDbContext = _context.Section.Include(s => s.Library);
            return View(applicationDbContext.ToList());
        }

        // GET: Sections/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            LibraryAndSectionAndSuite libraryAndSectionAndSuite = new LibraryAndSectionAndSuite();
            libraryAndSectionAndSuite.Section = _context.Section.Single(m => m.SectionID == id);
            if (libraryAndSectionAndSuite.Section == null)
            {
                return HttpNotFound();
            }

            int intLibraryID = libraryAndSectionAndSuite.Section.LibraryID;
            libraryAndSectionAndSuite.Library = _context.Library.Single(m => m.LibraryID == intLibraryID);

            libraryAndSectionAndSuite.Suite = new Suite();

            libraryAndSectionAndSuite.Suites = _context.Suite.Where(l => l.SectionID == id);

            return View(libraryAndSectionAndSuite);
        }

        // GET: Sections/Create
        public IActionResult Create()
        {
            ViewData["LibraryID"] = new SelectList(_context.Library, "LibraryID", "Library");
            return View();
        }

        // POST: Sections/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Section section)
        {
            if (ModelState.IsValid)
            {
                _context.Section.Add(section);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Section: " + section.Name + " successfully created");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Libraries",
                    action = "Details",
                    ID = section.LibraryID
                }));
            }

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Libraries",
                action = "Details",
                ID = section.LibraryID
            }));
        }

        // GET: Sections/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            LibraryAndSection libraryAndSection = new LibraryAndSection();
            libraryAndSection.Section = _context.Section.Single(m => m.SectionID == id);
            if (libraryAndSection.Section == null)
            {
                return HttpNotFound();
            }

            libraryAndSection.Library = _context.Library.Single(m => m.LibraryID == libraryAndSection.Section.LibraryID);

            ViewBag.Libraries = new SelectList(_context.Library, "LibraryID", "Name", libraryAndSection.Section.LibraryID);
            
            return View(libraryAndSection);
        }

        // POST: Sections/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Section section)
        {
            if (ModelState.IsValid)
            {
                _context.Update(section);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Section: " + section.Name + " successfully edited");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Libraries",
                    action = "Details",
                    ID = section.LibraryID
                }));
            }

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Libraries",
                action = "Details",
                ID = section.LibraryID
            }));
        }

        // GET: Sections/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            LibraryAndSection libraryAndSection = new LibraryAndSection();
            libraryAndSection.Section = _context.Section.Single(m => m.SectionID == id);
            if (libraryAndSection.Section == null)
            {
                return HttpNotFound();
            }

            int intLibraryID = libraryAndSection.Section.LibraryID;
            libraryAndSection.Library = _context.Library.Single(m => m.LibraryID == intLibraryID);

            return View(libraryAndSection);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Section section = _context.Section.Single(m => m.SectionID == id);
            _context.Section.Remove(section);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Section: " + section.Name + " successfully deleted");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Libraries",
                action = "Details",
                ID = section.LibraryID
            }));
        }
    }
}
