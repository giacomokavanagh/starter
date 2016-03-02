using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Starter.Models;
#if DNX451
using NBitcoin;
#endif
using Microsoft.AspNet.Http;
using System;
using Microsoft.AspNet.Authorization;

namespace Starter.Controllers
{
    [Authorize]
    public class MasterKeysController : Controller
    {
        private ApplicationDbContext _context;

        public MasterKeysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MasterKeys
        public IActionResult Index()
        {
            ViewData["Message"] = HttpContext.Session.GetString("Message");
            HttpContext.Session.Remove("Message");

            var MasterKeysList = _context.MasterKey.ToList();
            ViewData["MasterKeyExists"] = MasterKeysList.Any();
            if (MasterKeysList.Any())
            {
                MasterKeysList.First().DerivedKeys = _context.DerivedKey.Where
                    (t => t.MasterKeyID == MasterKeysList.First().MasterKeyID).ToList(); ;

                var listOfUsedTestRunnerIDs = _context.DerivedKey.Where(t => t.TestRunnerID != null).Select(t => t.TestRunnerID).ToList();
                var availableTestRunners = _context.TestRunner.Where(t => !listOfUsedTestRunnerIDs.Contains(t.TestRunnerID));

                ViewBag.ValidTestRunners = new SelectList(availableTestRunners, "TestRunnerID", "Name");
                foreach(var item in MasterKeysList.First().DerivedKeys)
                {
                    if(item.TestRunnerID != null)
                    {
                        item.TestRunner = _context.TestRunner.Single(t => t.TestRunnerID == item.TestRunnerID);
                    }
                }
            }

            return View(MasterKeysList);
        }

        // GET: MasterKeys/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            MasterKey masterKey = _context.MasterKey.Single(m => m.MasterKeyString == id);
            if (masterKey == null)
            {
                return HttpNotFound();
            }

            return View(masterKey);
        }

        // GET: MasterKeys/Create
        public IActionResult Create()
        {
            if (_context.MasterKey.Any())
            {
                HttpContext.Session.SetString("Message", "There can be only 1 Master Key");
                return View("Index");
            }
#if DNX451
            ExtKey masterKey = new ExtKey();
            ViewData["GeneratedMasterKeyString"] = Convert.ToBase64String(masterKey.ToBytes());
#endif
            return View();
        }

        // POST: MasterKeys/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MasterKey masterKey)
        {
            if (_context.MasterKey.Any())
            {
                HttpContext.Session.SetString("Message", "There can be only 1 Master Key");
                return View("Index");
            }

            if (ModelState.IsValid)
            {
                _context.MasterKey.Add(masterKey);
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Master Key Created");
                return RedirectToAction("Index");
            }
            return View(masterKey);
        }

        public IActionResult CreateNewDerivedKey()
        {
            var MasterKey = _context.MasterKey.First();
            var byteArrayedMasterKey = Convert.FromBase64String(MasterKey.MasterKeyString);

            //Find out how many keys have already been created
            var DerivedKeyList = _context.DerivedKey;

            DerivedKey derivedKey = new DerivedKey();

#if DNX451
            ExtKey newMasterKey = new ExtKey(byteArrayedMasterKey);
            ExtKey key = newMasterKey.Derive((uint)(DerivedKeyList.Count() + 1));
            var derivedKeyString = key.ToString(Network.Main);
            derivedKey.DerivedKeyString = derivedKeyString;
#endif

            derivedKey.MasterKeyID = MasterKey.MasterKeyID;

            _context.DerivedKey.Add(derivedKey);
            _context.SaveChanges();
            HttpContext.Session.SetString("Message", "Derived Key Created");

            return RedirectToAction("Index");
        }

        public IActionResult DeleteDerivedKey(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            DerivedKey derivedKey = _context.DerivedKey.Single(m => m.DerivedKeyID == id);
            if (derivedKey == null)
            {
                return HttpNotFound();
            }

            _context.DerivedKey.Remove(derivedKey);
            _context.SaveChanges();

            HttpContext.Session.SetString("Message", "Derived Key Deleted: " + derivedKey.DerivedKeyString);
            return RedirectToAction("Index");
        }

        // GET: MasterKeys/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            MasterKey masterKey = _context.MasterKey.Single(m => m.MasterKeyString == id);
            if (masterKey == null)
            {
                return HttpNotFound();
            }
            return View(masterKey);
        }

        // POST: MasterKeys/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MasterKey masterKey)
        {
            if (ModelState.IsValid)
            {
                _context.Update(masterKey);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(masterKey);
        }

        // GET: MasterKeys/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            MasterKey masterKey = _context.MasterKey.Single(m => m.MasterKeyID == id);
            if (masterKey == null)
            {
                return HttpNotFound();
            }

            return View(masterKey);
        }

        // POST: MasterKeys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            MasterKey masterKey = _context.MasterKey.Single(m => m.MasterKeyID == id);
            HttpContext.Session.SetString("Message", "Master Key Deleted");
            _context.MasterKey.Remove(masterKey);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public string SetTestRunner(int? id, int value)
        {
            DerivedKey derivedKey = _context.DerivedKey.Single(t => t.DerivedKeyID == id);
            TestRunner testRunner = _context.TestRunner.Single(t => t.TestRunnerID == value);

            if (_context.DerivedKey.Any(t =>t.TestRunnerID == value))
            {
                return "That test runner is already in use";
            }

            string strReturnMessage;

            if (value == -1)
            {
                derivedKey.TestRunnerID = null;
                strReturnMessage = derivedKey.DerivedKeyString + " key no longer in use";
            }
            else
            {
                derivedKey.TestRunnerID = value;
                strReturnMessage = derivedKey.DerivedKeyString + " assigned to " + testRunner.Name;
            }

            _context.Update(derivedKey);
            _context.SaveChanges();

            return strReturnMessage;
        }
    }
}