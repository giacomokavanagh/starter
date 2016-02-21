using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using Microsoft.AspNet.Http;
using System.IO;
using Microsoft.AspNet.Hosting;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNet.Routing;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Starter.Controllers
{
    public class TestEnvironmentsController : Controller
    {
        private ApplicationDbContext _context;
        private IHostingEnvironment _environment;

        public TestEnvironmentsController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        private string strUploadsDirectory
        {
            get
            {
                return Path.Combine(_environment.WebRootPath, "uploads", "environments");
            }
        }

        // GET: Environments
        public IActionResult Index()
        {
            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            TestEnvironmentsAndNewTestEnvironment testEnvironmentsAndNewTestEnvironment = new TestEnvironmentsAndNewTestEnvironment();
            testEnvironmentsAndNewTestEnvironment.TestEnvironment = new TestEnvironment();
            testEnvironmentsAndNewTestEnvironment.TestEnvironments = _context.TestEnvironment.ToList();

            return View(testEnvironmentsAndNewTestEnvironment);
        }

        // GET: Environments/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            TestEnvironment environment = _context.TestEnvironment.Single(m => m.TestEnvironmentID == id);
            if (environment == null)
            {
                return HttpNotFound();
            }

            return View(environment);
        }

        // GET: Environments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Environments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestEnvironment testEnvironment, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                _context.TestEnvironment.Add(testEnvironment);
                testEnvironment.ContentType = file.ContentType;
                _context.SaveChanges();
            }


            var uploads = Path.Combine(strUploadsDirectory, testEnvironment.TestEnvironmentID.ToString());

            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            if (file.Length > 0)
            {
                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                await file.SaveAsAsync(Path.Combine(uploads, fileName));

                _context.Update(testEnvironment);
                testEnvironment.XMLFilePath = fileName;
                _context.SaveChanges();
            }

            HttpContext.Session.SetString("Message", "Environment: " + testEnvironment.Name + " successfully created");

            return RedirectToAction("Index");
        }

        // GET: Environments/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TestEnvironment environment = _context.TestEnvironment.Single(m => m.TestEnvironmentID == id);
            if (environment == null)
            {
                return HttpNotFound();
            }
            return View(environment);
        }

        // POST: Environments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TestEnvironment testEnvironment, IFormFile file)
        {
            if (file != null)
            {
                var uploads = Path.Combine(strUploadsDirectory, testEnvironment.TestEnvironmentID.ToString());

                Directory.Delete(uploads, true);

                Directory.CreateDirectory(uploads);
            
                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                await file.SaveAsAsync(Path.Combine(uploads, fileName));

                testEnvironment.ContentType = file.ContentType;
                testEnvironment.XMLFilePath = fileName;
            }

            if (ModelState.IsValid)
            {
                _context.Update(testEnvironment);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Environment: " + testEnvironment.Name + " successfully edited");

                return RedirectToAction("Index");
            }
            return View(testEnvironment);
        }

        // GET: Environments/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TestEnvironment testEnvironment = _context.TestEnvironment.Single(m => m.TestEnvironmentID == id);
            if (testEnvironment == null)
            {
                return HttpNotFound();
            }

            return View(testEnvironment);
        }

        // POST: Environments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            TestEnvironment testEnvironment = _context.TestEnvironment.Single(m => m.TestEnvironmentID == id);
            _context.TestEnvironment.Remove(testEnvironment);

            Directory.Delete(Path.Combine(strUploadsDirectory, id.ToString()), true);

            HttpContext.Session.SetString("Message", "Environment: " + testEnvironment.Name + " successfully deleted");

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Tests/Delete/5
        [ActionName("ReturnTestEnvironmentsFile")]
        public ActionResult ReturnTestEnvironmentsFile(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            TestEnvironment testEnvironment = _context.TestEnvironment.Single(m => m.TestEnvironmentID == id);
            if(testEnvironment == null)
            {
                return HttpNotFound();
            }

            var path = Path.Combine(strUploadsDirectory, testEnvironment.TestEnvironmentID.ToString(), testEnvironment.XMLFilePath);

            var file = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);

            return File(file, testEnvironment.ContentType, testEnvironment.XMLFilePath);
        }
}

    public class TestEnvironmentsAndNewTestEnvironment
    {
        public TestEnvironment TestEnvironment { get; set; }
        public IEnumerable<TestEnvironment> TestEnvironments { get; set; }
    }
}
