using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Http;
using System;

namespace Starter.Controllers
{
    public class TestRunsController : Controller
    {
        private ApplicationDbContext _context;

        public TestRunsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: TestRuns
        public IActionResult Index()
        {
            var applicationDbContext = _context.TestRun.Include(t => t.Run).Include(t => t.Test);
            return View(applicationDbContext.ToList());
        }

        // GET: TestRuns/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TestRun testRun = _context.TestRun.Single(m => m.TestRunID == id);
            if (testRun == null)
            {
                return HttpNotFound();
            }

            return View(testRun);
        }

        // GET: TestRuns/Create
        public IActionResult Create()
        {
            ViewData["RunID"] = new SelectList(_context.Run, "RunID", "run");
            ViewData["TestID"] = new SelectList(_context.Test, "TestID", "test");
            return View();
        }

        // POST: TestRuns/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TestRun testRun)
        {
            if (ModelState.IsValid)
            {
                _context.TestRun.Add(testRun);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["RunID"] = new SelectList(_context.Run, "RunID", "run", testRun.RunID);
            ViewData["TestID"] = new SelectList(_context.Test, "TestID", "test", testRun.TestID);
            return View(testRun);
        }

        [HttpPost]
        public JsonResult RunTestNow(int id, string time)
        {
            TestRun testRun = _context.TestRun.Single(t => t.TestRunID == id);
            testRun.Test = _context.Test.Single(m => m.TestID == testRun.TestID);
            testRun.Run = _context.Run.Single(m => m.RunID == testRun.RunID);

            testRun.Status = StatusForTestRunBasedOnDependencies(id);
            testRun.StartTime = DateTime.Parse(time);
            if (testRun.Retries != null)
            {
                testRun.RetriesLeft = testRun.Retries;
            }
            _context.Update(testRun);
            _context.SaveChanges();

            return Json(testRun.Status);
        }

        [HttpPost]
        public IActionResult RunAllTestsNow(string AllTestRunIDs, int? id)
        {
            var TestRunIDArray = AllTestRunIDs.Split(',');
            var time = DateTime.Now;
            TestRun testRun = new TestRun();

            foreach (var testRunID in TestRunIDArray)
            {
                testRun = _context.TestRun.Single(t => t.TestRunID == Convert.ToInt32(testRunID));

                testRun.Status = StatusForTestRunBasedOnDependencies(Convert.ToInt32(testRunID));
                testRun.StartTime = time;
                if(testRun.Retries != null)
                {
                    testRun.RetriesLeft = testRun.Retries;
                }
                _context.Update(testRun);
            }
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "All tests set to run");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Runs",
                action = "Details",
                ID = id
            }));
        }

        [HttpPost]
        public IActionResult RunRemainingTestsNow(string AllTestRunIDs, int? id)
        {
            var TestRunIDArray = AllTestRunIDs.Split(',');
            var time = DateTime.Now;
            TestRun testRun = new TestRun();

            foreach (var testRunID in TestRunIDArray)
            {
                testRun = _context.TestRun.Single(t => t.TestRunID == Convert.ToInt32(testRunID));

                var status = testRun.Status;
                if(status == "Unassigned" || status == "Failed" || status == "Blocked" || status == "Waiting" || status == "Running")
                {
                    testRun.Status = StatusForTestRunBasedOnDependencies(Convert.ToInt32(testRunID));
                    testRun.StartTime = time;
                    if (testRun.Retries != null)
                    {
                        testRun.RetriesLeft = testRun.Retries;
                    }
                    _context.Update(testRun);
                }
            }
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "All remaining tests set to run");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Runs",
                action = "Details",
                ID = id
            }));
        }

        [HttpPost]
        public string StopTestNow(int? id)
        {
            TestRun testRun = _context.TestRun.Single(t => t.TestRunID == id);
            testRun.Test = _context.Test.Single(m => m.TestID == testRun.TestID);
            testRun.Run = _context.Run.Single(m => m.RunID == testRun.RunID);

            testRun.Status = "Unassigned";
            testRun.RetriesLeft = null;
            _context.Update(testRun);
            _context.SaveChanges();

            return testRun.Test.Name + " in " + testRun.Run.Name + " stopped";
        }

        [HttpPost]
        public IActionResult StopAllTestsNow(string AllTestRunIDs, int? id)
        {
            var TestRunIDArray = AllTestRunIDs.Split(',');
            TestRun testRun = new TestRun();

            foreach (var testRunID in TestRunIDArray)
            {
                testRun = _context.TestRun.Single(t => t.TestRunID == Convert.ToInt32(testRunID));

                if(testRun.Status != "Passed")
                {
                    testRun.RetriesLeft = null;
                    testRun.Status = "Unassigned";
                    _context.Update(testRun);
                }
            }
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "All tests set to stop");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Runs",
                action = "Details",
                ID = id
            }));
        }

        [HttpPost]
        public string UpdateTimeFromPicker(int? id, string time)
        {
            TestRun testRun = _context.TestRun.Single(t => t.TestRunID == id);
            testRun.Test = _context.Test.Single(m => m.TestID == testRun.TestID);
            testRun.Run = _context.Run.Single(m => m.RunID == testRun.RunID);

            testRun.StartTime = DateTime.Parse(time);
            _context.Update(testRun);
            _context.SaveChanges();

            return testRun.Test.Name + " in " + testRun.Run.Name + " start time set to " + time.ToString();
        }

        [HttpPost]
        public string SetRetries(int? id, int value)
        {
            TestRun testRun = _context.TestRun.Single(t => t.TestRunID == id);
            testRun.Test = _context.Test.Single(m => m.TestID == testRun.TestID);
            testRun.Run = _context.Run.Single(m => m.RunID == testRun.RunID);

            testRun.Retries = value;
            testRun.RetriesLeft = value;

            _context.Update(testRun);
            _context.SaveChanges();

            return testRun.Test.Name + " in " + testRun.Run.Name + " set to retry " + value + " time(s)";
        }

        [HttpPost]
        public string SetDependencyGroup(int? id, int value)
        {
            TestRun testRun = _context.TestRun.Single(t => t.TestRunID == id);
            testRun.Test = _context.Test.Single(m => m.TestID == testRun.TestID);
            testRun.Run = _context.Run.Single(m => m.RunID == testRun.RunID);
            string strTestRunName;

            if(value == -1)
            {
                testRun.DependencyGroupID = null;
                strTestRunName = testRun.Test.Name + " in " + testRun.Run.Name + " dependency group cleared";
            }
            else
            {
                testRun.DependencyGroupID = value;
                strTestRunName = testRun.Test.Name + " in " + testRun.Run.Name + " set to dependency group " + value;
            }
            

            _context.Update(testRun);
            _context.SaveChanges();

            return strTestRunName;
        }

        [HttpPost]
        public IActionResult ClearDependencyGroup(int? id)
        {
            TestRun testRun = _context.TestRun.Single(t => t.TestRunID == id);

            testRun.DependencyGroupID = null;

            _context.Update(testRun);

            testRun.Status = "Unassigned";
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Dependency Group for Test Run ID " + testRun.TestRunID.ToString() + " successfully deleted");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Runs",
                action = "Details",
                ID = testRun.RunID
            }));
        }


        // GET: TestRuns/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TestRun testRun = _context.TestRun.Single(m => m.TestRunID == id);

            testRun.Test = _context.Test.Single(m => m.TestID == testRun.TestID);
            testRun.Run = _context.Run.Single(m => m.RunID == testRun.RunID);

            ViewBag.testEnvironments = new SelectList(_context.TestEnvironment, "TestEnvironmentID", "Name", testRun.TestEnvironmentID);
            ViewBag.browsers = new SelectList(new List<string> { "Chrome", "Firefox", "IE" }, testRun.Browser);
            ViewBag.statuses = new SelectList(new List<string> { "Passed", "Failed", "Blocked", "Waiting", "Ready", "Unassigned" }, testRun.Status);

            ViewBag.TestRunnerGroups = new SelectList(_context.TestRunnerGroup, "TestRunnerGroupID", "Name", testRun.TestRunnerGroupID);

            List<DependencyGroup> dependencyGroups = new List<DependencyGroup>();
            if (testRun.DependencyGroupID == null)
            {
                var InvalidDependencyGroupIDsList = _context.Dependency.Where(t => t.TestRunID == testRun.TestRunID)
                .Select(t => t.DependencyGroupID).ToList();
                dependencyGroups = _context.DependencyGroup.Where
                    (i => !InvalidDependencyGroupIDsList.Contains(i.DependencyGroupID)).ToList();
            }
            else
            {
                dependencyGroups = _context.DependencyGroup.ToList();
            }

            ViewBag.dependencyGroups = new SelectList(dependencyGroups, "DependencyGroupID", "Name", testRun.DependencyGroupID);

            if (testRun == null)
            {
                return HttpNotFound();
            }

            return View(testRun);
        }

        // POST: TestRuns/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TestRun testRun)
        {
            if (testRun.TestRunnerGroupID == -1)
            {
                testRun.TestRunnerGroupID = null;
            }

            if (ModelState.IsValid)
            {
                
                _context.Update(testRun);

                if(testRun.DependencyGroupID == 0)
                {
                    testRun.DependencyGroupID = null;
                }

                _context.SaveChanges();
            }

            HttpContext.Session.SetString("Message", "Test Run ID " + testRun.TestRunID.ToString() + " successfully edited");


            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Runs",
                action = "Details",
                ID = testRun.RunID
            }));
        }

        // GET: TestRuns/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TestRun testRun = _context.TestRun.Single(m => m.TestRunID == id);
            if (testRun == null)
            {
                return HttpNotFound();
            }

            testRun.Test = _context.Test.Single(m => m.TestID == testRun.TestID);
            testRun.Run = _context.Run.Single(m => m.RunID == testRun.RunID);
            testRun.TestEnvironment = _context.TestEnvironment.Single(m => m.TestEnvironmentID == testRun.TestEnvironmentID);
            testRun.DependencyGroup = _context.DependencyGroup.Single(m => m.DependencyGroupID == testRun.DependencyGroupID);

            return View(testRun);
        }

        // POST: TestRuns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            TestRun testRun = _context.TestRun.Single(m => m.TestRunID == id);
            testRun.Run = _context.Run.Single(m => m.RunID == testRun.RunID);
            testRun.Test = _context.Test.Single(m => m.TestID == testRun.TestID);

            _context.TestRun.Remove(testRun);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Test Run for Test " + testRun.Test.Name + " in Run " + testRun.Run.Name + " successfully deleted");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Runs",
                action = "Details",
                ID = testRun.RunID
            }));
        }

        private string StatusForTestRunBasedOnDependencies(int id)
        {
            TestRun testRun = _context.TestRun.Single(m => m.TestRunID == id);
            string Status = "No Status Assigned";

            if (testRun.DependencyGroupID != null)
            {
                var dependencies = _context.Dependency.Where(t => t.DependencyGroupID == testRun.DependencyGroupID);
                var dependenciesTestRunIDsList = dependencies.Select(t => t.TestRunID).ToList();
                var dependentTestRuns = _context.TestRun.Where(t => dependenciesTestRunIDsList.Contains(t.TestRunID));
                var dependentTestRunsStatusList = dependentTestRuns.Select(t => t.Status).ToList();
                if (dependentTestRunsStatusList.Contains("Blocked") ||
                    dependentTestRunsStatusList.Contains("Failed"))
                {
                    Status = "Blocked";
                }
                else if(dependentTestRunsStatusList.Contains("Waiting") ||
                    dependentTestRunsStatusList.Contains("Running") ||
                    dependentTestRunsStatusList.Contains("Unassigned") ||
                    dependentTestRunsStatusList.Contains("Ready"))
                {
                    Status = "Waiting";
                }
                else if(!dependentTestRunsStatusList.Any(t => t != "Passed"))
                {
                    Status = "Ready";
                }
            }
            else
            {
                Status = "Ready";
            }

            return Status;
        }

        public void ResetWaitingOrBlockedTestRuns()
        {
            var testRunsWaitingOrBlocked = _context.TestRun.Where(t => (t.Status == "Waiting"|| t.Status == "Blocked")
                && t.StartTime < DateTime.Now);

            var testRunIDsWithDependency = _context.Dependency.Select(t => t.TestRunID).ToList();
            var testRunsWaitingOrBlockedWithDependency = testRunsWaitingOrBlocked.Where
                (t => testRunIDsWithDependency.Contains(t.TestRunID));

            if(testRunsWaitingOrBlockedWithDependency.Count() > 0)
            {
                foreach (var testRun in testRunsWaitingOrBlockedWithDependency)
                {
                    var status = StatusForTestRunBasedOnDependencies(testRun.TestRunID);
                    _context.Update(testRun);
                    testRun.Status = status;
                }
                _context.SaveChanges();
            }
        }

        public void ResetFailedTests()
        {
            var testRuns = _context.TestRun.Where(t => (t.Status == "Failed" || t.Status == "Blocked")
                && t.Retries != null && t.RetriesLeft > 0 && t.StartTime < DateTime.Now).ToList();

            if(testRuns.Count > 0)
            {
                foreach (var testRun in testRuns)
                {
                    _context.Update(testRun);
                    testRun.Status = "Ready";
                    testRun.RetriesLeft = testRun.RetriesLeft - 1;
                }
                _context.SaveChanges();
            }
        }
    }
}
