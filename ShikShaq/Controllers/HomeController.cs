using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShikShaq.Data;
using ShikShaq.Logic;
using ShikShaq.Models;

namespace ShikShaq.Controllers
{
    public class HomeController : Controller
    {

        private readonly ShikShaqContext _context;
        private ShikShaqContextInitializer dbInitializer;

        public HomeController (ShikShaqContext context)
        {
            _context = context;

            dbInitializer = new ShikShaqContextInitializer();
            dbInitializer.Initialize(_context);
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Products");
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
