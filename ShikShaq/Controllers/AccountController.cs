using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShikShaq.Data;
using WebApplication1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ShikShaq.Controllers
{
    public class AccountController : Controller
    {
        private readonly ShikShaqContext _context;

        public AccountController(ShikShaqContext context)
        {
            _context = context;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(User loginUser)
        {

            using (_context)
            {
                var user = await (from usr in _context.User
                                  where usr.Email == loginUser.Email && usr.Password == loginUser.Password
                                  select usr)
                                  .FirstOrDefaultAsync<User>();

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

        public IActionResult Logout()
        {
            // USER IS LOGGED OUT!!
            HttpContext.Session.SetInt32("userId", -1);
            HttpContext.Session.SetString("userName", "");
            HttpContext.Session.SetString("isUserAdmin", "N");

            return RedirectToAction("Index", "Home");
        }

    }
}