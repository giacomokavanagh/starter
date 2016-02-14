using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace Starter.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Control multiple Selenium C# servers from one location.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Some help would be available to those that know where to look.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
