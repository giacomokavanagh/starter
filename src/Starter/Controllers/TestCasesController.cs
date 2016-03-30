using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using Starter.ViewModels.TestCase;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Http;

namespace Starter.Controllers
{
    public class TestCasesController : Controller
    {
        private ApplicationDbContext _context;

        public TestCasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TestCases
        public IActionResult Index()
        {
            return View(_context.TestCase.ToList());
        }

        // GET: TestCases/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TestCase testCase = _context.TestCase.Single(m => m.TestCaseID == id);
            if (testCase == null)
            {
                return HttpNotFound();
            }

            return View(testCase);
        }

        // GET: TestCases/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: TestCases/Create
        public IActionResult TestCasesForProcedure(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            var model = new TestCasesForProcedureViewModel();
            model.Procedure = _context.Procedure.Single(t => t.ProcedureID == id);

            if (model.Procedure == null)
            {
                return HttpNotFound();
            }

            model.Procedure.TestCases = _context.TestCase.Where(t => t.ProcedureID == id).ToList();

            model.NewTestCase = new TestCase();
            model.NewTestCase.Order = _context.TestCase.Where(t => t.ProcedureID == id).Select(t => t.Order).Max() + 1;
            model.NewTestCase.ProcedureID = id.Value;

            return View(model);
        }

        // GET: TestCases/Create
        public IActionResult ManageTestCasesForProcedure(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            var model = new ManageTestCasesForProcedureViewModel();
            model.Procedure = _context.Procedure.Single(t => t.ProcedureID == id);

            if (model.Procedure == null)
            {
                return HttpNotFound();
            }

            model.Procedure.ProcedureSteps = _context.ProcedureStep.Where
                    (t => t.ProcedureID == model.Procedure.ProcedureID).ToList();

            model.Procedure.TestCases = _context.TestCase.Where(t => t.ProcedureID == id).ToList();
            foreach (var item in model.Procedure.TestCases)
            {
                item.TestCaseSteps = _context.TestCaseStep.Where(t => t.TestCaseID == item.TestCaseID).ToList();
            }

            model.NewTestCase = new TestCase();
            model.NewTestCase.Order = _context.TestCase.Where(t => t.ProcedureID == id).Select(t => t.Order).Max() + 1;
            model.NewTestCase.ProcedureID = id.Value;

            ViewBag.Suites = new SelectList(_context.Suite, "SuiteID", "Name");

            model.GenerateTestCasesFromProcedure = new GenerateTestCasesFromProcedure();
            model.GenerateTestCasesFromProcedure.ProcedureID = id.Value;

            return View(model);
        }

        // POST: TestCases/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TestCase testCase)
        {
            if (ModelState.IsValid)
            {
                _context.TestCase.Add(testCase);

                var procedureID = testCase.ProcedureID;
                Procedure procedure = _context.Procedure.Single(t => t.ProcedureID == procedureID);

                var procedureSteps = _context.ProcedureStep.Where(t => t.ProcedureID == procedureID);
                foreach (var item in procedureSteps)
                {
                    TestCaseStep testCaseStep = new TestCaseStep();
                    testCaseStep.ProcedureStepID = item.ProcedureStepID;
                    testCaseStep.TestCaseID = testCase.TestCaseID;
                    _context.TestCaseStep.Add(testCaseStep);
                }
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Test Case: " + testCase.Name + " successfully created");

                return RedirectToAction("TestCasesForProcedure", new RouteValueDictionary(new
                {
                    controller = "TestCases",
                    action = "ManageTestCasesForProcedure",
                    ID = testCase.ProcedureID
                }));
            }
            return View(testCase);
        }

        // GET: TestCases/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TestCase testCase = _context.TestCase.Single(m => m.TestCaseID == id);
            if (testCase == null)
            {
                return HttpNotFound();
            }
            return View(testCase);
        }

        // POST: TestCases/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TestCase testCase)
        {
            if (ModelState.IsValid)
            {
                _context.Update(testCase);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Test Case: " + testCase.Name + " successfully edited");

                return RedirectToAction("TestCasesForProcedure", new RouteValueDictionary(new
                {
                    controller = "TestCases",
                    action = "TestCasesForProcedure",
                    ID = testCase.TestCaseID
                }));
            }
            return View(testCase);
        }

        // GET: TestCases/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TestCase testCase = _context.TestCase.Single(m => m.TestCaseID == id);
            if (testCase == null)
            {
                return HttpNotFound();
            }

            testCase.Procedure = _context.Procedure.Single(t => t.ProcedureID == testCase.ProcedureID);

            return View(testCase);
        }

        // POST: TestCases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            TestCase testCase = _context.TestCase.Single(m => m.TestCaseID == id);
            _context.TestCase.Remove(testCase);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Test Case: " + testCase.Name + " successfully deleted");

            return RedirectToAction("TestCasesForProcedure", new RouteValueDictionary(new
            {
                controller = "TestCases",
                action = "TestCasesForProcedure",
                ID = testCase.ProcedureID
            }));
        }

        [HttpPost]
        public string SetData(int? id, string value)
        {
            TestCaseStep testCaseStep = _context.TestCaseStep.Single(t => t.TestCaseStepID == id);

            testCaseStep.Data = value;

            _context.Update(testCaseStep);
            _context.SaveChanges();

            return "Data changed to " + testCaseStep.Data;
        }
    }
}
