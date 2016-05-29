using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Authorization;
using Starter.ViewModels.PageObject;
using Microsoft.AspNet.Routing;

namespace Starter.Controllers
{
    public class ObjectLibrariesController : Controller
    {
        private ApplicationDbContext _context;

        public ObjectLibrariesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        [Authorize]
        // GET: ObjectLibraries/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            var model = new ObjectLibraryDetailsViewModel();
            model.ObjectLibrary = _context.ObjectLibrary.Single(t => t.ObjectLibraryID == id);

            if (model.ObjectLibrary == null)
            {
                return HttpNotFound();
            }

            model.ObjectLibrary.TagLinks = _context.TagLink.Where(t => t.ObjectLibraryID == id).ToList();
            foreach(var item in model.ObjectLibrary.TagLinks)
            {
                item.Tag = _context.Tag.Single(t => t.TagID == item.TagID);
            }

            model.PageObjects = _context.PageObject.Where(t => t.ObjectLibraryID == id).ToList();
            foreach(var item in model.PageObjects)
            {
                item.TagLinks = _context.TagLink.Where(t => t.PageObjectID == item.PageObjectID).ToList();
                foreach(var tagLink in item.TagLinks)
                {
                    tagLink.Tag = _context.Tag.Single(t => t.TagID == tagLink.TagID);
                }
            }

            model.NewPageObject = new PageObject();
            model.NewPageObject.ObjectLibraryID = id.Value;

            model.ObjectLibrary.GenericFolder = _context.GenericFolder.Single
                (t => t.GenericFolderID == model.ObjectLibrary.GenericFolderID);

            ViewBag.AvailableMethods = new SelectList(_context.AvailableMethod, "Name", "Name");

            return View(model);
        }

        [Authorize]
        // GET: ObjectLibraries/Create
        public IActionResult Create(int? id)
        {
            ViewData["GenericFolderID"] = new SelectList(_context.GenericFolder, "GenericFolderID", "Folder", id.Value);
            var model = new ObjectLibrary();
            model.GenericFolder = _context.GenericFolder.Single(t => t.GenericFolderID == id);
            return View(model);
        }

        // POST: ObjectLibraries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(ObjectLibrary objectLibrary)
        {
            if (ModelState.IsValid)
            {
                _context.ObjectLibrary.Add(objectLibrary);
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Object Library: " + objectLibrary.Name + " successfully created");
                return redirectToDetails(objectLibrary.ObjectLibraryID);
            }
            ViewData["GenericFolderID"] = new SelectList(_context.GenericFolder, "GenericFolderID", "GenericFolder", objectLibrary.GenericFolderID);
            return View(objectLibrary);
        }

        [Authorize]
        // GET: ObjectLibraries/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var objectLibrary = _context.ObjectLibrary.Single(m => m.ObjectLibraryID == id);
            if (objectLibrary == null)
            {
                return HttpNotFound();
            }
            ViewData["GenericFolderID"] = new SelectList(_context.GenericFolder, "GenericFolderID", "GenericFolder", objectLibrary.GenericFolderID);
            return View(objectLibrary);
        }

        // POST: ObjectLibraries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(ObjectLibrary objectLibrary)
        {
            if (ModelState.IsValid)
            {
                _context.Update(objectLibrary);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Object Library: " + objectLibrary.Name + " successfully edited");
                return redirectToDetails(objectLibrary.ObjectLibraryID);
            }
            ViewData["GenericFolderID"] = new SelectList(_context.GenericFolder, "GenericFolderID", "GenericFolder", objectLibrary.GenericFolderID);
            return View(objectLibrary);
        }

        // GET: ObjectLibraries/Delete/5
        [Authorize]
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ObjectLibrary objectLibrary = _context.ObjectLibrary.Single(m => m.ObjectLibraryID == id);
            if (objectLibrary == null)
            {
                return HttpNotFound();
            }

            return View(objectLibrary);
        }

        // POST: ObjectLibraries/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            ObjectLibrary objectLibrary = _context.ObjectLibrary.Single(m => m.ObjectLibraryID == id);
            _context.ObjectLibrary.Remove(objectLibrary);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Object Library: " + objectLibrary.Name + " successfully deleted");
            return RedirectToAction("Index");
        }

        private IActionResult redirectToDetails(int objectLibraryID)
        {
            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Projects",
                action = "Details",
                ID = objectLibraryID
            }));
        }

    }
}