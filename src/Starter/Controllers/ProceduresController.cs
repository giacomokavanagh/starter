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
            var procedureID = id.Value;

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
            model.NewProcedureStep.ProcedureID = procedureID;

            var allApplicableProcedureSteps = _context.ProcedureStep.Where(t => t.ProcedureID == procedureID).OrderBy(t => t.Order);
            model.Procedure.ProcedureSteps = allApplicableProcedureSteps.ToList();

            model.ProcessesInProcedure = _context.ProcessInProcedure.Where(t => t.ProcedureID == procedureID).ToList();
            foreach (var item in model.ProcessesInProcedure)
            {
                item.Process = _context.Process.Single(t => t.ProcessID == item.ProcessID);
                item.ProcedureStepsInProcessInProcedures = _context.ProcedureStepInProcessInProcedure.Where
                    (t => t.ProcessInProcedureID == item.ProcessInProcedureID).ToList();
            }

            //for all the processes in the procedure
            foreach(var item in model.ProcessesInProcedure)
            {
                //for every procedure step stored in the process, in the procedure
                foreach (var procedureStepInProcessInProcedure in item.ProcedureStepsInProcessInProcedures)
                {
                    var procedureStep = _context.ProcedureStep.Single(t => t.ProcedureStepID ==
                        procedureStepInProcessInProcedure.ProcedureStepID);
                    _context.Update(procedureStep);

                    var processStep = _context.ProcessStep.Single(t => t.ProcessStepID == procedureStep.ProcessStepID);

                    if (processStep.Method == procedureStep.Method && processStep.Attribute == procedureStep.Attribute &&
                        processStep.Value == procedureStep.Value && processStep.Input == procedureStep.Input &&
                        processStep.Static == procedureStep.Static)
                    {
                        if (procedureStep.Order == processStep.Order)
                        {
                            procedureStep.MatchesProcessStep = true;
                        }
                        else
                        {
                            var processStepOrder = processStep.Order;
                            var procedureStepOrder = procedureStep.Order;

                            var ProcedureStepIDsInProcedureInProcess = item.ProcedureStepsInProcessInProcedures.Select(t => t.ProcedureStepID);
                            var ProcedureSteps = _context.ProcedureStep.Where(t => ProcedureStepIDsInProcedureInProcess.Contains
                                (t.ProcedureStepID)).OrderBy(t => t.Order);
                            var orderOfFirstStep = ProcedureSteps.First().Order;
                            //var orderOfFirstStep = _context.ProcedureStep.Single
                            //    (t => t.ProcedureStepID == ProcedureSteps.First().ProcedureStepID).Order;
                            if (processStepOrder == procedureStepOrder - orderOfFirstStep + 1)
                            {
                                procedureStep.MatchesProcessStep = true;
                            }
                            else
                            {
                                procedureStep.MatchesProcessStep = false;
                            }
                        }
                    }
                    else
                    {
                        procedureStep.MatchesProcessStep = false;
                    }
                }
            }
            _context.SaveChanges();

            if (model.Procedure.ProcedureSteps.Any())
            {
                model.NewProcedureStep.Order = model.Procedure.ProcedureSteps.Last().Order + 1;
            }
            else
            {
                model.NewProcedureStep.Order = 1;
            }

            model.ProcessImportViewModel = new ProcessImportViewModel();
            model.ProcessImportViewModel.ProcedureID = model.Procedure.ProcedureID;

            model.ProcessesInProcedure = _context.ProcessInProcedure.Where(t => t.ProcedureID == procedureID).ToList();
            foreach (var item in model.ProcessesInProcedure)
            {
                item.Process = _context.Process.Single(t => t.ProcessID == item.ProcessID);
                item.ProcedureStepsInProcessInProcedures = _context.ProcedureStepInProcessInProcedure.Where
                    (t => t.ProcessInProcedureID == item.ProcessInProcedureID).ToList();
            }

            ViewBag.AvailableMethods = new SelectList(_context.AvailableMethod, "Name", "Name");
            ViewBag.AvailableProcesses = new SelectList(_context.Process, "ProcessID", "Name");
            ViewData["ProcedureHasSteps"] = _context.ProcedureStep.Where(t => t.ProcedureID == id).Any();
            ViewBag.Steps = new SelectList(_context.ProcedureStep.Where(t => t.ProcedureID == id).OrderBy(t => t.Order)
                , "Order", "Order");

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
            ViewBag.Sets = new SelectList(_context.Set, "SetID", "Name", procedure.SetID);
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
                    controller = "Procedures",
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

            var testCases = _context.TestCase.Where(t => t.ProcedureID == id);
            foreach(var item in testCases)
            {
                _context.TestCase.Remove(item);
            }

            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Procedure: " + procedure.Name + " successfully deleted");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Sets",
                action = "Details",
                ID = procedure.SetID
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
                controller = "Procedures",
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

                if (stepIDIsNotUniqueToProcedureStep(procedureStep.ProcedureStepID, procedureStep.StepID, previousStepID))
                {
                    HttpContext.Session.SetString("Message", "Step ID must be unique for procedure");

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
                return "Procedure Step not found";
            }

            var AllStepsInProcedure = _context.ProcedureStep.Where
                (t => t.ProcedureID == procedureStep.ProcedureID).OrderBy(t => t.Order);
            int newOrder = 1;
            foreach (var item in AllStepsInProcedure)
            {
                _context.Update(item);
                if (item.ProcedureStepID == procedureStep.ProcedureStepID)
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

            if (procedureStep.ProcessStepID != null)
            {
                ProcedureStepInProcessInProcedure procedureStepInProcessinProcedure = _context.
                    ProcedureStepInProcessInProcedure.SingleOrDefault
                    (t => t.ProcedureStepID == procedureStep.ProcedureStepID);
                if (procedureStepInProcessinProcedure != null)
                {
                    _context.ProcedureStepInProcessInProcedure.Remove(procedureStepInProcessinProcedure);

                    if (_context.ProcedureStepInProcessInProcedure.Where
                        (t => t.ProcessInProcedureID == procedureStepInProcessinProcedure.ProcessInProcedureID)
                        .Count() == 1)
                    {
                        ProcessInProcedure processInProcedure = _context.ProcessInProcedure.Single
                        (t => t.ProcessInProcedureID == procedureStepInProcessinProcedure.ProcessInProcedureID);
                        _context.ProcessInProcedure.Remove(processInProcedure);
                    }
                }
            }

            _context.ProcedureStep.Remove(procedureStep);
            _context.SaveChanges();

            ReorderStepsAroundDelete(procedureID);

            HttpContext.Session.SetString("Message", "Procedure Step: " + procedureStep.StepID + " successfully deleted");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Procedures",
                action = "Details",
                ID = procedureID
            }));
        }

        public string ReorderStepsAroundDelete(int? id)
        {
            if (id == null)
            {
                return "Procedure Step ID not present";
            }

            IEnumerable<ProcedureStep> AllStepsInProcedure = _context.ProcedureStep.Where(t => t.ProcedureID == id).OrderBy(t => t.Order);
            int newOrder = 1;
            foreach (var item in AllStepsInProcedure)
            {
                _context.Update(item);
                item.Order = newOrder;
                newOrder = newOrder + 1;
            }

            _context.SaveChanges();

            return "Items reordered";
        }

        public IActionResult UpdateStepFromProcess(int? id)
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

            if (procedureStep.ProcessStepID == null)
            {
                return HttpNotFound();
            }

            var processStep = _context.ProcessStep.Single(t => t.ProcessStepID == procedureStep.ProcessStepID);

            procedureStep.Method = processStep.Method;
            procedureStep.Attribute = processStep.Attribute;
            procedureStep.Value = processStep.Value;
            procedureStep.Input = processStep.Input;
            procedureStep.Static = processStep.Static;

            _context.Update(procedureStep);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Procedure Step: " + procedureStep.StepID + " successfully updated from Process");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Procedures",
                action = "Details",
                ID = procedureStep.ProcedureID
            }));
        }

        public IActionResult GoToProcessFromProcedureStep(int? id)
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

            if (procedureStep.ProcessStepID == null)
            {
                HttpContext.Session.SetString("Message", "There is no Process associated with that Procedure Step");
                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Procedures",
                    action = "Details",
                    ID = procedureStep.ProcedureID
                }));
            }

            var processID = _context.ProcessStep.Single(t => t.ProcessStepID == procedureStep.ProcessStepID).ProcessID;

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Processes",
                action = "Details",
                ID = processID
            }));
        }

        [HttpPost]
        public string SetStepID(int? id, string value)
        {
            ProcedureStep procedureStep = _context.ProcedureStep.Single(t => t.ProcedureStepID == id);

            if (stepIDIsNotUniqueToProcedureStep(id, value, procedureStep.StepID))
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
            ReorderStepsAroundEdit(id, value);

            return "Order changed to " + value;
        }

        private bool stepIDIsNotUniqueToProcedureStep(int? id, string newStepID, string previousStepID)
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

        private bool stepIDIsNotUniqueToProcedure(int procedureID, string newStepID)
        {
            var allStepIDs = _context.ProcedureStep.Where(t => t.ProcedureID == procedureID).
                Select(t => t.StepID).AsNoTracking().ToList();
            return allStepIDs.Contains(newStepID);
        }

        // POST: Procedures/ImportProcess/5
        [HttpPost, ActionName("ImportProcess")]
        [ValidateAntiForgeryToken]
        public IActionResult ImportProcess(ProcessImportViewModel processImportViewModel)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }
            int newOrder, initialOrder;
            var processID = processImportViewModel.ProcessID;
            var procedureID = processImportViewModel.ProcedureID;

            Procedure procedure = _context.Procedure.Single(t => t.ProcedureID == procedureID);
            Process process = _context.Process.Single(t => t.ProcessID == processID);

            ICollection<ProcessStep> processStepsToImport = _context.ProcessStep.Where(t => t.ProcessID == processID).ToList();

            if (processImportViewModel.AtEndOfProcedure)
            {
                newOrder = _context.ProcedureStep.Max(t => t.Order) + 1;
            }
            else if (processImportViewModel.AtStartOfProcedure)
            {
                newOrder = 1;
            }
            else if (processImportViewModel.AfterStep != null)
            {
                newOrder = processImportViewModel.AfterStep.Value + 1;
            }
            else
            {
                newOrder = 1;
            }
            initialOrder = newOrder;

            var ImportedProcedureStepIDs = new List<int>();
            foreach (var item in processStepsToImport)
            {
                ProcedureStep procedureStep = new ProcedureStep();
                procedureStep.ProcessStepID = item.ProcessStepID;
                procedureStep.ProcedureID = procedureID;

                var i = 1;
                var processName = "";
                do
                {
                    processName = Convert.ToString(i) + "_" + process.Name + "_" + item.StepID;
                    i = i + 1;
                } while (stepIDIsNotUniqueToProcedure(procedureID, processName));

                procedureStep.StepID = processName;
                procedureStep.Method = item.Method;
                procedureStep.Attribute = item.Attribute;
                procedureStep.Input = item.Input;
                procedureStep.Order = newOrder;
                procedureStep.Value = item.Value;
                procedureStep.Static = item.Static;

                _context.ProcedureStep.Add(procedureStep);

                _context.SaveChanges();

                ImportedProcedureStepIDs.Add(procedureStep.ProcedureStepID);
                newOrder = newOrder + 1;
            }

            var processInProcedure = new ProcessInProcedure();
            processInProcedure.ProcedureID = procedureID;
            processInProcedure.ProcessID = processID;
            _context.ProcessInProcedure.Add(processInProcedure);
            _context.SaveChanges();

            foreach (var item in ImportedProcedureStepIDs)
            {
                var procedureStepInProcessInProcedure = new ProcedureStepInProcessInProcedure();
                procedureStepInProcessInProcedure.ProcedureStepID = item;
                procedureStepInProcessInProcedure.ProcessInProcedureID = processInProcedure.ProcessInProcedureID;
                _context.ProcedureStepInProcessInProcedure.Add(procedureStepInProcessInProcedure);
            }
            _context.SaveChanges();

            reorderAroundImport(procedureID, ImportedProcedureStepIDs, initialOrder, newOrder);

            HttpContext.Session.SetString("Message", "Process: " + process.Name + " imported into Procedure: "
                + procedure.Name);

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Procedures",
                action = "Details",
                ID = procedure.ProcedureID
            }));
        }

        private string reorderAroundImport(int procedureID, List<int> ImportedProcedureStepIDs, int initialOrder,
            int firstStepToReorder)
        {
            var AllStepsInProcedure = _context.ProcedureStep.Where(t => t.ProcedureID == procedureID).ToList();

            var AllStepsToReorder = AllStepsInProcedure.Where(t => !ImportedProcedureStepIDs.Any
                (l => t.ProcedureStepID == l));

            int newOrder = firstStepToReorder;
            foreach (var item in AllStepsToReorder)
            {
                if (item.Order < initialOrder)
                {
                    //Don't need to do anything
                }
                else
                {
                    item.Order = newOrder;
                    newOrder = newOrder + 1;
                    _context.Update(item);
                }
            }
            _context.SaveChanges();

            return "Items successfully reordered";
        }

        [ActionName("DeleteProcessFromProcedure")]
        public IActionResult DeleteProcessFromProcedure(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ProcessInProcedure processInProcedure = _context.ProcessInProcedure.Single(t => t.ProcessInProcedureID == id);
            if (processInProcedure == null)
            {
                return HttpNotFound();
            }
            var process = _context.Process.Single(t => t.ProcessID == processInProcedure.ProcessID);
            var procedure = _context.Procedure.Single(t => t.ProcedureID == processInProcedure.ProcedureID);

            var ProcedureStepsToDeleteFromProcess = _context.ProcedureStepInProcessInProcedure.Where
                (t => t.ProcessInProcedureID == id);
            foreach (var item in ProcedureStepsToDeleteFromProcess)
            {
                _context.ProcedureStepInProcessInProcedure.Remove(item);
                var procedureStepInProcedure = _context.ProcedureStep.Single(t => t.ProcedureStepID == item.ProcedureStepID);
                _context.ProcedureStep.Remove(procedureStepInProcedure);
            }

            _context.ProcessInProcedure.Remove(processInProcedure);

            _context.SaveChanges();

            //After deleting steps, reorder the whole procedure by finding the min order and editing that to be 1
            ProcedureStep firstProcedureStep = _context.ProcedureStep.Where(t => t.ProcedureID == procedure.ProcedureID)
                .OrderBy(t => t.Order).First();

            ReorderStepsAroundEdit(firstProcedureStep.ProcedureStepID, 1);

            HttpContext.Session.SetString("Message", "Process: " + process.Name + " removed from Procedure: " + procedure.Name);

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Procedures",
                action = "Details",
                ID = processInProcedure.ProcedureID
            }));
        }

        [ActionName("DisassociateProcedureFromProcess")]
        public IActionResult DisassociateProcedureFromProcess(int? id, string redirect)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ProcessInProcedure processInProcedure = _context.ProcessInProcedure.Single(t => t.ProcessInProcedureID == id);
            if (processInProcedure == null)
            {
                return HttpNotFound();
            }
            var process = _context.Process.Single(t => t.ProcessID == processInProcedure.ProcessID);
            var procedure = _context.Procedure.Single(t => t.ProcedureID == processInProcedure.ProcedureID);

            var ProcedureStepsToDeleteFromProcess = _context.ProcedureStepInProcessInProcedure.Where
                (t => t.ProcessInProcedureID == id);
            foreach (var item in ProcedureStepsToDeleteFromProcess)
            {
                ProcedureStep procedureStep = _context.ProcedureStep.Single(t => t.ProcedureStepID == item.ProcedureStepID);
                procedureStep.ProcessStepID = null;
                _context.ProcedureStep.Update(procedureStep);
                _context.ProcedureStepInProcessInProcedure.Remove(item);
            }

            _context.ProcessInProcedure.Remove(processInProcedure);

            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Process: " + process.Name + " disassociated from Procedure: " + procedure.Name);

            if (redirect == "Process")
            {
                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Processes",
                    action = "Details",
                    ID = processInProcedure.ProcessID
                }));
            }
            else
            {
                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Procedures",
                    action = "Details",
                    ID = processInProcedure.ProcedureID
                }));
            }
        }

        [ActionName("UpdateProcedureFromProcess")]
        public IActionResult UpdateProcedureFromProcess(int? id, string redirect)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ProcessInProcedure processInProcedure = _context.ProcessInProcedure.Single(t => t.ProcessInProcedureID == id);
            if (processInProcedure == null)
            {
                return HttpNotFound();
            }
            var process = _context.Process.Single(t => t.ProcessID == processInProcedure.ProcessID);
            var procedure = _context.Procedure.Single(t => t.ProcedureID == processInProcedure.ProcedureID);

            var AllProcedureStepsInProcessList = _context.ProcedureStepInProcessInProcedure.Where
                (t => t.ProcessInProcedureID == processInProcedure.ProcessInProcedureID);

            foreach(var item in AllProcedureStepsInProcessList)
            {
                ProcedureStep procedureStep = _context.ProcedureStep.Single(t => t.ProcedureStepID == item.ProcedureStepID);
                if(procedureStep.ProcessStepID != null)
                {
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

            HttpContext.Session.SetString("Message", "Process: " + process.Name + " updated in Procedure: " + procedure.Name);

            if(redirect == "Process")
            {
                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Processes",
                    action = "Details",
                    ID = processInProcedure.ProcessID
                }));
            }
            else
            {
                return RedirectToAction("Details", new RouteValueDictionary(new
                {
                    controller = "Procedures",
                    action = "Details",
                    ID = processInProcedure.ProcedureID
                }));
            }
        }

        [ActionName("UpdateAllProcessesInProcedureFromProcess")]
        public IActionResult UpdateAllProcessesInProcedureFromProcess(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Procedure procedure = _context.Procedure.Single(t => t.ProcedureID == id);
            if (procedure == null)
            {
                return HttpNotFound();
            }

            var AllProcedureStepsInProcedure = _context.ProcedureStep.Where(t => t.ProcedureID == id);

            foreach (var item in AllProcedureStepsInProcedure)
            {
                ProcedureStep procedureStep = _context.ProcedureStep.Single(t => t.ProcedureStepID == item.ProcedureStepID);
                if (procedureStep.ProcessStepID != null)
                {
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

            HttpContext.Session.SetString("Message", "All steps in Procedure: " + procedure.Name + " updated from Process");

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                controller = "Procedures",
                action = "Details",
                ID = id
            }));
        }

        [HttpPost]
        public IActionResult DisplayOrHideStaticSteps(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Procedure procedure = _context.Procedure.Single(t => t.ProcedureID == id);
            if (procedure == null)
            {
                return HttpNotFound();
            }
            procedure.DisplayStaticSteps = !procedure.DisplayStaticSteps;

            _context.Update(procedure);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Static steps displayed for: " + procedure.Name);

            return RedirectToAction("ManageTestCasesForProcedure", new RouteValueDictionary(new
            {
                controller = "TestCases",
                action = "ManageTestCasesForProcedure",
                ID = procedure.ProcedureID
            }));
        }

    }
}
