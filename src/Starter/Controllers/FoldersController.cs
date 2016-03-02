using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Authorization;

namespace Starter.Controllers
{
    [Authorize]
    public class FoldersController : Controller
    {
        private ApplicationDbContext _context;

        public FoldersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Folders
        public IActionResult Index()
        {
            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");
            return View(_context.Folder.ToList());
        }

        // GET: Folders/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            ProjectAndFolderAndGroup projectAndFolderAndGroup = new ProjectAndFolderAndGroup();
            projectAndFolderAndGroup.Folder = _context.Folder.Single(m => m.FolderID == id);
            if (projectAndFolderAndGroup.Folder == null)
            {
                return HttpNotFound();
            }

            projectAndFolderAndGroup.Folder.TestRunnerGroup = 
                _context.TestRunnerGroup.SingleOrDefault(t => t.TestRunnerGroupID == projectAndFolderAndGroup.Folder.TestRunnerGroupID);

            projectAndFolderAndGroup.Project = _context.Project.Single(m => m.ID == projectAndFolderAndGroup.Folder.ProjectID);

            projectAndFolderAndGroup.Group = new Group();
            projectAndFolderAndGroup.Groups = _context.Group.Where(l => l.FolderID == id);

            return View(projectAndFolderAndGroup);
        }

        // GET: Folders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Folders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Folder folder)
        {
            if (ModelState.IsValid)
            {
                _context.Folder.Add(folder);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Folder: " + folder.Name + " successfully created");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Folders",
                    action = "Details",
                    ID = folder.FolderID
                }));
            }

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Folders",
                action = "Details",
                ID = folder.FolderID
            }));
        }

        // GET: Folders/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ProjectAndFolder projectAndFolder = new ProjectAndFolder();
            projectAndFolder.Folder = _context.Folder.SingleOrDefault(m => m.FolderID == id);
            if (projectAndFolder.Folder == null)
            {
                return HttpNotFound();
            }

            projectAndFolder.Folder.TestRunnerGroup =
                _context.TestRunnerGroup.SingleOrDefault(t => t.TestRunnerGroupID == projectAndFolder.Folder.TestRunnerGroupID);

            ViewBag.TestRunnerGroups = new SelectList(_context.TestRunnerGroup, "TestRunnerGroupID", "Name").ToList();

            ViewBag.Projects = new SelectList(_context.Project, "ID", "Name", projectAndFolder.Folder.ProjectID);

            projectAndFolder.Project = _context.Project.Single(m => m.ID == projectAndFolder.Folder.ProjectID);
            
            return View(projectAndFolder);
        }

        // POST: Folders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Folder folder)
        {
            if (ModelState.IsValid)
            {
                _context.Update(folder);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Folder: " + folder.Name + " successfully edited");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Folders",
                    action = "Details",
                    ID = folder.FolderID
                }));
            }
            return View(folder);
        }

        // GET: Folders/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ProjectAndFolder projectAndFolder = new ProjectAndFolder();
            projectAndFolder.Folder = _context.Folder.Single(m => m.FolderID == id);
            if (projectAndFolder.Folder == null)
            {
                return HttpNotFound();
            }

            int intProjectID = projectAndFolder.Folder.ProjectID;
            projectAndFolder.Project = _context.Project.Single(m => m.ID == intProjectID);

            return View(projectAndFolder);
        }

        // POST: Folders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Folder folder = _context.Folder.Single(m => m.FolderID == id);
            _context.Folder.Remove(folder);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Folder: " + folder.Name + " successfully deleted");

            int ProjectID = folder.ProjectID;
            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Projects",
                action = "Details",
                ID = ProjectID
            }));
        }
    }
}
