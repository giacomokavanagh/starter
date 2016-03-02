using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Authorization;

namespace Starter.Controllers
{
    [Authorize]
    public class DependenciesController : Controller
    {
        private ApplicationDbContext _context;

        public DependenciesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Dependencies
        public IActionResult Index()
        {
            var applicationDbContext = _context.Dependency.Include(d => d.DependencyGroup).Include(d => d.TestRun);
            return View(applicationDbContext.ToList());
        }

        // GET: Dependencies/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Dependency dependency = _context.Dependency.Single(m => m.DependencyID == id);
            if (dependency == null)
            {
                return HttpNotFound();
            }

            return View(dependency);
        }

        // GET: Dependencies/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Dependencies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection form)
        {
            var DependencyGroupID = Convert.ToInt32(form["id"]);

            List<string> usersListOfTestRuns = form["TestRunID"].ToList();

            string CreationMessage = "Dependencies created for" + System.Environment.NewLine;

            foreach(var TestRunID in usersListOfTestRuns)
            {
                Dependency dependency = new Dependency();
                DependencyGroup dependencyGroup = _context.DependencyGroup.SingleOrDefault
                    (t => t.DependencyGroupID == DependencyGroupID);

                dependency.DependencyGroupID = DependencyGroupID;
                dependency.TestRunID = Convert.ToInt32(TestRunID);

                CreationMessage = CreationMessage + "Dependency Group: " + dependencyGroup.Name + " & TestRunID: "
                    + TestRunID;

                _context.Dependency.Add(dependency);
            }

            _context.SaveChanges();

            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("Message", CreationMessage);

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "DependencyGroups",
                    action = "Details",
                    ID = DependencyGroupID
                }));
            }

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "DependencyGroups",
                action = "Details",
                ID = DependencyGroupID
            }));
        }

        // GET: Dependencies/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Dependency dependency = _context.Dependency.Single(m => m.DependencyID == id);
            if (dependency == null)
            {
                return HttpNotFound();
            }
            ViewData["DependencyGroupID"] = new SelectList(_context.DependencyGroup, "DependencyGroupID", "DependencyGroup", dependency.DependencyGroupID);
            ViewData["TestRunID"] = new SelectList(_context.TestRun, "TestRunID", "TestRun", dependency.TestRunID);
            return View(dependency);
        }

        // POST: Dependencies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Dependency dependency)
        {
            if (ModelState.IsValid)
            {
                _context.Update(dependency);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dependency);
        }

        // GET: Dependencies/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Dependency dependency = _context.Dependency.Single(m => m.DependencyID == id);
            if (dependency == null)
            {
                return HttpNotFound();
            }

            return View(dependency);
        }

        // POST: Dependencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Dependency dependency = _context.Dependency.Single(m => m.DependencyID == id);
            _context.Dependency.Remove(dependency);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
