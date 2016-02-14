using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Http;

namespace Starter.Controllers
{
    public class DependencyGroupsController : Controller
    {
        private ApplicationDbContext _context;

        public DependencyGroupsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: DependencyGroups
        public IActionResult Index()
        {
            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            DependencyGroupsAndNewDependencyGroup dependencyGroupsAndNewDependencyGroup = new DependencyGroupsAndNewDependencyGroup();
            dependencyGroupsAndNewDependencyGroup.DependencyGroups = _context.DependencyGroup.ToList();
            return View(dependencyGroupsAndNewDependencyGroup);
        }

        // GET: DependencyGroups/Details/5
        public IActionResult Details(int? id)
        {
            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            if (id == null)
            {
                return HttpNotFound();
            }
            DependencyGroupAndNewDependency dependencyGroupAndNewDependency = new DependencyGroupAndNewDependency();
            dependencyGroupAndNewDependency.DependencyGroup = _context.DependencyGroup.Single(m => m.DependencyGroupID == id);

            if (dependencyGroupAndNewDependency.DependencyGroup == null)
            {
                return HttpNotFound();
            }

            dependencyGroupAndNewDependency.DependencyGroup.Dependencies = _context.Dependency.Where(t => t.DependencyGroupID == id).ToList();

            foreach (var dpg in dependencyGroupAndNewDependency.DependencyGroup.Dependencies)
            {
                dpg.TestRun = _context.TestRun.SingleOrDefault(t => t.TestRunID == dpg.TestRunID);
                dpg.TestRun.Test = _context.Test.SingleOrDefault(t => t.TestID == dpg.TestRun.TestID);
            }

            ViewData["DependencyGroupID"] = id;

            var TestRuns = new List<SelectListItem>();
            
            foreach(var testRun in _context.TestRun)
            {
                var testName = _context.Test.SingleOrDefault(t => t.TestID == testRun.TestID).Name;
                var runName = _context.Run.SingleOrDefault(t => t.RunID == testRun.RunID).Name;
                var text = runName + "=>" + testName + "=>" + testRun.TestRunID.ToString();
                var SelectListItem = new SelectListItem() { Value = testRun.TestRunID.ToString(), Text = text };
                TestRuns.Add(SelectListItem);
            }

            ViewBag.TestRuns = new SelectList(TestRuns, "Value", "Text");
            ViewBag.browsers = new SelectList(new List<string> { "Chrome", "Firefox", "IE" });

            return View(dependencyGroupAndNewDependency);
        }

        // GET: DependencyGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DependencyGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DependencyGroup dependencyGroup)
        {
            if (ModelState.IsValid)
            {
                _context.DependencyGroup.Add(dependencyGroup);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Dependency Group: " + dependencyGroup.Name + " successfully created");

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: DependencyGroups/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            DependencyGroup dependencyGroup = _context.DependencyGroup.Single(m => m.DependencyGroupID == id);
            if (dependencyGroup == null)
            {
                return HttpNotFound();
            }
            return View(dependencyGroup);
        }

        // POST: DependencyGroups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DependencyGroup dependencyGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Update(dependencyGroup);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Dependency Group: " + dependencyGroup.Name + " successfully edited");
                return RedirectToAction("Index");
            }
            return View(dependencyGroup);
        }

        // GET: DependencyGroups/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            DependencyGroup dependencyGroup = _context.DependencyGroup.Single(m => m.DependencyGroupID == id);
            if (dependencyGroup == null)
            {
                return HttpNotFound();
            }

            return View(dependencyGroup);
        }

        // POST: DependencyGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            DependencyGroup dependencyGroup = _context.DependencyGroup.Single(m => m.DependencyGroupID == id);
            _context.DependencyGroup.Remove(dependencyGroup);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Dependency Group: " + dependencyGroup.Name + " successfully deleted");
            return RedirectToAction("Index");
        }
    }

    public class DependencyGroupsAndNewDependencyGroup
    {
        public IEnumerable<DependencyGroup> DependencyGroups { get; set; } 
        public DependencyGroup DependencyGroup { get; set; }
    }

    public class DependencyGroupAndNewDependency
    {
        public DependencyGroup DependencyGroup { get; set; }
        public Dependency Dependency { get; set; }
    }

}
