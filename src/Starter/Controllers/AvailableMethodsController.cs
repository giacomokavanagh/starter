using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Http;

namespace Starter.Controllers
{
    public class AvailableMethodsController : Controller
    {
        private ApplicationDbContext _context;

        public AvailableMethodsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: AvailableMethods
        public IActionResult Index()
        {
            var model = new ViewAvailableMethodIndexModel();

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            model.AvailableMethods = _context.AvailableMethod.ToList();

            model.NewAvailableMethod = new AvailableMethod();

            return View(model);
        }

        // GET: AvailableMethods/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            AvailableMethod availableMethod = _context.AvailableMethod.Single(m => m.AvailableMethodID == id);
            if (availableMethod == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            availableMethod.Steps = _context.Step.Where(t => t.Method == availableMethod.Name).ToList();
            foreach(var step in availableMethod.Steps)
            {
                step.Test = _context.Test.Single(t => t.TestID == step.TestID);
            }

            var ListOfResultIDs = _context.StoredStepDetails.Where(t => t.Method == availableMethod.Name).
                Select(t => t.ResultID).ToList();
            availableMethod.Results = _context.Result.Where(t => ListOfResultIDs.Contains(t.ResultID)).ToList();
            
            var ScreenshotsFromResult = _context.StoredScreenshotDetails.Where(t => ListOfResultIDs.Contains
                (t.ResultID));

            var availableScreenshots = ScreenshotsFromResult.Where(t => t.Order == availableMethod.Steps.
                Last().Order - 1);

            if (availableScreenshots.Any())
            {
                availableMethod.StoredScreenshot = availableScreenshots.Last();
            }

            return View(availableMethod);
        }

        // GET: AvailableMethods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AvailableMethods/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AvailableMethod availableMethod)
        {
            if (ModelState.IsValid)
            {
                _context.AvailableMethod.Add(availableMethod);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Available Method: " + availableMethod.Name + " successfully created");

                return RedirectToAction("Index");
            }
            return View(availableMethod);
        }

        // GET: AvailableMethods/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            AvailableMethod availableMethod = _context.AvailableMethod.Single(m => m.AvailableMethodID == id);
            if (availableMethod == null)
            {
                return HttpNotFound();
            }
            return View(availableMethod);
        }

        // POST: AvailableMethods/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AvailableMethod availableMethod)
        {
            if (ModelState.IsValid)
            {
                _context.Update(availableMethod);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Available Method: " + availableMethod.Name + " successfully edited");

                return RedirectToAction("Index");
            }
            return View(availableMethod);
        }

        // GET: AvailableMethods/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            AvailableMethod availableMethod = _context.AvailableMethod.Single(m => m.AvailableMethodID == id);
            if (availableMethod == null)
            {
                return HttpNotFound();
            }

            return View(availableMethod);
        }

        // POST: AvailableMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            AvailableMethod availableMethod = _context.AvailableMethod.Single(m => m.AvailableMethodID == id);
            _context.AvailableMethod.Remove(availableMethod);

            HttpContext.Session.SetString("Message", "Available Method: " + availableMethod.Name + " successfully deleted");

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

    public class ViewAvailableMethodIndexModel
    {
        public IEnumerable<AvailableMethod> AvailableMethods { get; set; }
        public AvailableMethod NewAvailableMethod { get; set; }
    }
}
