using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Http;

namespace Starter.Controllers
{
    public class GroupsController : Controller
    {
        private ApplicationDbContext _context;

        public GroupsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Groups
        public IActionResult Index()
        {
            var applicationDbContext = _context.Group.Include(g => g.Folder);
            return View(applicationDbContext.ToList());
        }

        // GET: Groups/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            ProjectAndFolderAndGroupAndRun projectAndFolderAndGroupAndRun = new ProjectAndFolderAndGroupAndRun();
            projectAndFolderAndGroupAndRun.Group = _context.Group.Single(m => m.GroupID == id);
            if (projectAndFolderAndGroupAndRun.Group == null)
            {
                return HttpNotFound();
            }

            projectAndFolderAndGroupAndRun.Group.TestRunnerGroup =
                _context.TestRunnerGroup.SingleOrDefault(t => t.TestRunnerGroupID == projectAndFolderAndGroupAndRun.Group.TestRunnerGroupID);

            projectAndFolderAndGroupAndRun.Folder = 
                _context.Folder.Single(m => m.FolderID == projectAndFolderAndGroupAndRun.Group.FolderID);

            projectAndFolderAndGroupAndRun.Project = 
                _context.Project.Single(m => m.ID == projectAndFolderAndGroupAndRun.Folder.ProjectID);

            projectAndFolderAndGroupAndRun.Run = new Run();
            projectAndFolderAndGroupAndRun.Runs = _context.Run.Where(l => l.GroupID == id);

            return View(projectAndFolderAndGroupAndRun);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            ViewData["FolderID"] = new SelectList(_context.Project, "ID", "Folder");
            return View();
        }

        // POST: Groups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Group group)
        {

            if (ModelState.IsValid)
            {
                _context.Group.Add(group);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Group: " + group.Name + " successfully created");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Groups",
                    action = "Details",
                    ID = group.GroupID
                }));
            }

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Groups",
                action = "Details",
                ID = group.GroupID
            }));
        }

        // GET: Groups/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ProjectAndFolderAndGroup projectAndFolderAndGroup = new ProjectAndFolderAndGroup();
            projectAndFolderAndGroup.Group = _context.Group.Single(m => m.GroupID == id);
            if (projectAndFolderAndGroup.Group == null)
            {
                return HttpNotFound();
            }

            projectAndFolderAndGroup.Group.TestRunnerGroup =
                _context.TestRunnerGroup.SingleOrDefault(t => t.TestRunnerGroupID == projectAndFolderAndGroup.Group.TestRunnerGroupID);

            projectAndFolderAndGroup.Folder = _context.Folder.Single(m => m.FolderID == projectAndFolderAndGroup.Group.FolderID);

            ViewBag.TestRunnerGroups = new SelectList(_context.TestRunnerGroup, "TestRunnerGroupID", "Name").ToList();

            ViewBag.Folders = new SelectList(_context.Folder, "FolderID", "Name", projectAndFolderAndGroup.Group.FolderID);
            
            return View(projectAndFolderAndGroup);
        }

        // POST: Groups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Group group)
        {
            if (ModelState.IsValid)
            {
                _context.Update(group);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Group: " + group.Name + " successfully edited");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Groups",
                    action = "Details",
                    ID = group.GroupID
                }));
            }

            return View(group);
        }

        // GET: Groups/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ProjectAndFolderAndGroup projectAndFolderAndGroup = new ProjectAndFolderAndGroup();
            projectAndFolderAndGroup.Group = _context.Group.Single(m => m.GroupID == id);
            if (projectAndFolderAndGroup.Group == null)
            {
                return HttpNotFound();
            }

            int intFolderID = projectAndFolderAndGroup.Group.FolderID;
            projectAndFolderAndGroup.Folder = _context.Folder.Single(m => m.FolderID == intFolderID);

            return View(projectAndFolderAndGroup);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Group group = _context.Group.Single(m => m.GroupID == id);
            _context.Group.Remove(group);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Group: " + group.Name + " successfully deleted");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Folders",
                action = "Details",
                ID = group.FolderID
            }));
        }
    }
}
