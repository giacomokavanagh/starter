using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using System.Collections.Generic;
using System;
using Starter.ViewModels.Category;

namespace Starter.Controllers
{
    public class ProceduresController : Controller
    {
        private ApplicationDbContext _context;

        public ProceduresController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Procedures
        public IActionResult Index()
        {
            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            return View(_context.Procedure.ToList());
        }

        // GET: Procedures/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            var model = new ProcedureDetailsViewModel();
            model.Procedure = _context.Procedure.Single(t => t.ProcedureID == id);
            if (model.Procedure == null)
            {
                return HttpNotFound();
            }

            model.Procedure.Set = _context.Set.Single(t => t.SetID == model.Procedure.SetID);
            model.Procedure.Set.Collection = _context.Collection.Single(m => m.CollectionID == model.Procedure.Set.CollectionID);
            model.Procedure.Set.Collection.Category = _context.Category.Single(t => t.CategoryID ==
                model.Procedure.Set.Collection.CategoryID);

            model.NewProcedureStep = new ProcedureStep();
            model.NewProcedureStep.ProcedureID = id.Value;

            var allApplicableProcedureSteps = _context.ProcedureStep.Where(t => t.ProcedureID == id.Value);
            model.ProcedureSteps = allApplicableProcedureSteps.OrderBy(t => t.Order).ToList();

            if (model.ProcedureSteps.Any())
            {
                model.NewProcedureStep.Order = model.ProcedureSteps.Last().Order + 1;
            }
            else
            {
                model.NewProcedureStep.Order = 1;
            }

            ViewBag.AvailableMethods = new SelectList(_context.AvailableMethod, "Name", "Name");

            return View(model);
        }

        // GET: Procedures/Create
        public IActionResult Create()
        {
            ViewData["SetID"] = new SelectList(_context.Set, "SetID", "Set");
            return View();
        }

        // POST: Procedures/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Procedure procedure)
        {
            if (ModelState.IsValid)
            {
                _context.Procedure.Add(procedure);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Procedure: " + procedure.Name + " successfully created");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Procedures",
                    action = "Details",
                    ID = procedure.ProcedureID
                }));
            }
            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Procedures",
                action = "Details",
                ID = procedure.ProcedureID
            }));
        }

        // GET: Procedures/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Procedure procedure = _context.Procedure.Single(m => m.ProcedureID == id);
            if (procedure == null)
            {
                return HttpNotFound();
            }
            ViewBag.Procedures = new SelectList(_context.Set, "SetID", "Name", procedure.SetID);
            return View(procedure);
        }

        // POST: Procedures/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Procedure procedure)
        {
            if (ModelState.IsValid)
            {
                _context.Update(procedure);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Procedure: " + procedure.Name + " successfully edited");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Processes",
                    action = "Details",
                    ID = procedure.ProcedureID
                }));
            }
            return View(procedure);
        }

        // GET: Procedures/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Procedure procedure = _context.Procedure.Single(m => m.ProcedureID == id);
            if (procedure == null)
            {
                return HttpNotFound();
            }
            procedure.Set = _context.Set.Single(t => t.SetID == procedure.SetID);

            return View(procedure);
        }

        // POST: Procedures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Procedure procedure = _context.Procedure.Single(m => m.ProcedureID == id);
            _context.Procedure.Remove(procedure);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Procedures: " + procedure.Name + " successfully deleted");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Procedures",
                action = "Details",
                ID = procedure.ProcedureID
            }));
        }

        public IActionResult CreateProcedureStep(ProcedureStep procedureStep)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            _context.ProcedureStep.Add(procedureStep);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Step: " + procedureStep.StepID + " successfully created");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Processes",
                action = "Details",
                ID = procedureStep.ProcedureID
            }));
        }

        [ActionName("EditStep")]
        public IActionResult EditStep(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ProcedureStep procedureStep = _context.ProcedureStep.Single(t => t.ProcedureStepID == id);
            if (procedureStep == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            ViewBag.AvailableMethods = new SelectList(_context.AvailableMethod, "Name", "Name");

            return View(procedureStep);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStep(ProcedureStep procedureStep)
        {
            if (ModelState.IsValid)
            {
                var previousStepID = _context.ProcedureStep.AsNoTracking().Single(t => t.ProcedureStepID == procedureStep.ProcedureStepID).StepID;

                if (stepIDIsNotUniqueToTest(procedureStep.ProcedureStepID, procedureStep.StepID, previousStepID))
                {
                    HttpContext.Session.SetString("Message", "Step ID must be unique for process");

                    return RedirectToAction("EditStep", new RouteValueDictionary(new
                    {
                        controller = "Procedures",
                        action = "EditStep",
                        ID = procedureStep.ProcedureID
                    }));
                }

                _context.Update(procedureStep);
                _context.SaveChanges();

                ReorderStepsAroundEdit(procedureStep.ProcedureStepID, procedureStep.Order);

                HttpContext.Session.SetString("Message", "Procedure Step: " + procedureStep.StepID + " successfully edited");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Procedures",
                    action = "Details",
                    ID = procedureStep.ProcedureID
                }));
            }

            HttpContext.Session.SetString("Message", "I was unable to edit a step for some reason");

            return RedirectToAction("EditStep", new RouteValueDictionary(new
            {
                controller = "Procedures",
                action = "EditStep",
                ID = procedureStep.ProcedureID
            }));
        }

        public string ReorderStepsAroundEdit(int? id, int Order)
        {
            if (id == null)
            {
                return "Step ID is null";
            }

            ProcedureStep procedureStep = _context.ProcedureStep.Single(t => t.ProcedureStepID == id);
            if (procedureStep == null)
            {
                return "Process Step not found";
            }

            IEnumerable<ProcedureStep> AllStepsInProcess = _context.ProcedureStep.Where(t => t.ProcedureID == procedureStep.ProcedureID).OrderBy(t => t.Order);
            int newOrder = 1;
            foreach (var item in AllStepsInProcess)
            {
                _context.Update(item);
                if (item.ProcedureStepID == procedureStep.ProcedureStepID)
                {
                    newOrder = newOrder + 1;
                }
                else if (newOrder == Order)
                {
                    newOrder = newOrder + 1;
                    item.Order = newOrder;
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

            ProcedureStep procedureStep = _context.ProcedureStep.Single(t => t.ProcedureStepID == id);

            if (procedureStep == null)
            {
                return HttpNotFound();
            }

            ProcedureStep procedureStepAbove = _context.ProcedureStep.Single(t => t.ProcedureID == procedureStep.ProcedureID && t.Order == procedureStep.Order - 1);

            _context.Update(procedureStep);
            procedureStep.Order = procedureStep.Order - 1;

            _context.Update(procedureStepAbove);
            procedureStepAbove.Order = procedureStepAbove.Order + 1;

            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Procedure Step: " + procedureStep.StepID + " successfully swapped with " + procedureStepAbove.StepID);

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Procedures",
                action = "Details",
                ID = procedureStep.ProcedureID
            }));
        }

        public IActionResult MoveStepDown(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ProcedureStep procedureStep = _context.ProcedureStep.Single(t => t.ProcedureStepID == id);

            if (procedureStep == null)
            {
                return HttpNotFound();
            }

            ProcedureStep procedureStepBelow = _context.ProcedureStep.Single(t => t.ProcedureID == procedureStep.ProcedureID && t.Order == procedureStep.Order + 1);

            _context.Update(procedureStep);
            procedureStep.Order = procedureStep.Order + 1;

            _context.Update(procedureStepBelow);
            procedureStepBelow.Order = procedureStepBelow.Order - 1;

            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Procedure Step: " + procedureStep.StepID + " successfully swapped with " + procedureStepBelow.StepID);

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Procedures",
                action = "Details",
                ID = procedureStep.ProcedureID
            }));
        }

        public IActionResult DeleteStep(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ProcedureStep procedureStep = _context.ProcedureStep.Single(t => t.ProcedureStepID == id);
            if (procedureStep == null)
            {
                return HttpNotFound();
            }
            var procedureID = procedureStep.ProcedureID;

            _context.ProcedureStep.Remove(procedureStep);
            _context.SaveChanges();

            ReorderStepsAroundDelete(procedureID);

            HttpContext.Session.SetString("Message", "Process Step: " + procedureStep.StepID + " successfully deleted");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Processes",
                action = "Details",
                ID = procedureID
            }));
        }

        public string ReorderStepsAroundDelete(int? id)
        {
            if (id == null)
            {
                return "Process Step ID not present";
            }

            IEnumerable<ProcedureStep> AllStepsInProcess = _context.ProcedureStep.Where(t => t.ProcedureStepID == id).OrderBy(t => t.Order);
            int newOrder = 1;
            foreach (var item in AllStepsInProcess)
            {
                _context.Update(item);
                item.Order = newOrder;
                newOrder = newOrder + 1;
            }

            _context.SaveChanges();

            return "Items reordered";
        }

        [HttpPost]
        public string SetStepID(int? id, string value)
        {
            ProcedureStep procedureStep = _context.ProcedureStep.Single(t => t.ProcedureStepID == id);

            if (stepIDIsNotUniqueToTest(id, value, procedureStep.StepID))
            {
                var messageString = "Step ID of: " + procedureStep.StepID + " was not unique for step of ID: " + procedureStep.StepID;
                HttpContext.Session.SetString("Message", messageString);
                return messageString;
            }

            procedureStep.StepID = value;

            _context.Update(procedureStep);
            _context.SaveChanges();

            return "StepID changed to " + procedureStep.StepID;
        }

        [HttpPost]
        public string SetMethod(int? id, string value)
        {
            ProcedureStep procedureStep = _context.ProcedureStep.Single(t => t.ProcedureStepID == id);


            if (!_context.AvailableMethod.Where(t => t.Name == value).Any())
            {
                var messageString = "That method: " + value + " is not available";
                HttpContext.Session.SetString("Message", messageString);
                return messageString;
            }

            procedureStep.Method = value;

            _context.Update(procedureStep);
            _context.SaveChanges();

            return "Method changed to " + procedureStep.Method;
        }

        [HttpPost]
        public string SetAttribute(int? id, string value)
        {
            ProcedureStep procedureStep = _context.ProcedureStep.Single(t => t.ProcedureStepID == id);

            procedureStep.Attribute = value;

            _context.Update(procedureStep);
            _context.SaveChanges();

            return "Attribute changed to " + procedureStep.Attribute;
        }

        [HttpPost]
        public string SetValue(int? id, string value)
        {
            ProcedureStep procedureStep = _context.ProcedureStep.Single(t => t.ProcedureStepID == id);

            procedureStep.Value = value;

            _context.Update(procedureStep);
            _context.SaveChanges();

            return "Value changed to " + procedureStep.Value;
        }

        [HttpPost]
        public string SetInput(int? id, string value)
        {
            ProcedureStep procedureStep = _context.ProcedureStep.Single(t => t.ProcedureStepID == id);

            procedureStep.Input = value;

            _context.Update(procedureStep);
            _context.SaveChanges();

            return "Input changed to " + procedureStep.Input;
        }

        [HttpPost]
        public string SetOrder(int? id, int value)
        {
            ProcedureStep procedureStep = _context.ProcedureStep.Single(t => t.ProcedureStepID == id);

            procedureStep.Order = value;

            _context.Update(procedureStep);
            _context.SaveChanges();

            ReorderStepsAroundEdit(procedureStep.ProcedureStepID, value);

            return "Order changed to " + procedureStep.Order;
        }

        private bool stepIDIsNotUniqueToTest(int? id, string newStepID, string previousStepID)
        {
            if (id == null)
            {
                return false;
            }

            if (newStepID == previousStepID)
            {
                return false;
            }

            ProcedureStep procedureStep = _context.ProcedureStep.AsNoTracking().Single(t => t.ProcedureStepID == id);
            if (procedureStep == null)
            {
                return false;
            }

            var allStepIDs = _context.ProcedureStep.Where(t => t.ProcedureID == procedureStep.ProcedureID && t.ProcedureStepID != id).
                Select(t => t.StepID).AsNoTracking().ToList();
            allStepIDs.Add(previousStepID);
            return allStepIDs.Contains(newStepID);
        }
    }
}
