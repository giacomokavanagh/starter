using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;

namespace Starter.Controllers
{
    public class TagsController : Controller
    {
        private ApplicationDbContext _context;

        public TagsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Tags
        public IActionResult Index()
        {
            var applicationDbContext = _context.Tag.Include(t => t.TagGroup);
            return View(applicationDbContext.ToList());
        }

        // GET: Tags/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Tag tag = _context.Tag.Single(m => m.TagID == id);
            if (tag == null)
            {
                return HttpNotFound();
            }

            return View(tag);
        }

        // GET: Tags/Create
        public IActionResult Create()
        {
            ViewData["TagGroupID"] = new SelectList(_context.Set<TagGroup>(), "TagGroupID", "TagGroup");
            return View();
        }

        // POST: Tags/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _context.Tag.Add(tag);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["TagGroupID"] = new SelectList(_context.Set<TagGroup>(), "TagGroupID", "TagGroup", tag.TagGroupID);
            return View(tag);
        }

        // GET: Tags/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Tag tag = _context.Tag.Single(m => m.TagID == id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            ViewData["TagGroupID"] = new SelectList(_context.Set<TagGroup>(), "TagGroupID", "TagGroup", tag.TagGroupID);
            return View(tag);
        }

        // POST: Tags/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _context.Update(tag);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["TagGroupID"] = new SelectList(_context.Set<TagGroup>(), "TagGroupID", "TagGroup", tag.TagGroupID);
            return View(tag);
        }

        // GET: Tags/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Tag tag = _context.Tag.Single(m => m.TagID == id);
            if (tag == null)
            {
                return HttpNotFound();
            }

            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Tag tag = _context.Tag.Single(m => m.TagID == id);
            _context.Tag.Remove(tag);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
