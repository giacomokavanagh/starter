using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using System.Collections.Generic;
using System;
using Microsoft.AspNet.Authorization;

namespace Starter.Controllers
{
    [Authorize]
    public class RunsController : Controller
    {
        private ApplicationDbContext _context;

        public RunsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Runs
        public IActionResult Index()
        {
            var applicationDbContext = _context.Run.Include(r => r.Group);
            return View(applicationDbContext.ToList());
        }

        // GET: Runs/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            ProjectAndFolderAndGroupAndRunAndTestsAndTestRun viewModel = new ProjectAndFolderAndGroupAndRunAndTestsAndTestRun();
            viewModel.Run = _context.Run.Single(m => m.RunID == id);

            if (viewModel.Run == null)
            {
                return HttpNotFound();
            }

            viewModel.Run.TestRuns = _context.TestRun.Where(m => m.RunID == id).ToList();

            var dependencyGroup = new DependencyGroup();
            foreach (TestRun testRun in viewModel.Run.TestRuns)
            {
                testRun.Test = _context.Test.Single(t => t.TestID == testRun.TestID);
                testRun.TestEnvironment = _context.TestEnvironment.SingleOrDefault(t => t.TestEnvironmentID == testRun.TestEnvironmentID);

                var Results = _context.Result.Where(t => t.TestRunID == testRun.TestRunID);
                if(Results.Any())
                {
                    testRun.Result = Results.OrderBy(t => t.ResultID).First();
                }

                if (testRun.TestRunnerGroupID != null)
                {
                    testRun.TestRunnerGroup = _context.TestRunnerGroup.Single(t => t.TestRunnerGroupID == testRun.TestRunnerGroupID);
                }

                if (testRun.DependencyGroupID == null)
                {
                    var InvalidDependencyGroupIDsList = _context.Dependency.Where(t => t.TestRunID == testRun.TestRunID)
                    .Select(t => t.DependencyGroupID).ToList();
                    testRun.ValidDependencyGroups = _context.DependencyGroup.Where
                        (i => !InvalidDependencyGroupIDsList.Contains(i.DependencyGroupID)).ToList();
                }

                if (testRun.DependencyGroupID != null)
                {
                    dependencyGroup = _context.DependencyGroup.Single(t => t.DependencyGroupID == testRun.DependencyGroupID);
                    testRun.DependencyGroup = dependencyGroup;
                    var Dependencies = _context.Dependency.Where(t => t.DependencyGroupID == dependencyGroup.DependencyGroupID);


                    foreach (var dependency in Dependencies)
                    {
                        dependency.TestRun = _context.TestRun.SingleOrDefault(t => t.TestRunID == dependency.TestRunID);
                        testRun.DependencyGroup.Dependencies.Add(dependency);

                        var Run = _context.Run.SingleOrDefault(t => t.RunID == testRun.RunID);
                        var Group = _context.Group.SingleOrDefault(t => t.GroupID == Run.GroupID);
                        var Folder = _context.Folder.SingleOrDefault(t => t.FolderID == Group.FolderID);
                        var Project = _context.Project.SingleOrDefault(t => t.ID == Folder.ProjectID);

                        List<NavigationLink> NavLinkList = new List<NavigationLink>
                        {
                            new NavigationLink
                            {
                                Text = "Project: " + Project.Name,
                                Controller = "Projects",
                                Action = "Details",
                                RouteID = Project.ID
                            },
                            new NavigationLink
                            {
                                Text = "Folder: " + Folder.Name,
                                Controller = "Folders",
                                Action = "Details",
                                RouteID = Folder.FolderID
                            },
                            new NavigationLink
                            {
                                Text = "Group: " + Group.Name,
                                Controller = "Groups",
                                Action = "Details",
                                RouteID = Group.GroupID
                            },
                            new NavigationLink
                            {
                                Text = "Run: " + Run.Name,
                                Controller = "Runs",
                                Action = "Details",
                                RouteID = Run.RunID
                            }
                        };

                        ViewData["DependencyNavLinkList" + dependency.DependencyID] = NavLinkList;
                    }
                }
            }

            viewModel.Run.TestRunnerGroup = _context.TestRunnerGroup.SingleOrDefault
                (t => t.TestRunnerGroupID == viewModel.Run.TestRunnerGroupID);

            viewModel.Group =
                _context.Group.Single(m => m.GroupID == viewModel.Run.GroupID);

            viewModel.Folder =
                _context.Folder.Single(m => m.FolderID == viewModel.Group.FolderID);

            viewModel.Project =
                _context.Project.Single(m => m.ID == viewModel.Folder.ProjectID);

            string strTestRunnerGroupDescription, strTestRunnerGroupName;
            if (viewModel.Run.TestRunnerGroupID != null)
            {
                strTestRunnerGroupDescription = "Taken from Run";
                strTestRunnerGroupName = _context.TestRunnerGroup.Single
                (t => t.TestRunnerGroupID == viewModel.Run.TestRunnerGroupID).Name;
            }
            else if (viewModel.Group.TestRunnerGroupID != null)
            {
                strTestRunnerGroupDescription = "Taken from Group";
                strTestRunnerGroupName = _context.TestRunnerGroup.Single
                (t => t.TestRunnerGroupID == viewModel.Group.TestRunnerGroupID).Name;
            }
            else if (viewModel.Folder.TestRunnerGroupID != null)
            {
                strTestRunnerGroupDescription = "Taken from Folder";
                strTestRunnerGroupName = _context.TestRunnerGroup.Single
                (t => t.TestRunnerGroupID == viewModel.Folder.TestRunnerGroupID).Name;
            }
            else if (viewModel.Project.TestRunnerGroupID != null)
            {
                strTestRunnerGroupDescription = "Taken from Project";
                strTestRunnerGroupName = _context.TestRunnerGroup.Single
                (t => t.TestRunnerGroupID == viewModel.Project.TestRunnerGroupID).Name;
            }
            else
            {
                strTestRunnerGroupDescription = "No Test Runner Group assigned";
                strTestRunnerGroupName = "No Test Runner Group assigned";
            }
            ViewBag.TestRunnerGroupDescription = strTestRunnerGroupDescription;
            ViewBag.TestRunnerGroupName = strTestRunnerGroupName;
            var alltestrunIDs = _context.TestRun.Where(m => m.RunID == id).Select(t => t.TestRunID);
            ViewData["AllTestRunIDs"] = string.Join(",", alltestrunIDs);

            ViewBag.tests = new SelectList(_context.Test, "TestID", "Name");
            ViewBag.testEnvironments = new SelectList(_context.TestEnvironment, "TestEnvironmentID", "Name");
            ViewBag.browsers = new SelectList(new List<string> { "Chrome", "Firefox", "IE" });

            return View(viewModel);
        }

        public ActionResult LinkTestsToRuns(IFormCollection form)
        {
            Run run = _context.Run.Single(m => m.RunID == Convert.ToInt32(form["id"]));
            if(run == null)
            {
                return HttpNotFound();
            }

            List<string> usersListOfTests = form["Run.Tests"].ToList();

            string TestRunCreationMessage = "TestRuns created for: " + System.Environment.NewLine;
            int TestEnvironmentID = Convert.ToInt32(form["TestRun.TestEnvironmentID"]);
            List<string> BrowserList = form["TestRun.Browser"].ToList();
            string Status = form["Status"];
            string ResultFile = form["ResultFile"];

            foreach (string testID in usersListOfTests)
            {
                foreach (string Browser in BrowserList)
                {
                    TestRun testRun = new TestRun();
                    Test test = _context.Test.Single(t => t.TestID == Convert.ToInt32(testID));

                    testRun.RunID = run.RunID;
                    testRun.TestID = test.TestID;
                    testRun.TestEnvironmentID = TestEnvironmentID;
                    testRun.Browser = Browser;
                    testRun.Status = Status;

                    _context.TestRun.Add(testRun);

                    TestRunCreationMessage = TestRunCreationMessage + "Test: " + test.Name + " & Run: " + run.Name + System.Environment.NewLine;
                }
            }

            _context.SaveChanges();

            HttpContext.Session.SetString("Message", TestRunCreationMessage);

            if (ModelState.IsValid)
            {
                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Runs",
                    action = "Details",
                    ID = Convert.ToInt32(form["id"])
                }));
            }

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Runs",
                action = "Details",
                ID = Convert.ToInt32(form["id"])
            }));
        }

        // GET: Runs/Create
        public IActionResult Create()
        {
            ViewData["GroupID"] = new SelectList(_context.Group, "GroupID", "Group");
            return View();
        }

        // POST: Runs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Run run)
        {
            if (ModelState.IsValid)
            {
                _context.Run.Add(run);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Run: " + run.Name + " successfully created");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Runs",
                    action = "Details",
                    ID = run.RunID
                }));
            }

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Runs",
                action = "Details",
                ID = run.RunID
            }));
        }

        // GET: Runs/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            GroupAndRun groupAndRun = new GroupAndRun();
            groupAndRun.Run = _context.Run.Single(m => m.RunID == id);
            if (groupAndRun.Run == null)
            {
                return HttpNotFound();
            }

            groupAndRun.Group = _context.Group.Single(m => m.GroupID == groupAndRun.Run.GroupID);

            groupAndRun.Run.TestRunnerGroup = _context.TestRunnerGroup.SingleOrDefault
                (t => t.TestRunnerGroupID == groupAndRun.Run.TestRunnerGroupID);

            ViewBag.TestRunnerGroups = new SelectList(_context.TestRunnerGroup, "TestRunnerGroupID", "Name").ToList();

            ViewBag.Groups = new SelectList(_context.Group, "GroupID", "Name", groupAndRun.Run.GroupID);

            return View(groupAndRun);
        }

        // POST: Runs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Run run)
        {
            if (ModelState.IsValid)
            {
                _context.Update(run);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Run: " + run.Name + " successfully edited");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Runs",
                    action = "Details",
                    ID = run.RunID
                }));
            }

            return View(run);
        }

        // GET: Runs/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Run run = _context.Run.Single(m => m.RunID == id);
            if (run == null)
            {
                return HttpNotFound();
            }

            return View(run);
        }

        // POST: Runs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Run run = _context.Run.Single(m => m.RunID == id);
            _context.Run.Remove(run);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Run: " + run.Name + " successfully deleted");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Runs",
                action = "Details",
                ID = run.RunID
            }));
        }
    }
}
