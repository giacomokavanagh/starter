using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;

namespace Starter.Controllers
{
    public class PageObjectsController : Controller
    {
        private ApplicationDbContext _context;

        public PageObjectsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: PageObjects
        public IActionResult Index()
        {
            var applicationDbContext = _context.PageObject.Include(p => p.ObjectLibrary);
            return View(applicationDbContext.ToList());
        }

        // GET: PageObjects/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            PageObject pageObject = _context.PageObject.Single(m => m.PageObjectID == id);
            if (pageObject == null)
            {
                return HttpNotFound();
            }

            return View(pageObject);
        }

        // GET: PageObjects/Create
        public IActionResult Create()
        {
            ViewData["ObjectLibraryID"] = new SelectList(_context.Set<ObjectLibrary>(), "ObjectLibraryID", "ObjectLibrary");
            return View();
        }

        // POST: PageObjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PageObject pageObject)
        {
            if (ModelState.IsValid)
            {
                _context.PageObject.Add(pageObject);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ObjectLibraryID"] = new SelectList(_context.Set<ObjectLibrary>(), "ObjectLibraryID", "ObjectLibrary", pageObject.ObjectLibraryID);
            return View(pageObject);
        }

        // GET: PageObjects/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            PageObject pageObject = _context.PageObject.Single(m => m.PageObjectID == id);
            if (pageObject == null)
            {
                return HttpNotFound();
            }
            ViewData["ObjectLibraryID"] = new SelectList(_context.Set<ObjectLibrary>(), "ObjectLibraryID", "ObjectLibrary", pageObject.ObjectLibraryID);
            return View(pageObject);
        }

        // POST: PageObjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PageObject pageObject)
        {
            if (ModelState.IsValid)
            {
                _context.Update(pageObject);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ObjectLibraryID"] = new SelectList(_context.Set<ObjectLibrary>(), "ObjectLibraryID", "ObjectLibrary", pageObject.ObjectLibraryID);
            return View(pageObject);
        }

        // GET: PageObjects/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            PageObject pageObject = _context.PageObject.Single(m => m.PageObjectID == id);
            if (pageObject == null)
            {
                return HttpNotFound();
            }

            return View(pageObject);
        }

        // POST: PageObjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            PageObject pageObject = _context.PageObject.Single(m => m.PageObjectID == id);
            _context.PageObject.Remove(pageObject);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
