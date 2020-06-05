﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShikShaq.Data;
using WebApplication1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

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
                    // Create the user claims
                    List<Claim> Claims = new List<Claim>();
                    Claims.Add(new Claim(ClaimTypes.Sid, user.Id.ToString()));
                    Claims.Add(new Claim(ClaimTypes.DateOfBirth, user.Birthday.ToString()));
                    Claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    Claims.Add(new Claim(ClaimTypes.StreetAddress, user.Address));
                    if (user.IsAdmin == "Y")
                    {
                        //Create the identity for the user, But as an Admin
                        Claims.Add(new Claim(ClaimTypes.Role , "Admin"));
                        Claims.Add(new Claim(ClaimTypes.Name , "Admin"));
                    }
                    else
                    {
                        //Create the identity for the user, But as a regular customer. User role will be used for everything besides the admin
                        Claims.Add(new Claim(ClaimTypes.Role , "User"));
                        Claims.Add(new Claim(ClaimTypes.Name , user.Name));
                    }
                    ClaimsIdentity identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
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

            // Clearing the Authentication Cookies
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Orders()
        {
            int? userId = HttpContext.Session.GetInt32("userId");
            List<Order> orders = new List<Order>();

            if (userId != null && userId >= 0)
            {
                var userOrderQuery = from order in _context.Order
                                     where order.User.Id == userId
                                     select order;

                orders = await userOrderQuery
                    .Include(o => o.Branch)
                    .Include(o => o.ProductInOrders).ThenInclude(pio => pio.Product)
                    .ToListAsync();
            }

            return View(orders);
        }

    }
}