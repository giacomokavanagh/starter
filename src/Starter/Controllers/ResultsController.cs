using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Authorization;

namespace Starter.Controllers
{
    [Authorize]
    public class ResultsController : Controller
    {
        private ApplicationDbContext _context;

        public ResultsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Results
        public IActionResult Index()
        {
            return View();
        }

        // GET: Results
        public IActionResult ResultsForTestRun(int? id)
        {
            ResultsForTestRun resultsForTestRun = new ResultsForTestRun();
            resultsForTestRun.TestRun = _context.TestRun.Single(t => t.TestRunID == id);
            resultsForTestRun.Results = _context.Result.Where(t => t.TestRunID == id).ToList();

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            ViewData["TestRunID"] = id;

            var Test = _context.Test.Single(t => t.TestID == resultsForTestRun.TestRun.TestID);
            var TestName = Test.Name;
            ViewData["TestID"] = Test.TestID;

            var Run = _context.Run.Single(t => t.RunID == resultsForTestRun.TestRun.RunID);
            var RunName = Run.Name;
            ViewData["RunID"] = Run.RunID;
            ViewData["TestRunPageTitle"] = "Results for Test: " + TestName + " in Run: " + RunName;

            return View(resultsForTestRun);
        }

        // GET: Results/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ResultWithAllStepsAndScreenshotsAndTestRun resultWithAllStepsAndScreenshotsAndTestRun
                = new ResultWithAllStepsAndScreenshotsAndTestRun();
            resultWithAllStepsAndScreenshotsAndTestRun.Result = _context.Result.Single(m => m.ResultID == id);
            if (resultWithAllStepsAndScreenshotsAndTestRun == null)
            {
                return HttpNotFound();
            }

            resultWithAllStepsAndScreenshotsAndTestRun.StoredStepDetailsList = _context.StoredStepDetails.Where
                (t => t.ResultID == id).ToList();
            resultWithAllStepsAndScreenshotsAndTestRun.StoredScreenshotDetailsList = _context.StoredScreenshotDetails
                .Where(t => t.ResultID == id).ToList();

            var TestRunID = resultWithAllStepsAndScreenshotsAndTestRun.Result.TestRunID;
            resultWithAllStepsAndScreenshotsAndTestRun.TestRun = _context.TestRun.Single
                (t => t.TestRunID == TestRunID);
            resultWithAllStepsAndScreenshotsAndTestRun.TestRun.Run = _context.Run.Single(t => t.RunID ==
                resultWithAllStepsAndScreenshotsAndTestRun.TestRun.RunID);
            resultWithAllStepsAndScreenshotsAndTestRun.TestRun.Test = _context.Test.Single(t => t.TestID
                == resultWithAllStepsAndScreenshotsAndTestRun.TestRun.TestID);

            foreach (var item in resultWithAllStepsAndScreenshotsAndTestRun.StoredStepDetailsList)
            {
                var StoredTestExceptionDetailsList = _context.StoredTestExceptionDetails.Where
                    (t => t.StoredStepDetailsID == item.StoredStepDetailsID).ToList();
                if(StoredTestExceptionDetailsList.Any())
                {
                    item.listTestExceptionDetails = StoredTestExceptionDetailsList;
                }
            }

            return View(resultWithAllStepsAndScreenshotsAndTestRun);
        }

        // GET: Results/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Results/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Result result)
        {
            if (ModelState.IsValid)
            {
                _context.Result.Add(result);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(result);
        }

        // GET: Results/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Result result = _context.Result.Single(m => m.ResultID == id);
            if (result == null)
            {
                return HttpNotFound();
            }

            return View(result);
        }

        // POST: Results/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Result result)
        {
            if (ModelState.IsValid)
            {
                _context.Update(result);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(result);
        }

        // GET: Results/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Result result = _context.Result.Single(m => m.ResultID == id);

            
            if (result == null)
            {
                return HttpNotFound();
            }

            return View(result);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Result result = _context.Result.Single(m => m.ResultID == id);
            _context.Result.Remove(result);
            Directory.Delete(result.ResultDirectory, true);
            Directory.Delete(result.ScreenshotsDirectory, true);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Result: " + result.ResultID + " successfully deleted");

            return RedirectToAction("ResultsForTestRun", new RouteValueDictionary(new
            {
                controller = "Results",
                action = "ResultsForTestRun",
                ID = result.TestRunID
            }));

        }

        [ActionName("ReturnStoredTestFile")]
        public ActionResult ReturnStoredTestFile(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Result result = _context.Result.Single(m => m.ResultID == id);
            if (result == null)
            {
                return HttpNotFound();
            }

            var path = Path.Combine(result.ResultDirectory, "test", result.StoredTestDataFileName);

            var file = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);

            return File(file, result.StoredTestDataFileContentType, result.StoredTestDataFileName);
        }

        [ActionName("ReturnStoredTestEnvironmentsFile")]
        public ActionResult ReturnStoredTestEnvironmentsFile(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Result result = _context.Result.Single(m => m.ResultID == id);
            if (result == null)
            {
                return HttpNotFound();
            }

            var path = Path.Combine(result.ResultDirectory, "environment", result.StoredTestEnvironmentFileName);

            var file = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);

            return File(file, result.StoredTestEnvironmentFileContentType, result.StoredTestEnvironmentFileName);
        }
    }

    public class ResultsForTestRun
    {
        public ICollection<Result> Results { get; set; }
        public TestRun TestRun { get; set; }
        public Result Result { get; set; }
    }
        
    public class ResultWithAllStepsAndScreenshotsAndTestRun
    {
        public Result Result { get; set; }
        public ICollection<StoredStepDetails> StoredStepDetailsList { get; set; }
        public ICollection<StoredScreenshotDetails> StoredScreenshotDetailsList { get; set; }
        public TestRun TestRun { get; set; }
    }
}
