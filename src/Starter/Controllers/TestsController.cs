using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNet.Http;
using System.IO;
using Microsoft.AspNet.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Authorization;
using System;

#if DNX451
using OfficeOpenXml;
#endif

namespace Starter.Controllers
{
    [Authorize]
    public class TestsController : Controller
    {
        private ApplicationDbContext _context;
        private IHostingEnvironment _environment;

        private string strUploadsDirectory
        {
            get
            {
                return Path.Combine(_environment.WebRootPath, "uploads", "tests");
            }
        }

        public TestsController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Tests
        public IActionResult Index()
        {
            var applicationDbContext = _context.Test.Include(t => t.Suite);
            return View(applicationDbContext.ToList());
        }

        // GET: Tests/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            TestViewModel model = new TestViewModel();
            model.Test = _context.Test.Single(m => m.TestID == id);
            if (model.Test == null)
            {
                return HttpNotFound();
            }

            int intSuiteID = model.Test.SuiteID;
            model.Suite = _context.Suite.Single(m => m.SuiteID == intSuiteID);

            int intSectionID = model.Suite.SectionID;
            model.Section = _context.Section.Single(m => m.SectionID == intSectionID);

            int intLibraryID = model.Section.LibraryID;
            model.Library = _context.Library.Single(m => m.LibraryID == intLibraryID);

            if (model.Test.TestDataSource == "Excel")
            {
                //ViewBag.strUploadsDirectory = strUploadsDirectory;
            }
            else if (model.Test.TestDataSource == "Starter")
            {
                var AllApplicableSteps = _context.Step.Where(t => t.TestID == id).ToList();
                model.Steps = AllApplicableSteps.OrderBy(t => t.Order);

                model.NewStep = new Step();
                model.NewStep.TestID = id.Value;
                if (model.Steps.Any())
                {
                    model.NewStep.Order = model.Steps.Last().Order + 1;
                }
                else
                {
                    model.NewStep.Order = 1;
                }

                ViewBag.AvailableMethods = new SelectList(_context.AvailableMethod, "Name", "Name");
            }

            model.TestRuns = _context.TestRun.Where(t => t.TestID == id);
            foreach(var item in model.TestRuns)
            {
                item.Run = _context.Run.Single(t => t.RunID == item.RunID);
            }

            return View(model);
        }

        // GET: Tests/Create
        public IActionResult Create()
        {
            ViewData["SuiteID"] = new SelectList(_context.Suite, "SuiteID", "Suite");
            return View();
        }

        // POST: Tests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Test test, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            _context.Test.Add(test);

            if (test.TestDataSource == "Excel")
            {
                test.ContentType = file.ContentType;
                if (file.Length > 0)
                {
                    var uploads = Path.Combine(strUploadsDirectory, test.TestID.ToString());

                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    await file.SaveAsAsync(Path.Combine(uploads, fileName));

                    _context.SaveChanges();
                    _context.Update(test);
                    test.ExcelFilePath = fileName;
                    _context.SaveChanges();

                    HttpContext.Session.SetString("Message", "Excel based Test: " + test.Name + " successfully created");
                }
            }
            else
            {
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Empty Test: " + test.Name + " successfully created. Please add some steps.");
            }

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Tests",
                action = "Details",
                ID = test.TestID
            }));
        }

        public IActionResult CreateStep(Step step)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            _context.Step.Add(step);
            _context.SaveChanges();

            createExcelFile(step.TestID);

            HttpContext.Session.SetString("Message", "Step: " + step.StepID + " successfully created");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Tests",
                action = "Details",
                ID = step.TestID
            }));
        }

        public void createExcelFile(int id)
        {
            IEnumerable<Step> Steps = _context.Step.Where(t => t.TestID == id).OrderBy(t => t.Order);

            Test test = _context.Test.Single(t => t.TestID == id);
            var strFileName = "GeneratedFor" + test.Name + ".xlsx";

            _context.Update(test);
            test.ExcelFilePath = strFileName;
            test.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            _context.SaveChanges();

            var uploads = Path.Combine(strUploadsDirectory, id.ToString());

            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            var strFilePath = Path.Combine(uploads, strFileName);
            FileInfo newFile = new FileInfo(strFilePath);

            if (newFile.Exists)
            {
                newFile.Delete();
            }

#if DNX451
            ExcelPackage pck = new ExcelPackage(newFile);

            var ws = pck.Workbook.Worksheets.Add("Script");

            ws.Cells["A1"].Value = "STEP_ID";
            ws.Cells["B1"].Value = "METHOD";
            ws.Cells["C1"].Value = "ATTRIBUTE";
            ws.Cells["D1"].Value = "VALUE";
            ws.Cells["E1"].Value = "INPUT";
            ws.Cells["A1:E1"].Style.Font.Bold = true;

            var row = 2;
            foreach (var item in Steps)
            {
                ws.Cells["A" + row].Value = item.StepID;
                ws.Cells["B" + row].Value = item.Method;
                ws.Cells["C" + row].Value = item.Attribute;
                ws.Cells["D" + row].Value = item.Value;
                ws.Cells["E" + row].Value = item.Input;

                row = row + 1;
            }

            pck.Save();
#endif
        }

        // GET: Tests/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            LibraryAndSectionAndSuiteAndTest libraryAndSectionAndSuiteAndTest = new LibraryAndSectionAndSuiteAndTest();
            libraryAndSectionAndSuiteAndTest.Test = _context.Test.Single(m => m.TestID == id);
            if (libraryAndSectionAndSuiteAndTest.Test == null)
            {
                return HttpNotFound();
            }

            libraryAndSectionAndSuiteAndTest.Suite = _context.Suite.Single
                (m => m.SuiteID == libraryAndSectionAndSuiteAndTest.Test.SuiteID);

            libraryAndSectionAndSuiteAndTest.Section = _context.Section.Single
                (m => m.SectionID == libraryAndSectionAndSuiteAndTest.Suite.SectionID);

            libraryAndSectionAndSuiteAndTest.Library = _context.Library.Single
                (m => m.LibraryID == libraryAndSectionAndSuiteAndTest.Section.LibraryID);

            ViewBag.Suites = new SelectList(_context.Suite, "SuiteID", "Name", libraryAndSectionAndSuiteAndTest.Test.SuiteID);

            return View(libraryAndSectionAndSuiteAndTest);
        }

        // POST: Tests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Test test, IFormFile file)
        {
            if (file != null)
            {
                var uploads = Path.Combine(strUploadsDirectory, test.TestID.ToString());

                Directory.Delete(uploads, true);

                Directory.CreateDirectory(uploads);

                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                await file.SaveAsAsync(Path.Combine(uploads, fileName));
                test.ContentType = file.ContentType;

                test.ExcelFilePath = fileName;
            }

            if (ModelState.IsValid)
            {
                _context.Update(test);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Test: " + test.Name + " successfully edited");
            }

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Tests",
                action = "Details",
                ID = test.TestID
            }));
        }

        [ActionName("EditStep")]
        public IActionResult EditStep(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Step step = _context.Step.Single(t => t.ID == id);
            if (step == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            ViewBag.AvailableMethods = new SelectList(_context.AvailableMethod, "Name", "Name");

            return View(step);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStep(Step step)
        {
            if (ModelState.IsValid)
            {
                var previousStepID = _context.Step.AsNoTracking().Single(t => t.ID == step.ID).StepID;

                if (stepIDIsNotUniqueToTest(step.ID, step.StepID, previousStepID))
                {
                    HttpContext.Session.SetString("Message", "Step ID must be unique for test");

                    return RedirectToAction("EditStep", new RouteValueDictionary(new
                    {
                        controller = "Tests",
                        action = "EditStep",
                        ID = step.ID
                    }));
                }
                        
                _context.Update(step);
                _context.SaveChanges();

                ReorderStepsAroundEdit(step.ID, step.Order);

                createExcelFile(step.TestID);

                HttpContext.Session.SetString("Message", "Step: " + step.StepID + " successfully edited");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Tests",
                    action = "Details",
                    ID = step.TestID
                }));
            }

            HttpContext.Session.SetString("Message", "I was unable to edit a step for some reason");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Tests",
                action = "Details",
                ID = step.TestID
            }));
        }

        public string ReorderStepsAroundEdit(int? id, int Order)
        {
            if (id == null)
            {
                return "Step ID is null";
            }

            Step step = _context.Step.Single(t => t.ID == id);
            if (step == null)
            {
                return "Step not found";
            }

            IEnumerable<Step> AllStepsInTest = _context.Step.Where(t => t.TestID == step.TestID
                && t.ID != step.ID).OrderBy(t => t.Order);
            int newOrder = 1;
            foreach (var item in AllStepsInTest)
            {
                _context.Update(item);
                if (item.ID == step.ID)
                {
                    item.Order = Order;
                }
                else if (newOrder == Order)
                {
                    newOrder = newOrder + 1;
                    item.Order = newOrder;
                    newOrder = newOrder + 1;
                }
                else
                {
                    item.Order = newOrder;
                    newOrder = newOrder + 1;
                }
            }

            _context.SaveChanges();

            return "Items reordered";
        }

        public IActionResult MoveStepUp(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Step step = _context.Step.Single(t => t.ID == id);

            if (step == null)
            {
                return HttpNotFound();
            }

            Step stepAbove = _context.Step.Single(t => t.TestID == step.TestID && t.Order == step.Order - 1);

            _context.Update(step);
            step.Order = step.Order - 1;

            _context.Update(stepAbove);
            stepAbove.Order = stepAbove.Order + 1;

            _context.SaveChanges();

            createExcelFile(step.TestID);

            HttpContext.Session.SetString("Message", "Step: " + step.StepID + " successfully swapped with " + stepAbove.StepID);

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Tests",
                action = "Details",
                ID = step.TestID
            }));
        }

        public IActionResult MoveStepDown(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Step step = _context.Step.Single(t => t.ID == id);

            if (step == null)
            {
                return HttpNotFound();
            }

            Step stepBelow = _context.Step.Single(t => t.TestID == step.TestID && t.Order == step.Order + 1);

            _context.Update(step);
            step.Order = step.Order + 1;

            _context.Update(stepBelow);
            stepBelow.Order = stepBelow.Order - 1;

            _context.SaveChanges();

            createExcelFile(step.TestID);

            HttpContext.Session.SetString("Message", "Step: " + step.StepID + " successfully swapped with " + stepBelow.StepID);

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Tests",
                action = "Details",
                ID = step.TestID
            }));
        }

        public IActionResult DeleteStep(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Step step = _context.Step.Single(t => t.ID == id);
            if (step == null)
            {
                return HttpNotFound();
            }
            var testID = step.TestID;

            _context.Step.Remove(step);
            _context.SaveChanges();

            ReorderStepsAroundDelete(testID);

            createExcelFile(step.TestID);

            HttpContext.Session.SetString("Message", "Step: " + step.StepID + " successfully deleted");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Tests",
                action = "Details",
                ID = testID
            }));
        }

        public string ReorderStepsAroundDelete(int? id)
        {
            if(id == null)
            {
                return "Step ID not present";
            }

            IEnumerable<Step> AllStepsInTest = _context.Step.Where(t => t.TestID == id).OrderBy(t => t.Order);
            int newOrder = 1;
            foreach (var item in AllStepsInTest)
            {
                _context.Update(item);
                item.Order = newOrder;
                newOrder = newOrder + 1;
            }

            _context.SaveChanges();

            return "Items reordered";
        }


        // GET: Tests/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            LibraryAndSectionAndSuiteAndTest libraryAndSectionAndSuiteAndTest = new LibraryAndSectionAndSuiteAndTest();
            libraryAndSectionAndSuiteAndTest.Test = _context.Test.Single(m => m.TestID == id);
            if (libraryAndSectionAndSuiteAndTest.Test == null)
            {
                return HttpNotFound();
            }

            int intSuiteID = libraryAndSectionAndSuiteAndTest.Test.SuiteID;
            libraryAndSectionAndSuiteAndTest.Suite = _context.Suite.Single(m => m.SuiteID == intSuiteID);

            int intSectionID = libraryAndSectionAndSuiteAndTest.Suite.SectionID;
            libraryAndSectionAndSuiteAndTest.Section = _context.Section.Single(m => m.SectionID == intSectionID);

            int intLibraryID = libraryAndSectionAndSuiteAndTest.Section.LibraryID;
            libraryAndSectionAndSuiteAndTest.Library = _context.Library.Single(m => m.LibraryID == intLibraryID);

            return View(libraryAndSectionAndSuiteAndTest);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Test test = _context.Test.Single(m => m.TestID == id);
            _context.Test.Remove(test);
            _context.SaveChanges();

            if (test.TestDataSource == "Excel" && test.ExcelFilePath != null)
            {
                Directory.Delete(Path.Combine(strUploadsDirectory, id.ToString()), true);
            }

            HttpContext.Session.SetString("Message", "Test: " + test.Name + " successfully deleted");

            int intSuiteID = test.SuiteID;
            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Suites",
                action = "Details",
                ID = intSuiteID
            }));
        }

        // GET: Tests/Delete/5
        [AllowAnonymous]
        [ActionName("ReturnExternalTestFile")]
        public ActionResult ReturnExternalTestFile(int? id, string Key, string RobotName)
        {
            if (RobotName == null)
            {
                return HttpNotFound();
            }

            var TestRunner = _context.TestRunner.Single(t => t.Name == RobotName);
            if (TestRunner == null)
            {
                return HttpNotFound();
            }

            if (!DerivedKeyCheck(TestRunner.TestRunnerID, Key))
            {
                return HttpNotFound();
            }

            Test test = _context.Test.Single(m => m.TestID == id);
            if (test == null)
            {
                return HttpNotFound();
            }

            var path = Path.Combine(strUploadsDirectory, test.TestID.ToString(), test.ExcelFilePath);

            var file = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);

            return File(file, test.ContentType, test.ExcelFilePath);
        }

        private bool DerivedKeyCheck(int TestRunnerID, string Key)
        {
            try
            {
                var DerivedKeyCheck = _context.DerivedKey.Single(t => t.TestRunnerID == TestRunnerID
                && t.DerivedKeyString == Key);
                return true;
            }
            catch (Exception exception)
            {
                //will need to log this at some point
                return false;
            }
        }

        [ActionName("ReturnTestFile")]
        public ActionResult ReturnTestFile(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Test test = _context.Test.Single(m => m.TestID == id);
            if (test == null)
            {
                return HttpNotFound();
            }

            var path = Path.Combine(strUploadsDirectory, test.TestID.ToString(), test.ExcelFilePath);

            var file = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);

            return File(file, test.ContentType, test.ExcelFilePath);
        }

        [HttpPost]
        public string SetStepID(int? id, string value)
        {
            Step step = _context.Step.Single(t => t.ID == id);

            if(stepIDIsNotUniqueToTest(id, value, step.StepID))
            {
                var messageString = "Step ID of: " + step.StepID + " was not unique for step of ID: " + step.StepID;
                HttpContext.Session.SetString("Message", messageString);
                return messageString;
            }

            step.StepID = value;

            _context.Update(step);
            _context.SaveChanges();

            createExcelFile(step.TestID);

            return "StepID changed to " + step.StepID;
        }

        [HttpPost]
        public string SetMethod(int? id, string value)
        {
            Step step = _context.Step.Single(t => t.ID == id);

            if(!_context.AvailableMethod.Where(t => t.Name == value).Any())
            {
                var messageString = "That method: " + value + " is not available";
                HttpContext.Session.SetString("Message", messageString);
                return messageString;
            }

            step.Method = value;

            _context.Update(step);
            _context.SaveChanges();

            createExcelFile(step.TestID);

            return "Method changed to " + step.Method;
        }

        [HttpPost]
        public string SetAttribute(int? id, string value)
        {
            Step step = _context.Step.Single(t => t.ID == id);

            step.Attribute = value;

            _context.Update(step);
            _context.SaveChanges();

            createExcelFile(step.TestID);

            return "Attribute changed to " + step.Attribute;
        }

        [HttpPost]
        public string SetValue(int? id, string value)
        {
            Step step = _context.Step.Single(t => t.ID == id);

            step.Value = value;

            _context.Update(step);
            _context.SaveChanges();

            createExcelFile(step.TestID);

            return "Value changed to " + step.Value;
        }

        [HttpPost]
        public string SetInput(int? id, string value)
        {
            Step step = _context.Step.Single(t => t.ID == id);

            step.Input = value;

            _context.Update(step);
            _context.SaveChanges();

            createExcelFile(step.TestID);

            return "Input changed to " + step.Input;
        }

        [HttpPost]
        public string SetOrder(int? id, int value)
        {
            Step step = _context.Step.Single(t => t.ID == id);

            step.Order = value;

            _context.Update(step);
            _context.SaveChanges();

            createExcelFile(step.TestID);

            ReorderStepsAroundEdit(id, value);

            return "Order changed to " + step.Order;
        }

        private bool stepIDIsNotUniqueToTest(int? id, string newStepID, string previousStepID)
        {
            if(id == null)
            {
                return false;
            }

            if(newStepID == previousStepID)
            {
                return false;
            }

            Step step = _context.Step.AsNoTracking().Single(t => t.ID == id);
            if (step == null)
            {
                return false;
            }

            var allStepIDs = _context.Step.Where(t => t.TestID == step.TestID && t.ID != id).
                Select(t => t.StepID).AsNoTracking().ToList();
            allStepIDs.Add(previousStepID);
            return allStepIDs.Contains(newStepID);
        }

        // GET: TestCases/Edit/5
        public IActionResult GenerateTestsFromProcedure(ViewModels.TestCase.GenerateTestCasesFromProcedure generateTestCases)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            Suite suite = _context.Suite.AsNoTracking().SingleOrDefault(t => t.SuiteID == generateTestCases.SuiteID);
            if (suite == null)
            {
                return HttpNotFound();
            }

            var suiteID = suite.SuiteID;
            var testCases = _context.TestCase.AsNoTracking().Where
                (t => t.ProcedureID == generateTestCases.ProcedureID).ToList();

            var procedureID = generateTestCases.ProcedureID;
            Procedure procedure = _context.Procedure.AsNoTracking().Single(t => t.ProcedureID == procedureID);
            var procedureSteps = _context.ProcedureStep.AsNoTracking().Where(t => t.ProcedureID == procedureID).ToList();

            string messageString = "";
            foreach (var item in testCases)
            {
                var testCaseSteps = _context.TestCaseStep.Where(t => t.TestCaseID == item.TestCaseID).ToList();

                Test test = new Test();
                var testName = procedure.Name + "_" + item.Name;
                test.Name = testName;
                test.Description = item.Description;
                test.SuiteID = suiteID;
                test.TestDataSource = "Starter";
                test.ExcelFilePath = testName;
                test.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                var oldTest = _context.Test.AsNoTracking().SingleOrDefault(t => t.Name == testName && t.SuiteID == suiteID);
                if (oldTest == null)
                {
                    _context.Test.Add(test);
                    _context.SaveChanges();

                    messageString = messageString + "Test: " + testName + " successfully created. ";

                    foreach (var procedureStep in procedureSteps)
                    {
                        var testCaseStep = testCaseSteps.Single(t => t.ProcedureStepID == procedureStep.ProcedureStepID);
                        Step step = new Step();
                        step.TestID = test.TestID;
                        step.Attribute = procedureStep.Attribute;
                        step.StepID = procedureStep.StepID;
                        step.Method = procedureStep.Method;
                        step.Attribute = procedureStep.Attribute;
                        step.Value = procedureStep.Value;
                        step.Input = testCaseStep.Data;
                        step.Order = procedureStep.Order;

                        var oldStep = _context.Step.AsNoTracking().SingleOrDefault(t => t.TestID == test.TestID
                            && t.Order == step.Order);
                        if (oldStep == null)
                        {
                            _context.Step.Add(step);
                        }
                        else
                        {
                            _context.Update(step);
                        }
                    }
                    createExcelFile(test.TestID);
                }
                else if (oldTest != null && generateTestCases.Overwrite)
                {
                    test.TestID = oldTest.TestID;
                    _context.Update(test);
                    _context.SaveChanges();

                    messageString = messageString + "Test: " + testName + " successfully updated. ";

                    foreach (var procedureStep in procedureSteps)
                    {
                        var testCaseStep = testCaseSteps.Single(t => t.ProcedureStepID == procedureStep.ProcedureStepID);
                        Step step = new Step();
                        step.TestID = test.TestID;
                        step.Attribute = procedureStep.Attribute;
                        step.StepID = procedureStep.StepID;
                        step.Method = procedureStep.Method;
                        step.Attribute = procedureStep.Attribute;
                        step.Value = procedureStep.Value;
                        step.Input = testCaseStep.Data;
                        step.Order = procedureStep.Order;

                        var oldStep = _context.Step.AsNoTracking().SingleOrDefault(t => t.TestID == test.TestID
                            && t.Order == step.Order);
                        if (oldStep == null)
                        {
                            _context.Step.Add(step);
                        }
                        else
                        {
                            step.ID = oldStep.ID;
                            _context.Update(step);
                        }
                    }
                    createExcelFile(test.TestID);
                }
                else
                {
                    messageString = messageString + "Test: " + testName + " already exists in Suite: " + suite.Name + ". ";
                }

                _context.SaveChanges();
            }

            HttpContext.Session.SetString("Message", messageString);

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Suites",
                action = "Details",
                ID = suiteID
            }));
        }
    }
}
