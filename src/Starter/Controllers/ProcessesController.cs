using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using System.Collections.Generic;
using System;

namespace Starter.Controllers
{
    public class ProcessesController : Controller
    {
        private ApplicationDbContext _context;

        public ProcessesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Processes
        public IActionResult Index()
        {
            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            return View(_context.Process.ToList());
        }

        // GET: Processes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            var model = new ViewModels.Platform.ProcessDetailsViewModel();
            model.Process = _context.Process.Single(t => t.ProcessID == id);
            if (model.Process == null)
            {
                return HttpNotFound();
            }

            model.Process.Component = _context.Component.Single(t => t.ComponentID == model.Process.ComponentID);
            model.Process.Component.Area = _context.Area.Single(m => m.AreaID == model.Process.Component.AreaID);
            model.Process.Component.Area.Platform = _context.Platform.Single(t => t.PlatformID ==
                model.Process.Component.Area.PlatformID);

            model.NewProcessStep = new ProcessStep();
            model.NewProcessStep.ProcessID = id.Value;

            var allApplicableProcessSteps = _context.ProcessStep.Where(t => t.ProcessID == id.Value);
            model.ProcessSteps = allApplicableProcessSteps.OrderBy(t => t.Order).ToList();

            if (model.ProcessSteps.Any())
            {
                model.NewProcessStep.Order = model.ProcessSteps.Last().Order + 1;
            }
            else
            {
                model.NewProcessStep.Order = 1;
            }

            ViewBag.AvailableMethods = new SelectList(_context.AvailableMethod, "Name", "Name");

            model.ProcessesInProcedure = _context.ProcessInProcedure.Where(t => t.ProcessID == id).ToList();

            foreach(var item in model.ProcessesInProcedure)
            {
                item.Procedure = _context.Procedure.Single(t => t.ProcedureID == item.ProcedureID);
            }

            return View(model);
        }

        // GET: Processes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Processes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Process process)
        {
            if (ModelState.IsValid)
            {
                _context.Process.Add(process);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Process: " + process.Name + " successfully created");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Processes",
                    action = "Details",
                    ID = process.ProcessID
                }));
            }
            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Processes",
                action = "Details",
                ID = process.ProcessID
            }));
        }

        // GET: Processes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Process process = _context.Process.Single(m => m.ProcessID == id);
            if (process == null)
            {
                return HttpNotFound();
            }
            ViewBag.Components = new SelectList(_context.Component, "ComponentID", "Name", process.ComponentID);

            return View(process);
        }

        // POST: Processes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Process process)
        {
            if (ModelState.IsValid)
            {
                _context.Update(process);
                _context.SaveChanges();

                HttpContext.Session.SetString("Message", "Process: " + process.Name + " successfully edited");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Processes",
                    action = "Details",
                    ID = process.ProcessID
                }));
            }
            return View(process);
        }

        // GET: Processes/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Process process = _context.Process.Single(m => m.ProcessID == id);
            if (process == null)
            {
                return HttpNotFound();
            }
            process.Component = _context.Component.Single(t => t.ComponentID == process.ComponentID);

            return View(process);
        }

        // POST: Processes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Process process = _context.Process.Single(m => m.ProcessID == id);
            _context.Process.Remove(process);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Process: " + process.Name + " successfully deleted");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Component",
                action = "Details",
                ID = process.ComponentID
            }));
        }

        public IActionResult CreateProcessStep(ProcessStep processStep)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            _context.ProcessStep.Add(processStep);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Step: " + processStep.StepID + " successfully created");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Processes",
                action = "Details",
                ID = processStep.ProcessID
            }));
        }

        [ActionName("EditStep")]
        public IActionResult EditStep(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ProcessStep processStep = _context.ProcessStep.Single(t => t.ProcessStepID == id);
            if (processStep == null)
            {
                return HttpNotFound();
            }

            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            ViewBag.AvailableMethods = new SelectList(_context.AvailableMethod, "Name", "Name");

            return View(processStep);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStep(ProcessStep processStep)
        {
            if (ModelState.IsValid)
            {
                var previousStepID = _context.ProcessStep.AsNoTracking().Single(t => t.ProcessStepID == processStep.ProcessStepID).StepID;

                if (stepIDIsNotUniqueToTest(processStep.ProcessStepID, processStep.StepID, previousStepID))
                {
                    HttpContext.Session.SetString("Message", "Step ID must be unique for process");

                    return RedirectToAction("EditStep", new RouteValueDictionary(new
                    {
                        controller = "Processes",
                        action = "EditStep",
                        ID = processStep.ProcessStepID
                    }));
                }

                _context.Update(processStep);
                _context.SaveChanges();

                ReorderStepsAroundEdit(processStep.ProcessStepID, processStep.Order);

                HttpContext.Session.SetString("Message", "Process Step: " + processStep.StepID + " successfully edited");

                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Processes",
                    action = "Details",
                    ID = processStep.ProcessID
                }));
            }

            HttpContext.Session.SetString("Message", "I was unable to edit a step for some reason");

            return RedirectToAction("EditStep", new RouteValueDictionary(new
            {
                controller = "Processes",
                action = "EditStep",
                ID = processStep.ProcessStepID
            }));
        }

        public string ReorderStepsAroundEdit(int? id, int Order)
        {
            if (id == null)
            {
                return "Step ID is null";
            }

            ProcessStep processStep = _context.ProcessStep.Single(t => t.ProcessStepID == id);
            if (processStep == null)
            {
                return "Process Step not found";
            }

            IEnumerable<ProcessStep> AllStepsInProcess = _context.ProcessStep.Where
                (t => t.ProcessID == processStep.ProcessID).OrderBy(t => t.Order);
            int newOrder = 1;
            foreach (var item in AllStepsInProcess)
            {
                _context.Update(item);
                if (item.ProcessStepID == processStep.ProcessStepID)
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

            ProcessStep processStep = _context.ProcessStep.Single(t => t.ProcessStepID == id);

            if (processStep == null)
            {
                return HttpNotFound();
            }

            ProcessStep processStepAbove = _context.ProcessStep.Single(t => t.ProcessID == processStep.ProcessID && t.Order == processStep.Order - 1);

            _context.Update(processStep);
            processStep.Order = processStep.Order - 1;

            _context.Update(processStepAbove);
            processStepAbove.Order = processStepAbove.Order + 1;

            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Process Step: " + processStep.StepID + " successfully swapped with " + processStepAbove.StepID);

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Processes",
                action = "Details",
                ID = processStep.ProcessID
            }));
        }

        public IActionResult MoveStepDown(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ProcessStep processStep = _context.ProcessStep.Single(t => t.ProcessStepID == id);

            if (processStep == null)
            {
                return HttpNotFound();
            }

            ProcessStep processStepBelow = _context.ProcessStep.Single(t => t.ProcessID == processStep.ProcessID && t.Order == processStep.Order + 1);

            _context.Update(processStep);
            processStep.Order = processStep.Order + 1;

            _context.Update(processStepBelow);
            processStepBelow.Order = processStepBelow.Order - 1;

            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Process Step: " + processStep.StepID + " successfully swapped with " + processStepBelow.StepID);

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Processes",
                action = "Details",
                ID = processStep.ProcessID
            }));
        }

        public IActionResult DeleteStep(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ProcessStep processStep = _context.ProcessStep.Single(t => t.ProcessStepID == id);
            if (processStep == null)
            {
                return HttpNotFound();
            }
            var processID = processStep.ProcessID;

            _context.ProcessStep.Remove(processStep);
            _context.SaveChanges();

            ReorderStepsAroundDelete(processID);

            HttpContext.Session.SetString("Message", "Process Step: " + processStep.StepID + " successfully deleted");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Processes",
                action = "Details",
                ID = processID
            }));
        }

        public string ReorderStepsAroundDelete(int? id)
        {
            if (id == null)
            {
                return "Process Step ID not present";
            }

            IEnumerable<ProcessStep> AllStepsInProcess = _context.ProcessStep.Where(t => t.ProcessStepID == id).OrderBy(t => t.Order);
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
            ProcessStep processStep = _context.ProcessStep.Single(t => t.ProcessStepID == id);

            if (stepIDIsNotUniqueToTest(id, value, processStep.StepID))
            {
                var messageString = "Step ID of: " + processStep.StepID + " was not unique for step of ID: " + processStep.StepID;
                HttpContext.Session.SetString("Message", messageString);
                return messageString;
            }

            processStep.StepID = value;

            _context.Update(processStep);
            _context.SaveChanges();

            return "StepID changed to " + processStep.StepID;
        }

        [HttpPost]
        public string SetMethod(int? id, string value)
        {
            ProcessStep processStep = _context.ProcessStep.Single(t => t.ProcessStepID == id);


            if (!_context.AvailableMethod.Where(t => t.Name == value).Any())
            {
                var messageString = "That method: " + value + " is not available";
                HttpContext.Session.SetString("Message", messageString);
                return messageString;
            }

            processStep.Method = value;

            _context.Update(processStep);
            _context.SaveChanges();

            return "Method changed to " + processStep.Method;
        }

        [HttpPost]
        public string SetAttribute(int? id, string value)
        {
            ProcessStep processStep = _context.ProcessStep.Single(t => t.ProcessStepID == id);

            processStep.Attribute = value;

            _context.Update(processStep);
            _context.SaveChanges();

            return "Attribute changed to " + processStep.Attribute;
        }

        [HttpPost]
        public string SetValue(int? id, string value)
        {
            ProcessStep processStep = _context.ProcessStep.Single(t => t.ProcessStepID == id);

            processStep.Value = value;

            _context.Update(processStep);
            _context.SaveChanges();

            return "Value changed to " + processStep.Value;
        }

        [HttpPost]
        public string SetInput(int? id, string value)
        {
            ProcessStep processStep = _context.ProcessStep.Single(t => t.ProcessStepID == id);

            processStep.Input = value;

            _context.Update(processStep);
            _context.SaveChanges();

            return "Input changed to " + processStep.Input;
        }

        [HttpPost]
        public string SetStatic(string id, string value)
        {
            var actualID = id.Replace("StaticID", "");
            var intActualId = Convert.ToInt32(actualID);
            ProcessStep processStep = _context.ProcessStep.Single(t => t.ProcessStepID == intActualId);

            var blnValue = Convert.ToBoolean(value);
            processStep.Static = blnValue;

            _context.Update(processStep);
            _context.SaveChanges();

            return "Static changed to " + blnValue;
        }

        [HttpPost]
        public string SetOrder(int? id, int value)
        {
            ReorderStepsAroundEdit(id, value);

            return "Order changed to " + value;
        }

        private bool stepIDIsNotUniqueToTest(int? id, string newStepID, string previousStepID)
        {
            if (id == null)
            {
                return false;
            }

            if(newStepID == previousStepID)
            {
                return false;
            }

            ProcessStep processStep = _context.ProcessStep.AsNoTracking().Single(t => t.ProcessStepID == id);
            if (processStep == null)
            {
                return false;
            }

            var allStepIDs = _context.ProcessStep.Where(t => t.ProcessID == processStep.ProcessID && t.ProcessStepID != id).
                Select(t => t.StepID).AsNoTracking().ToList();
            allStepIDs.Add(previousStepID);
            return allStepIDs.Contains(newStepID);
        }

        public IActionResult UpdateAllProceduresFromProcess(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Process process = _context.Process.Single(t => t.ProcessID == id);
            if(process == null)
            {
                return HttpNotFound();
            }

            var processInProcedures = _context.ProcessInProcedure.Where(t => t.ProcessID == id);

            foreach(var item in processInProcedures)
            {
                item.ProcedureStepsInProcessInProcedures = _context.ProcedureStepInProcessInProcedure.Where
                    (t => t.ProcessInProcedureID == item.ProcessInProcedureID).ToList();
                foreach(var procedureStepInProcessInProcedure in item.ProcedureStepsInProcessInProcedures)
                {
                    ProcedureStep procedureStep = _context.ProcedureStep.Single(t => t.ProcedureStepID ==
                        procedureStepInProcessInProcedure.ProcedureStepID);
                    ProcessStep processStep = _context.ProcessStep.Single(t => t.ProcessStepID == procedureStep.ProcessStepID);
                    procedureStep.Method = processStep.Method;
                    procedureStep.Attribute = processStep.Attribute;
                    procedureStep.Value = processStep.Value;
                    procedureStep.Input = processStep.Input;
                    procedureStep.Static = processStep.Static;
                    _context.Update(procedureStep);
                }
            }
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Process updates pushed to all procedures using Process: "
                + process.Name);

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Processes",
                action = "Details",
                ID = id
            }));
        }
    }
}
