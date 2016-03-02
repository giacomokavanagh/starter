using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Http;
using System;
using Microsoft.CodeAnalysis;
using Microsoft.AspNet.Authorization;

namespace Starter.Controllers
{
    [Authorize]
    public class TestRunnerGroupsController : Controller
    {
        private ApplicationDbContext _context;

        public TestRunnerGroupsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: TestRunnerGroups
        public IActionResult Index()
        {
            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            var trg = new TestRunnerGroupsAndNewTestRunnerGroupAndProjectsAndFoldersAndGroupsAndRuns();

            trg.TestRunnerGroups = _context.TestRunnerGroup;
            trg.TestRunnerGroup = new TestRunnerGroup();
            trg.Projects = _context.Project;
            trg.Folders = _context.Folder;
            trg.Groups = _context.Group;
            trg.Runs = _context.Run;

            var BlankSelectListItem = new SelectListItem { Text = "", Value = null };

            var projects = new SelectList(_context.Project, "ID", "Name").ToList();
            projects.Insert(0, BlankSelectListItem);
            ViewBag.Projects = projects;

            var folders = new SelectList(_context.Folder, "FolderID", "Name").ToList();
            folders.Insert(0, BlankSelectListItem);
            ViewBag.Folders = folders;

            var groups = new SelectList(_context.Group, "GroupID", "Name").ToList();
            groups.Insert(0, BlankSelectListItem);
            ViewBag.Groups = groups;

            var runs = new SelectList(_context.Run, "RunID", "Name").ToList();
            runs.Insert(0, BlankSelectListItem);
            ViewBag.Runs = runs;

            return View(trg);
        }

        // GET: TestRunnerGroups/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            var trg = new TestRunnerGroupAndTestRunnersAndNewTestRunner();
            trg.TestRunnerGroup = _context.TestRunnerGroup.Single(m => m.TestRunnerGroupID == id);
            if (trg == null)
            {
                return HttpNotFound();
            }

            trg.TestRunners = _context.TestRunner.Where(t => t.TestRunnerGroupID == id);

            trg.TestRunner = new TestRunner();

            var projects = _context.Project.Where(t => t.TestRunnerGroupID == id).ToList();
            if (projects.Count != 0)
            {
                ViewBag.ProjectsDisplayString = String.Join(", ", projects.Select(t => t.Name).ToList());
            }

            var folders = _context.Folder.Where(t => t.TestRunnerGroupID == id).ToList();
            if (folders.Count != 0)
            {
                ViewBag.FoldersDisplayString = String.Join(", ", folders.Select(t => t.Name).ToList());
            }

            var groups = _context.Group.Where(t => t.TestRunnerGroupID == id).ToList();
            if (groups.Count != 0)
            {
                ViewBag.GroupsDisplayString = String.Join(", ", groups.Select(t => t.Name).ToList());
            }

            var runs = _context.Run.Where(t => t.TestRunnerGroupID == id).ToList();
            if (runs.Count != 0)
            {
                ViewBag.RunsDisplayString = String.Join(", ", runs.Select(t => t.Name).ToList());
            }

            return View(trg);
        }

        // GET: TestRunnerGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TestRunnerGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TestRunnerGroup testRunnerGroup, IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                _context.TestRunnerGroup.Add(testRunnerGroup);
                _context.SaveChanges();
                _context.Update(testRunnerGroup);

                foreach (var projectID in form["Projects"])
                {
                    var project = _context.Project.Single(t => t.ID == Convert.ToInt32(projectID));
                    _context.Update(project);
                    project.TestRunnerGroupID = testRunnerGroup.TestRunnerGroupID;
                    _context.SaveChanges();
                }

                foreach (var folderID in form["Folders"])
                {
                    var folder = _context.Folder.Single(t => t.FolderID == Convert.ToInt32(folderID));
                    _context.Update(folder);
                    folder.TestRunnerGroupID = testRunnerGroup.TestRunnerGroupID;
                    _context.SaveChanges();
                }

                foreach (var groupID in form["Groups"])
                {
                    var group = _context.Group.Single(t => t.GroupID == Convert.ToInt32(groupID));
                    _context.Update(group);
                    group.TestRunnerGroupID = testRunnerGroup.TestRunnerGroupID;
                    _context.SaveChanges();
                }

                foreach (var runID in form["Runs"])
                {
                    var run = _context.Run.Single(t => t.GroupID == Convert.ToInt32(runID));
                    _context.Update(run);
                    run.TestRunnerGroupID = testRunnerGroup.TestRunnerGroupID;
                    _context.SaveChanges();
                }

                HttpContext.Session.SetString("Message", "Test Runner Group: " + testRunnerGroup.Name + " successfully created");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "TestRunnerGroups",
                    action = "Details",
                    ID = testRunnerGroup.TestRunnerGroupID
                }));
            }

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "TestRunnerGroups",
                action = "Details",
                ID = testRunnerGroup.TestRunnerGroupID
            }));
        }

        // GET: TestRunnerGroups/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TestRunnerGroup testRunnerGroup = _context.TestRunnerGroup.Single(m => m.TestRunnerGroupID == id);
            if (testRunnerGroup == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.AllProjects = _context.Project.ToList();
            ViewBag.SelectedProjectIDs = _context.Project.Where(t => t.TestRunnerGroupID == id).Select(t => t.ID).ToList();

            ViewBag.AllFolders = _context.Folder.ToList();
            ViewBag.SelectedFolderIDs = _context.Folder.Where(t => t.TestRunnerGroupID == id).Select(t => t.FolderID).ToList();

            ViewBag.AllGroups = _context.Group.ToList();
            ViewBag.SelectedGroupIDs = _context.Group.Where(t => t.TestRunnerGroupID == id).Select(t => t.GroupID).ToList();

            ViewBag.AllRuns = _context.Run.ToList();
            ViewBag.SelectedRunIDs = _context.Run.Where(t => t.TestRunnerGroupID == id).Select(t => t.RunID).ToList();

            return View(testRunnerGroup);
        }

        // POST: TestRunnerGroups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TestRunnerGroup testRunnerGroup, IFormCollection form)
        {

            if (ModelState.IsValid)
            {
                _context.Update(testRunnerGroup);
                var id = testRunnerGroup.TestRunnerGroupID;

                var projectsSelected = form["Projects"].ToList();
                var projects = _context.Project;
                foreach (var project in projects)
                {
                    _context.Update(project);
                    if (projectsSelected.Any(t => Convert.ToInt32(t) == project.ID))
                    {
                        project.TestRunnerGroupID = id;
                    }
                    else
                    {
                        project.TestRunnerGroupID = null;
                    }
                }

                var foldersSelected = form["Folders"].ToList();
                var folders = _context.Folder;
                foreach (var folder in folders)
                {
                    _context.Update(folder);
                    if (foldersSelected.Any(t => Convert.ToInt32(t) == folder.FolderID))
                    {
                        folder.TestRunnerGroupID = id;
                    }
                    else
                    {
                        folder.TestRunnerGroupID = null;
                    }
                }

                var groupsSelected = form["Groups"].ToList();
                var groups = _context.Group;
                foreach (var group in groups)
                {
                    _context.Update(group);
                    if (groupsSelected.Any(t => Convert.ToInt32(t) == group.GroupID))
                    {
                        group.TestRunnerGroupID = id;
                    }
                    else
                    {
                        group.TestRunnerGroupID = null;
                    }
                }

                var runsSelected = form["Runs"].ToList();
                var runs = _context.Run;
                foreach (var run in runs)
                {
                    _context.Update(run);
                    if (runsSelected.Any(t => Convert.ToInt32(t) == run.RunID))
                    {
                        run.TestRunnerGroupID = id;
                    }
                    else
                    {
                        run.TestRunnerGroupID = null;
                    }
                }

                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Test Runner Group: " + testRunnerGroup.Name + " successfully edited");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "TestRunnerGroups",
                    action = "Details",
                    ID = testRunnerGroup.TestRunnerGroupID
                }));
            }

            return RedirectToAction("Index");
        }

        // GET: TestRunnerGroups/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TestRunnerGroup testRunnerGroup = _context.TestRunnerGroup.Single(m => m.TestRunnerGroupID == id);

            var projects = _context.Project.Where(t => t.TestRunnerGroupID == id).ToList();
            if (projects.Count != 0)
            {
                ViewBag.ProjectsDisplayString = String.Join(", ", projects.Select(t => t.Name).ToList());
            }

            var folders = _context.Folder.Where(t => t.TestRunnerGroupID == id).ToList();
            if (folders.Count != 0)
            {
                ViewBag.FoldersDisplayString = String.Join(", ", folders.Select(t => t.Name).ToList());
            }

            var groups = _context.Group.Where(t => t.TestRunnerGroupID == id).ToList();
            if (groups.Count != 0)
            {
                ViewBag.GroupsDisplayString = String.Join(", ", groups.Select(t => t.Name).ToList());
            }

            var runs = _context.Run.Where(t => t.TestRunnerGroupID == id).ToList();
            if (runs.Count != 0)
            {
                ViewBag.RunsDisplayString = String.Join(", ", runs.Select(t => t.Name).ToList());
            }

            if (testRunnerGroup == null)
            {
                return HttpNotFound();
            }

            return View(testRunnerGroup);
        }

        // POST: TestRunnerGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            TestRunnerGroup testRunnerGroup = _context.TestRunnerGroup.Single(m => m.TestRunnerGroupID == id);

            foreach (var project in _context.Project.Where(t => t.TestRunnerGroupID == id))
            {
                _context.Update(project);
                project.TestRunnerGroupID = null;
            }

            foreach (var project in _context.Project.Where(t => t.TestRunnerGroupID == id))
            {
                _context.Update(project);
                project.TestRunnerGroupID = null;
            }

            foreach (var folder in _context.Folder.Where(t => t.TestRunnerGroupID == id))
            {
                _context.Update(folder);
                folder.TestRunnerGroupID = null;
            }

            foreach (var group in _context.Group.Where(t => t.TestRunnerGroupID == id))
            {
                _context.Update(group);
                group.TestRunnerGroupID = null;
            }

            foreach (var run in _context.Run.Where(t => t.TestRunnerGroupID == id))
            {
                _context.Update(run);
                run.TestRunnerGroupID = null;
            }
            _context.SaveChanges();

            _context.TestRunnerGroup.Remove(testRunnerGroup);
            
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Test Runner Group: " + testRunnerGroup.Name + " successfully deleted");

            return RedirectToAction("Index");
        }
    }

    public class TestRunnerGroupsAndNewTestRunnerGroupAndProjectsAndFoldersAndGroupsAndRuns
    {
        public TestRunnerGroup TestRunnerGroup { get; set; }
        public IEnumerable<TestRunnerGroup> TestRunnerGroups { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<Folder> Folders { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<Run> Runs { get; set; }
    }

    public class TestRunnerGroupAndTestRunnersAndNewTestRunner
    {
        public TestRunnerGroup TestRunnerGroup { get; set; }
        public IEnumerable<TestRunner> TestRunners { get; set; }
        public TestRunner TestRunner { get; set; }
    }
}
