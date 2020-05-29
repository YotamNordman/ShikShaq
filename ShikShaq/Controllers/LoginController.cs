using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShikShaq.Data;
using WebApplication1.Models;
using Microsoft.AspNetCore.Http;

namespace ShikShaq.Controllers
{
    public class LoginController : Controller
    {
        private readonly ShikShaqContext _context;

        public LoginController(ShikShaqContext context)
        {
            _context = context;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User loginUser)
        {

            using (_context)
            {
                var user = (from usr in _context.User
                            where usr.Email == loginUser.Email && usr.Password == loginUser.Password
                            select usr)
                            .FirstOrDefault<User>();

                if (user == null)
                {
                    ViewBag.LoginErrorMessage = "User does not exist!";
                }
                else
                {
                    // USER IS LOGGED IN!!
                    HttpContext.Session.SetInt32("userId", user.Id);
                    HttpContext.Session.SetString("userName", user.Name);
                    HttpContext.Session.SetString("isUserAdmin", user.IsAdmin);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }
    }
}