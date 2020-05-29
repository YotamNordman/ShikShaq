using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ShikShaq.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            // USER IS LOGGED OUT!!
            HttpContext.Session.SetInt32("userId",-1);
            HttpContext.Session.SetString("userName", "");
            HttpContext.Session.SetString("isUserAdmin", "N");

            return RedirectToAction("Index", "Home");
        }
    }
}