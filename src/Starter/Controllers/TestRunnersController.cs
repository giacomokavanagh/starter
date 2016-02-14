using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using System.Collections.Generic;
using System;
using Microsoft.AspNet.Hosting;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.AspNet.WebUtilities;
using System.ComponentModel.DataAnnotations;

namespace Starter.Controllers
{
    public class TestRunnersController : Controller
    {
        private ApplicationDbContext _context;
        private IHostingEnvironment _environment;

        public TestRunnersController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        private string strTestUploadsDirectory
        {
            get
            {
                return Path.Combine(_environment.WebRootPath, "uploads", "tests");
            }
        }

        private string strEnvironmentsUploadsDirectory
        {
            get
            {
                return Path.Combine(_environment.WebRootPath, "uploads", "environments");
            }
        }

        private string strResultsDirectory
        {
            get
            {
                return Path.Combine(_environment.WebRootPath, "results");
            }
        }

        private string strScreenshotsDirectory
        {
            get
            {
                return Path.Combine(_environment.WebRootPath, "images", "screenshots");
            }
        }

        private string strTestRunnerLogsDirectory
        {
            get
            {
                return Path.Combine(_environment.WebRootPath, "TestRunnerLogs");
            }
        }

        // GET: TestRunners
        public IActionResult Index()
        {
            return View(_context.TestRunner.ToList());
        }

        // GET: TestRunners/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            TestRunner testRunner = _context.TestRunner.Single(m => m.TestRunnerID == id);
            if (testRunner == null)
            {
                return HttpNotFound();
            }

            testRunner.TestRunnerGroup = _context.TestRunnerGroup.Single(m => m.TestRunnerGroupID == testRunner.TestRunnerGroupID);
            testRunner.TestRunnerLogs = _context.TestRunnerLog.Where(t => t.TestRunnerID == id).ToList();

            return View(testRunner);
        }

        // GET: TestRunners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TestRunners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TestRunner testRunner)
        {
            if (ModelState.IsValid)
            {
                _context.TestRunner.Add(testRunner);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Test Runner: " + testRunner.Name + " successfully created");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "TestRunnerGroups",
                    action = "Details",
                    ID = testRunner.TestRunnerGroupID
                }));
            }
            return View(testRunner);
        }

        // GET: TestRunners/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TestRunner testRunner = _context.TestRunner.Single(m => m.TestRunnerID == id);
            if (testRunner == null)
            {
                return HttpNotFound();
            }

            ViewBag.TestRunnerGroups = new SelectList(_context.TestRunnerGroup, "TestRunnerGroupID", "Name", testRunner.TestRunnerGroupID);
            
            return View(testRunner);
        }

        // POST: TestRunners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TestRunner testRunner)
        {
            if (ModelState.IsValid)
            {
                _context.Update(testRunner);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Test Runner: " + testRunner.Name + " successfully edited");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "TestRunners",
                    action = "Details",
                    ID = testRunner.TestRunnerID
                }));
            }
            return View(testRunner);
        }

        // GET: TestRunners/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TestRunner testRunner = _context.TestRunner.Single(m => m.TestRunnerID == id);
            if (testRunner == null)
            {
                return HttpNotFound();
            }

            testRunner.TestRunnerGroup = _context.TestRunnerGroup.Single(m => m.TestRunnerGroupID == testRunner.TestRunnerGroupID);

            return View(testRunner);
        }

        // POST: TestRunners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            TestRunner testRunner = _context.TestRunner.Single(m => m.TestRunnerID == id);
            _context.TestRunner.Remove(testRunner);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Test Runner: " + testRunner.Name + " successfully deleted");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "TestRunnerGroups",
                action = "Details",
                ID = testRunner.TestRunnerGroup
            }));
        }

        public ActionResult GetTestRunParametersForRobot(string RobotName)
        {
            if(RobotName == null)
            {
                return HttpNotFound();
            }

            var TestRunner = _context.TestRunner.Single(t => t.Name == RobotName);
            if (TestRunner == null)
            {
                return HttpNotFound();
            }

            var TestRunnerID = TestRunner.TestRunnerID;
            var trgID = TestRunner.TestRunnerGroupID;
            var Response = "No Test Run ready for robot";

            var i = 0;
            var TestFound = false;
            do
            {
                var ProjectsWithTestRunnerGroup = _context.Project.Where(t => t.TestRunnerGroupID == trgID).ToList();
                var FoldersWithParentProjectWithTestRunnerGroup = _context.Folder.Where
                    (t => t.TestRunnerGroupID == null && ProjectsWithTestRunnerGroup.Any(l => t.ProjectID == l.ID)).ToList();

                var FoldersWithTestRunnerGroup = _context.Folder.Where(t => t.TestRunnerGroupID == trgID).ToList();
                var combinedFolders = FoldersWithParentProjectWithTestRunnerGroup.Union(FoldersWithTestRunnerGroup);

                var GroupsWithTestRunnerGroup = _context.Group.Where(t => t.TestRunnerGroupID == trgID).ToList();
                var combinedGroups = GroupsWithTestRunnerGroup.Union
                    (_context.Group.Where(t => t.TestRunnerGroupID == null && combinedFolders.Any(l => t.FolderID == l.FolderID)));

                var RunsWithTestRunnerGroup = _context.Run.Where(t => t.TestRunnerGroupID == trgID).ToList();
                var combinedRuns = RunsWithTestRunnerGroup.Union
                    (_context.Run.Where(t => t.TestRunnerGroupID == null && combinedGroups.Any(l => t.GroupID == l.GroupID)));

                var TestRunsWithTestRunnerGroups = _context.TestRun.Where(t => t.TestRunnerGroupID == trgID).ToList();
                var combinedTestRuns = TestRunsWithTestRunnerGroups.Union
                    (_context.TestRun.Where(t => t.TestRunnerGroupID == null && combinedRuns.Any(l => t.RunID == l.RunID)));

                var ActiveTestRuns = combinedTestRuns.Where(t => t.Status == "Ready" && t.StartTime < DateTime.Now
                && (t.Retries == null || t.RetriesLeft > 0) && t.TestRunner == null);

                if (ActiveTestRuns.Any())
                {
                    var PossibleCandidateForRun = ActiveTestRuns.First();
                    var RefreshedCandidate = _context.TestRun.SingleOrDefault(t => t.TestRunID == PossibleCandidateForRun.TestRunID);

                    if (RefreshedCandidate.TestRunner == null)
                    {
                        RefreshedCandidate.TestRunner = TestRunnerID;
                        _context.Update(RefreshedCandidate);
                        _context.SaveChanges();
                    }
                    else
                    {
                        //pick another candidate
                    }

                    //Check it's not been picked up elsewhere at near exact same time
                    var SecondaryilyRefreshedCandidate = _context.TestRun.SingleOrDefault(t => t.TestRunID == PossibleCandidateForRun.TestRunID);

                    if (SecondaryilyRefreshedCandidate.TestRunner == TestRunnerID)
                    {
                        TestFound = true;

                        if (SecondaryilyRefreshedCandidate.Retries != null)
                        {
                            SecondaryilyRefreshedCandidate.RetriesLeft = SecondaryilyRefreshedCandidate.RetriesLeft - 1;
                            _context.Update(SecondaryilyRefreshedCandidate);
                            
                        }
                        Test test = _context.Test.Single(t => t.TestID ==
                            SecondaryilyRefreshedCandidate.TestID);

                        SecondaryilyRefreshedCandidate.Status = "Running";

                        CommandAndControlParameters commandAndControlParameters = new CommandAndControlParameters();
                        commandAndControlParameters.TestRunID = SecondaryilyRefreshedCandidate.TestRunID;
                        commandAndControlParameters.TestID = SecondaryilyRefreshedCandidate.TestID;
                        commandAndControlParameters.TestName = test.Name;
                        commandAndControlParameters.TestDataFilename = test.ExcelFilePath;
                        commandAndControlParameters.TestEnvironmentID = SecondaryilyRefreshedCandidate.TestEnvironmentID.Value;
                        commandAndControlParameters.Browser = SecondaryilyRefreshedCandidate.Browser;
                        commandAndControlParameters.TestEnvironmentFilename = _context.TestEnvironment.Single(t => t.TestEnvironmentID ==
                            SecondaryilyRefreshedCandidate.TestEnvironmentID).XMLFilePath;
                        commandAndControlParameters.ClearRemoteLogOnNextAccess = TestRunner.ClearRemoteLogOnNextAccess;

                        Response = JsonConvert.SerializeObject(commandAndControlParameters);

                        _context.SaveChanges();

                        break;
                    }
                }

                i++;
            } while (TestFound && i < 10);

            return new ObjectResult(Response);
        }

        [HttpPost]
        public void UploadResult(int? id, IFormCollection Form)
        {
            TestRun testRun = _context.TestRun.Single(t => t.TestRunID == id);
            Test test = _context.Test.Single(t => t.TestID == testRun.TestID);

            var TestReportContents = Form.Single(t => t.Key == "TestReportDetails").Value;
            TestReportDetails testReportDetails = JsonConvert.DeserializeObject<TestReportDetails>(TestReportContents);

            var DateTimeForFilename = StringClass.sanitiseDateTimeStringForFilename(testReportDetails.strStartTime);

            var frameworkLogDirectory = Path.Combine("TestRunnerLogs", DateTimeForFilename);
            Directory.CreateDirectory(frameworkLogDirectory);
            var frameworkLogFilePath = Path.Combine("TestRunnerLogs", DateTimeForFilename, Form.First().Key);
            System.IO.File.WriteAllText(frameworkLogFilePath, Form.First().Value);

            AddTestRunnerLog(frameworkLogFilePath, Form.First().Key, testRun.TestRunner.Value, DateTimeForFilename);

            var resultDirectory = Path.Combine(strResultsDirectory, testRun.TestRunID.ToString(), DateTimeForFilename);
            Directory.CreateDirectory(resultDirectory);

            string screenshotList = Form.Single(t => t.Key == "ListOfScreenshots").Value;
            List<ScreenshotDetails> ListOfScreenshotDetails = JsonConvert.DeserializeObject<List<ScreenshotDetails>>(screenshotList);
            var screenshotFolder = Path.Combine(strScreenshotsDirectory, testRun.TestRunID.ToString(), DateTimeForFilename);
            Directory.CreateDirectory(screenshotFolder);

            var ResultID = AddResultOfID(testRun, resultDirectory, screenshotFolder, testReportDetails, test, testRun.TestEnvironmentID.Value);

            string stepDetailsList = Form.Single(t => t.Key == "ListOfStepDetails").Value;
            List<StepDetails> ListOfStepDetails = JsonConvert.DeserializeObject<List<StepDetails>>(stepDetailsList);
            StoreStepDetailsList(ListOfStepDetails, ResultID);

            StoreScreenshotDetailsList(ListOfScreenshotDetails, screenshotFolder, ResultID);

            foreach (var item in ListOfScreenshotDetails)
            {
                string imageString = Form.Single(t => t.Key == item.strStepID).Value;
                byte[] imageByteArray = JsonConvert.DeserializeObject<byte[]>(imageString);
                var screenshotFilePath = Path.Combine(screenshotFolder, item.strStepID);

                System.IO.File.WriteAllBytes(screenshotFilePath + ".png", imageByteArray);
            }
        }

        private void AddTestRunnerLog(string strFilePath, string strFilename, int TestRunnerID, string DateTimeForFilename)
        {
            TestRunnerLog testRunnerLog = new TestRunnerLog();
            _context.TestRunnerLog.Add(testRunnerLog);
            testRunnerLog.FilePath = strFilePath;
            testRunnerLog.Filename = strFilename;
            testRunnerLog.TestRunnerID = TestRunnerID;
            testRunnerLog.DateTime = DateTimeForFilename;
            _context.SaveChanges();
        }

        [HttpPost]
        private int AddResultOfID(TestRun TestRun, string ResultDirectory, string ScreenshotsDirectory, TestReportDetails TestReportDetails, Test Test, int TestEnvironmentID)
        {
            Result result = new Result();
            _context.Result.Add(result);
            
            result.ResultName = TestReportDetails.TestName;
            result.ResultDirectory = ResultDirectory;
            result.ScreenshotsDirectory = ScreenshotsDirectory;

            result.StoredStatus = TestReportDetails.TestStatus;

            result.StepsPassed = TestReportDetails.StepsPassed;
            result.StepsFailed = TestReportDetails.StepsFailed;
            result.StepsBlocked = TestReportDetails.StepsBlocked;

            result.StoredStartTime = Convert.ToDateTime(TestReportDetails.strStartTime);
            result.StoredEndTime = Convert.ToDateTime(TestReportDetails.strEndTime);
            result.Duration = Convert.ToDateTime(TestReportDetails.Duration);

            result.StoredBrowser = TestReportDetails.Browser;

            result.TestRunID = TestRun.TestRunID;

            result.StoredTestID = Test.TestID;
            result.StoredTestName = Test.Name;
            if (Test.TestDataSource == "Excel")
            {
                var ExcelFilename = Test.ExcelFilePath;
                result.StoredTestDataFileName = ExcelFilename;
                var TestDataFilePath = Path.Combine(_environment.WebRootPath, "uploads", "tests", Test.TestID.ToString(), ExcelFilename);

                var testDirectory = Path.Combine(ResultDirectory, "test");
                Directory.CreateDirectory(testDirectory);
                var StoredTestDataFilePath = Path.Combine(testDirectory, ExcelFilename);
                System.IO.File.Copy(TestDataFilePath, StoredTestDataFilePath);

                result.StoredTestDataFileContentType = Test.ContentType;
            }

            var environmentDirectory = Path.Combine(ResultDirectory, "environment");
            Directory.CreateDirectory(environmentDirectory);

            TestEnvironment testEnvironment = _context.TestEnvironment.Single(t => t.TestEnvironmentID == TestEnvironmentID);
            result.StoredTestEnvironmentID = TestEnvironmentID;
            var TestEnvironmentFileName = testEnvironment.XMLFilePath;
            result.StoredTestEnvironmentFileName = TestEnvironmentFileName;
            result.StoredTestEnvironmentName = testEnvironment.Name;
            var EnvironmentDataFilePath = Path.Combine(_environment.WebRootPath, "uploads", "environments", TestEnvironmentID.ToString(),
                TestEnvironmentFileName);
            var StoredEnvironmentFilePath = Path.Combine(environmentDirectory, TestEnvironmentFileName);
            System.IO.File.Copy(EnvironmentDataFilePath, StoredEnvironmentFilePath);

            result.StoredTestEnvironmentFileContentType = testEnvironment.ContentType;

            result.StoredTestRunnerID = TestRun.TestRunner.Value;
            result.StoredTestRunnerName = _context.TestRunner.Single(t => t.TestRunnerID == TestRun.TestRunner.Value).Name;

            _context.SaveChanges();

            return result.ResultID;
        }

        [HttpPost]
        private void StoreScreenshotDetailsList(List<ScreenshotDetails> ListOfScreenshotDetails, string ScreenshotFolder, int ResultID)
        {
            int i = 0;
            foreach (var ScreenshotDetails in ListOfScreenshotDetails)
            {
                StoredScreenshotDetails storedScreenshotDetails = new StoredScreenshotDetails();

                var ScreenshotFilePath = System.IO.Path.Combine(ScreenshotFolder, ScreenshotDetails.strStepID);

                storedScreenshotDetails.StepID = ScreenshotDetails.strStepID;
                storedScreenshotDetails.StoredScreenshotFilePath = ScreenshotFilePath;
                storedScreenshotDetails.Order = i;
                storedScreenshotDetails.ResultID = ResultID;

                _context.StoredScreenshotDetails.Add(storedScreenshotDetails);

                i++;
            }
            _context.SaveChanges();
        }

        [HttpPost]
        private void StoreStepDetailsList(List<StepDetails> ListOfStepDetails, int ResultID)
        {
            int i = 0;
            foreach (var StepDetails in ListOfStepDetails)
            {
                StoredStepDetails storedStepDetails = new StoredStepDetails();

                storedStepDetails.StepID = StepDetails.strStepID;
                storedStepDetails.StoredStepStatus = StepDetails.StepStatus;
                storedStepDetails.Method = StepDetails.Method;
                storedStepDetails.Attribute = StepDetails.Attribute;
                storedStepDetails.Value = StepDetails.Value;
                storedStepDetails.Input = StepDetails.Input;
                storedStepDetails.StepStartTime = StepDetails.strStepStartTime;
                storedStepDetails.StepEndTime = StepDetails.strStepEndTime;
                storedStepDetails.CatastrophicFailure = StepDetails.CatastrophicFailure;

                storedStepDetails.Order = i;
                storedStepDetails.ResultID = ResultID;

                _context.StoredStepDetails.Add(storedStepDetails);
                _context.SaveChanges();

                if (StepDetails.ListOfTestExceptionDetails != null)
                {
                    foreach (var exception in StepDetails.ListOfTestExceptionDetails)
                    {
                        StoredTestExceptionDetails storedTestExceptionDetails = new StoredTestExceptionDetails();
                        storedTestExceptionDetails.StoredStepDetailsID = storedStepDetails.StoredStepDetailsID;
                        storedTestExceptionDetails.ExceptionType = exception.ExceptionType;
                        storedTestExceptionDetails.ExceptionMessage = exception.ExceptionMessage;

                        _context.StoredTestExceptionDetails.Add(storedTestExceptionDetails);
                    }
                }
                i++;
                _context.SaveChanges();
            }
        }

        public void FinaliseTestRun(int? id, string status, DateTime endTime)
        {
            TestRun testRun = _context.TestRun.Single(t => t.TestRunID == id);
            testRun.Status = status;
            testRun.EndTime = endTime;
            testRun.TestRunner = null;

            _context.Update(testRun);
            _context.SaveChanges();
        }

        public ActionResult ReturnTestRunnerLogFile(int? id)
        {
            if (id != null)
            {
                TestRunnerLog testRunnerLog = _context.TestRunnerLog.Single(m => m.TestRunnerLogID == id);

                var file = new FileStream(testRunnerLog.FilePath, FileMode.Open, FileAccess.ReadWrite);

                return File(file, "text/HTML", testRunnerLog.Filename);
            }
            return HttpNotFound();
        }

        // POST: TestRunners/Delete/5
        [ActionName("Delete Log")]
        public IActionResult DeleteLog(int? id)
        {
            if (id != null)
            {
                TestRunnerLog testRunnerLog = _context.TestRunnerLog.Single(m => m.TestRunnerLogID == id);
                _context.TestRunnerLog.Remove(testRunnerLog);
                _context.SaveChanges();

                System.IO.File.Delete(testRunnerLog.FilePath);

                var DateTimeDirectory = Path.Combine(strTestRunnerLogsDirectory, testRunnerLog.DateTime);
                System.IO.Directory.Delete(DateTimeDirectory);

                HttpContext.Session.SetString("Message", "Test Runner Log: " + testRunnerLog.Filename + " successfully deleted");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "TestRunners",
                    action = "Details",
                    ID = testRunnerLog.TestRunnerID
                }));
            }

            return HttpNotFound();
        }

        public ActionResult DeleteAllTestRunnerLogs(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TestRunner testRunner = new TestRunner();
            testRunner = _context.TestRunner.Single(t => t.TestRunnerID == id);
            if (testRunner == null)
            {
                return HttpNotFound();
            }

            return View(testRunner);
        }

        [HttpPost]
        public ActionResult ConfirmDeleteAllTestRunnerLogs(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            TestRunner testRunner = new TestRunner();
            testRunner = _context.TestRunner.Single(t => t.TestRunnerID == id);
            if(testRunner == null)
            {
                return HttpNotFound();
            }

            var AllTestRunnerLogsForTestRunner = _context.TestRunnerLog.Where(t => t.TestRunnerID == id);

            foreach(var testRunnerLog in AllTestRunnerLogsForTestRunner)
            {
                _context.TestRunnerLog.Remove(testRunnerLog);

                var DateTimeDirectory = Path.Combine(strTestRunnerLogsDirectory, testRunnerLog.DateTime);
                Directory.Delete(DateTimeDirectory);
            }
            _context.SaveChanges();

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "TestRunners",
                action = "Details",
                ID = testRunner.TestRunnerID
            }));
        }
    }

    public class CommandAndControlParameters
    {
        public int TestRunID { get; set; }
        public int TestID { get; set; }
        public string TestName { get; set; }
        public string TestDataFilename { get; set; }
        public int TestEnvironmentID { get; set; }
        public string TestEnvironmentFilename { get; set; }
        public string Browser { get; set; }
        public bool ClearRemoteLogOnNextAccess { get; set; }
    }

    public class ScreenshotDetails
    {
        public string strStepID { get; set; }
        public string ScreenshotFilePath { get; set; }
    }

    public class StepDetails
    {
        public string strStepID { get; set; }
        public string StepStatus { get; set; }
        public string Method { get; set; }
        public string Attribute { get; set; }
        public string Value { get; set; }
        public string Input { get; set; }
        public string strStepStartTime { get; set; }
        public string strStepEndTime { get; set; }
        public List<TestExceptionDetails> ListOfTestExceptionDetails { get; set; }
        public bool CatastrophicFailure { get; set; }
    }

    public class TestExceptionDetails
    {
        public string ExceptionType { get; set; }
        [Key]
        public string ExceptionMessage { get; set; }
    }

    public class TestReportDetails
    {
        public string TestName { get; set; }
        public string TestStatus { get; set; }
        public int StepsPassed { get; set; }
        public int StepsFailed { get; set; }
        public int StepsBlocked { get; set; }
        public string strStartTime { get; set; }
        public string strEndTime { get; set; }
        public string Browser { get; set; }
        public string Duration { get; set; }
    }
}
