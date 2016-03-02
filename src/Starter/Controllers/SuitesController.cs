using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using Microsoft.AspNet.Routing;
using System.Collections.Generic;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Authorization;

namespace Starter.Controllers
{
    [Authorize]
    public class SuitesController : Controller
    {
        private ApplicationDbContext _context;

        public SuitesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Suites
        public IActionResult Index()
        {
            var applicationDbContext = _context.Suite.Include(s => s.Section);
            return View(applicationDbContext.ToList());
        }

        // GET: Suites/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            LibraryAndSectionAndSuiteAndTest libraryAndSectionAndSuiteAndTest = new LibraryAndSectionAndSuiteAndTest();
            libraryAndSectionAndSuiteAndTest.Suite = _context.Suite.Single(m => m.SuiteID == id);
            if (libraryAndSectionAndSuiteAndTest.Suite == null)
            {
                return HttpNotFound();
            }

            int intSectionID = libraryAndSectionAndSuiteAndTest.Suite.SectionID;
            libraryAndSectionAndSuiteAndTest.Section = _context.Section.Single(m => m.SectionID == intSectionID);

            int intLibraryID = libraryAndSectionAndSuiteAndTest.Section.LibraryID;
            libraryAndSectionAndSuiteAndTest.Library = _context.Library.Single(m => m.LibraryID == intLibraryID);

            libraryAndSectionAndSuiteAndTest.Test = new Test();
            libraryAndSectionAndSuiteAndTest.Tests = _context.Test.Where(l => l.SuiteID == id);

            return View(libraryAndSectionAndSuiteAndTest);
        }

        // GET: Suites/Create
        public IActionResult Create()
        {
            ViewData["SectionID"] = new SelectList(_context.Section, "SectionID", "Section");
            return View();
        }

        // POST: Suites/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Suite suite)
        {
            if (ModelState.IsValid)
            {
                _context.Suite.Add(suite);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Suite: " + suite.Name + " successfully created");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Sections",
                    action = "Details",
                    ID = suite.SectionID
                }));
            }
            return View(suite);
        }

        // GET: Suites/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            LibraryAndSectionAndSuite libraryAndSectionAndSuite = new LibraryAndSectionAndSuite();
            libraryAndSectionAndSuite.Suite = _context.Suite.Single(m => m.SuiteID == id);
            if (libraryAndSectionAndSuite.Suite == null)
            {
                return HttpNotFound();
            }

            libraryAndSectionAndSuite.Section = _context.Section.Single
                (m => m.SectionID == libraryAndSectionAndSuite.Suite.SectionID);

            ViewBag.Sections = new SelectList(_context.Section, "SectionID", "Name", libraryAndSectionAndSuite.Suite.SectionID);
            
            return View(libraryAndSectionAndSuite);
        }

        // POST: Suites/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Suite suite)
        {
            if (ModelState.IsValid)
            {
                _context.Update(suite);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Suite: " + suite.Name + " successfully edited");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Section",
                    action = "Details",
                    ID = suite.SectionID
                }));
            }
            ViewData["SectionID"] = new SelectList(_context.Project, "ID", "Section", suite.SectionID);
            return View(suite);
        }

        // GET: Suites/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            LibraryAndSectionAndSuite libraryAndSectionAndSuite = new LibraryAndSectionAndSuite();
            libraryAndSectionAndSuite.Suite = _context.Suite.Single(m => m.SuiteID == id);
            if (libraryAndSectionAndSuite.Suite == null)
            {
                return HttpNotFound();
            }

            int intSectionID = libraryAndSectionAndSuite.Suite.SectionID;
            libraryAndSectionAndSuite.Section = _context.Section.Single(m => m.SectionID == intSectionID);

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Sections",
                action = "Details",
                ID = libraryAndSectionAndSuite.Suite.SectionID
            }));
        }

        // POST: Suites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Suite suite = _context.Suite.Single(m => m.SuiteID == id);
            _context.Suite.Remove(suite);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Suite: " + suite.Name + " successfully deleted");

            return RedirectToAction("Index");
        }
    }
}
