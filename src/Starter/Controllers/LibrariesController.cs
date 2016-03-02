using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Authorization;

namespace Starter.Controllers
{
    [Authorize]
    public class LibrariesController : Controller
    {
        private ApplicationDbContext _context;

        public LibrariesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Libraries
        public IActionResult Index()
        {
            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            LibrariesAndNewLibrary librariesAndNewLibrary = new LibrariesAndNewLibrary();
            librariesAndNewLibrary.Library = new Library();

            librariesAndNewLibrary.Libraries = _context.Library.ToList();

            return View(librariesAndNewLibrary);
        }

        // GET: Libraries/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            LibraryAndSection libraryAndSection = new LibraryAndSection();
            libraryAndSection.Library = _context.Library.Single(m => m.LibraryID == id);
            if (libraryAndSection.Library == null)
            {
                return HttpNotFound();
            }

            libraryAndSection.Section = new Section();
            libraryAndSection.Sections = _context.Section.Where(l => l.LibraryID == id);

            return View(libraryAndSection);
        }

        // GET: Libraries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Libraries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Library library)
        {
            if (ModelState.IsValid)
            {
                _context.Library.Add(library);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Library: " + library.Name + " successfully created");

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Libraries/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Library library = _context.Library.Single(m => m.LibraryID == id);
            if (library == null)
            {
                return HttpNotFound();
            }
            return View(library);
        }

        // POST: Libraries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Library library)
        {
            if (ModelState.IsValid)
            {
                _context.Update(library);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Library: " + library.Name + " successfully edited");

                return RedirectToAction("Index");
            }
            return View(library);
        }

        // GET: Libraries/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Library library = _context.Library.Single(m => m.LibraryID == id);
            if (library == null)
            {
                return HttpNotFound();
            }

            return View(library);
        }

        // POST: Libraries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Library library = _context.Library.Single(m => m.LibraryID == id);
            _context.Library.Remove(library);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Library: " + library.Name + " successfully deleted");

            return RedirectToAction("Index");
        }
    }

    public class LibrariesAndNewLibrary
    {
        public ICollection<Library> Libraries { get; set; }
        public Library Library { get; set; }
    }

    public class LibraryAndSection
    {
        public Library Library { get; set; }
        public Section Section { get; set; }
        public IEnumerable<Section> Sections { get; set; }
    }

    public class LibraryAndSectionAndSuite
    {
        public Library Library { get; set; }
        public Section Section { get; set; }
        public Suite Suite { get; set; }
        public IEnumerable<Suite> Suites { get; set; }
    }

    public class LibraryAndSectionAndSuiteAndTest
    {
        public Library Library { get; set; }
        public Section Section { get; set; }
        public Suite Suite { get; set; }
        public Test Test { get; set; }
        public IEnumerable<Test> Tests { get; set; }
    }
}