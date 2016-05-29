using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;

namespace Starter.Controllers
{
    public class TagLinksController : Controller
    {
        private ApplicationDbContext _context;

        public TagLinksController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: TagLinks
        public IActionResult Index()
        {
            var applicationDbContext = _context.TagLink.Include(t => t.ObjectLibrary).Include(t => t.PageObject).Include(t => t.Tag);
            return View(applicationDbContext.ToList());
        }

        // GET: TagLinks/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TagLink tagLink = _context.TagLink.Single(m => m.TagLinkID == id);
            if (tagLink == null)
            {
                return HttpNotFound();
            }

            return View(tagLink);
        }

        // GET: TagLinks/Create
        public IActionResult Create()
        {
            ViewData["ObjectLibraryID"] = new SelectList(_context.ObjectLibrary, "ObjectLibraryID", "ObjectLibrary");
            ViewData["PageObjectID"] = new SelectList(_context.PageObject, "PageObjectID", "PageObject");
            ViewData["TagID"] = new SelectList(_context.Tag, "TagID", "Tag");
            return View();
        }

        // POST: TagLinks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TagLink tagLink)
        {
            if (ModelState.IsValid)
            {
                _context.TagLink.Add(tagLink);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ObjectLibraryID"] = new SelectList(_context.ObjectLibrary, "ObjectLibraryID", "ObjectLibrary", tagLink.ObjectLibraryID);
            ViewData["PageObjectID"] = new SelectList(_context.PageObject, "PageObjectID", "PageObject", tagLink.PageObjectID);
            ViewData["TagID"] = new SelectList(_context.Tag, "TagID", "Tag", tagLink.TagID);
            return View(tagLink);
        }

        // GET: TagLinks/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TagLink tagLink = _context.TagLink.Single(m => m.TagLinkID == id);
            if (tagLink == null)
            {
                return HttpNotFound();
            }
            ViewData["ObjectLibraryID"] = new SelectList(_context.ObjectLibrary, "ObjectLibraryID", "ObjectLibrary", tagLink.ObjectLibraryID);
            ViewData["PageObjectID"] = new SelectList(_context.PageObject, "PageObjectID", "PageObject", tagLink.PageObjectID);
            ViewData["TagID"] = new SelectList(_context.Tag, "TagID", "Tag", tagLink.TagID);
            return View(tagLink);
        }

        // POST: TagLinks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TagLink tagLink)
        {
            if (ModelState.IsValid)
            {
                _context.Update(tagLink);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ObjectLibraryID"] = new SelectList(_context.ObjectLibrary, "ObjectLibraryID", "ObjectLibrary", tagLink.ObjectLibraryID);
            ViewData["PageObjectID"] = new SelectList(_context.PageObject, "PageObjectID", "PageObject", tagLink.PageObjectID);
            ViewData["TagID"] = new SelectList(_context.Tag, "TagID", "Tag", tagLink.TagID);
            return View(tagLink);
        }

        // GET: TagLinks/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TagLink tagLink = _context.TagLink.Single(m => m.TagLinkID == id);
            if (tagLink == null)
            {
                return HttpNotFound();
            }

            return View(tagLink);
        }

        // POST: TagLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            TagLink tagLink = _context.TagLink.Single(m => m.TagLinkID == id);
            _context.TagLink.Remove(tagLink);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
