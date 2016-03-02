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

            ViewBag.strUploadsDirectory = strUploadsDirectory;

            return View(libraryAndSectionAndSuiteAndTest);
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
            if (ModelState.IsValid)
            {
                _context.Test.Add(test);
                test.ContentType = file.ContentType;
                _context.SaveChanges();

                if (file.Length > 0)
                {
                    var uploads = Path.Combine(strUploadsDirectory, test.TestID.ToString());

                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    await file.SaveAsAsync(Path.Combine(uploads, fileName));

                    _context.Update(test);
                    test.ExcelFilePath = fileName;
                    _context.SaveChanges();
                }

                HttpContext.Session.SetString("Message", "Test: " + test.Name + " successfully created");
            }

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Tests",
                action = "Details",
                ID = test.TestID
            }));
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

            if(test.TestDataSource == "Excel" && test.ExcelFilePath != null)
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
    }
}
