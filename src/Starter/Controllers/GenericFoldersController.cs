using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using Starter.Models;
using Starter.Services;
using Starter.ViewModels.Manage;

namespace Starter.Controllers
{
    public class GenericFoldersController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public GenericFoldersController(ApplicationDbContext context,
             UserManager<ApplicationUser> userManager,
             SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;    
        }

        // GET: GenericFolders
        public async Task<IActionResult> Index()
        {
            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            var user = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());

            var model = new ViewModels.GenericFolders.GenericFolderIndexViewModel();
            model.TreeviewNode = new ViewModels.GenericFolders.GenericFolderTreeNode();
            model.TreeviewNode.Seed = null;
            model.TreeviewNode.GenericFolders = _context.GenericFolder.ToList();

            var allTreeviewNodesForUser = _context.TreeviewNodeState.Where(t => t.UserEmail == user.Email).ToList();
            if (allTreeviewNodesForUser.Count != 0)
            {
                foreach (var item in model.TreeviewNode.GenericFolders)
                {
                    //Get the state of node expansion for all GenericFolders
                    if (allTreeviewNodesForUser.Any(t => t.GenericFolderID == item.GenericFolderID))
                    {
                        item.isExpanded = allTreeviewNodesForUser.SingleOrDefault(t => t.GenericFolderID == item.GenericFolderID).isExpanded;
                    }

                    //Get the test environments in all GenericFolders
                    item.TestEnvironments = _context.TestEnvironment.Where(t => t.GenericFolderID == item.GenericFolderID);
                }
            }

            model.NewFolder = new GenericFolder();
            model.NewFolder.ParentLocationID = null;
            model.ImaginaryRootFolder = new GenericFolder();

            model.ImaginaryRootFolder.TestEnvironments = _context.TestEnvironment.Where(t => t.GenericFolderID == null);
            //var testEnvironmentsForRootFolder = _context.TestEnvironment.Where(t => t.GenericFolderID == null);
            //if (testEnvironmentsForRootFolder.Any())
            //{
            //    model.ImaginaryRootFolder.TestEnvironments = testEnvironmentsForRootFolder;
            //}

            return View(model);
        }   

        // GET: GenericFolders/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var model = new ViewModels.GenericFolders.GenericFolderDetailsViewModel();
            model.ThisFolder = _context.GenericFolder.Single(m => m.GenericFolderID == id);
            if (model.ThisFolder == null)
            {
                return HttpNotFound();
            }

            model.ChildFolders = _context.GenericFolder.Where(m => m.ParentLocationID == id).ToList();

            model.NewFolder = new GenericFolder();
            model.NewFolder.ParentLocationID = id;

            return View(model);
        }

        // GET: GenericFolders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GenericFolders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GenericFolder genericFolder)
        {
            if (ModelState.IsValid)
            {
                _context.GenericFolder.Add(genericFolder);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genericFolder);
        }

        // GET: GenericFolders/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            GenericFolder genericFolder = _context.GenericFolder.Single(m => m.GenericFolderID == id);
            if (genericFolder == null)
            {
                return HttpNotFound();
            }
            return View(genericFolder);
        }

        // POST: GenericFolders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GenericFolder genericFolder)
        {
            if (ModelState.IsValid)
            {
                _context.Update(genericFolder);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genericFolder);
        }

        // GET: GenericFolders/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            GenericFolder genericFolder = _context.GenericFolder.Single(m => m.GenericFolderID == id);
            if (genericFolder == null)
            {
                return HttpNotFound();
            }

            return View(genericFolder);
        }

        // POST: GenericFolders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            GenericFolder genericFolder = _context.GenericFolder.Single(m => m.GenericFolderID == id);
            _context.GenericFolder.Remove(genericFolder);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task SetTreeviewNodeState(int? id, bool isExpanded)
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
            var userEmail = user.NormalizedEmail;

            var existingTreeviewNode = _context.TreeviewNodeState.SingleOrDefault(t => t.GenericFolderID == id
                && t.UserEmail == userEmail);

            if (existingTreeviewNode != null)
            {
                _context.Update(existingTreeviewNode);
                existingTreeviewNode.isExpanded = isExpanded;
            }
            else
            {
                var newTreeviewNode = new TreeviewNodeState();
                _context.TreeviewNodeState.Add(newTreeviewNode);

                newTreeviewNode.GenericFolderID = id.Value;
                newTreeviewNode.isExpanded = isExpanded;
                newTreeviewNode.UserEmail = user.NormalizedEmail;
            }

            _context.SaveChanges();
        }
    }
}
