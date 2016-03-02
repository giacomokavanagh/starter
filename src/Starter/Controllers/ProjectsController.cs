using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Authorization;

namespace Starter.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Projects
        public IActionResult Index()
        {

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            ProjectsAndNewProject projectsAndNewProject = new ProjectsAndNewProject();
            projectsAndNewProject.Project = new Project();
            projectsAndNewProject.Projects = _context.Project.ToList();

            return View(projectsAndNewProject);
        }

        // GET: Projects/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            ProjectAndFolder projectAndFolder = new ProjectAndFolder();
            projectAndFolder.Project = _context.Project.Single(m => m.ID == id);
            if (projectAndFolder.Project == null)
            {
                return HttpNotFound();
            }

            projectAndFolder.Project.TestRunnerGroup = 
                _context.TestRunnerGroup.SingleOrDefault(t => t.TestRunnerGroupID == projectAndFolder.Project.TestRunnerGroupID);
            projectAndFolder.Folder = new Folder();
            projectAndFolder.Folders = _context.Folder.Where(l => l.ProjectID == id);

            return View(projectAndFolder);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name, Description")]Project Project)
        {
            if (ModelState.IsValid)
            {
                _context.Project.Add(Project);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Project: " + Project.Name + " successfully created");
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: Projects/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Project Project = _context.Project.Single(m => m.ID == id);
            if (Project == null)
            {
                return HttpNotFound();
            }

            var testRunnerGroups = new SelectList(_context.TestRunnerGroup, "TestRunnerGroupID", "Name").ToList();
            ViewBag.TestRunnerGroups = testRunnerGroups;
            
            return View(Project);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Project Project)
        {
            if (ModelState.IsValid)
            {
                _context.Update(Project);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Project: " + Project.Name + " successfully edited");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Projects",
                    action = "Details",
                    ID = Project.ID
                }));
            }

            return RedirectToAction("Index");
        }

        // GET: Projects/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Project Project = _context.Project.Single(m => m.ID == id);
            if (Project == null)
            {
                return HttpNotFound();
            }

            Project.TestRunnerGroup = _context.TestRunnerGroup.Single(t => t.TestRunnerGroupID == Project.TestRunnerGroupID);

            return View(Project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Project Project = _context.Project.Single(m => m.ID == id);
            _context.Project.Remove(Project);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Project: " + Project.Name + " successfully deleted");
            return RedirectToAction("Index");
        }
    }

    public class ProjectsAndNewProject
    {
        public ICollection<Project> Projects { get; set; }
        public Project Project { get; set; }
    }

    public class ProjectAndFolder
    {
        public Project Project { get; set; }
        public Folder Folder { get; set; }
        public IEnumerable<Folder> Folders { get; set; }
    }

    public class ProjectAndFolderAndGroup
    {
        public Project Project { get; set; }
        public Folder Folder { get; set; }
        public Group Group { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }

    public class ProjectAndFolderAndGroupAndRun
    {
        public Project Project { get; set; }
        public Folder Folder { get; set; }
        public Group Group { get; set; }
        public Run Run { get; set; }
        public IEnumerable<Run> Runs { get; set; }
    }

    public class ProjectAndFolderAndGroupAndRunAndTestsAndTestRun
    {
        public Project Project { get; set; }
        public Folder Folder { get; set; }
        public Group Group { get; set; }
        public Run Run { get; set; }
        public IEnumerable<Test> Tests { get; set; }
        public TestRun TestRun { get; set; }
    }

    public class ProjectAndFolderAndGroupAndRunAndTestRun
    {
        public Project Project { get; set; }
        public Folder Folder { get; set; }
        public Group Group { get; set; }
        public Run Run { get; set; }
        public TestRun TestRun { get; set; }
    }


    public class GroupAndRun
    {
        public Group Group { get; set; }
        public Run Run { get; set; }
    }
}
