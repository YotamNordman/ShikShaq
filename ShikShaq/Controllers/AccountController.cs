using System;
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
using Microsoft.AspNetCore.Authorization;

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
            return RedirectToAction("Index", "Products");
        }

        [Authorize(Roles = "User,Admin")]
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

        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> Cart()
        {
            int? userId = HttpContext.Session.GetInt32("userId");
            List<CartItem> cartItems = new List<CartItem>();

            if (userId != null && userId >= 0)
            {
                var userCartItemsQuery =   from item in _context.CartItem
                                            where item.User.Id == userId
                                            select item;

                cartItems = await userCartItemsQuery
                    .Include(ci => ci.Product)
                    .ToListAsync();
            }

            float totalPrice = 0;
            foreach (CartItem ci in cartItems)
            {
                totalPrice += (ci.Product.Price * ci.Quantity);
            }

            ViewBag.totalPrice = totalPrice;
            List<Branch> branches = await _context.Branch.ToListAsync();

            Tuple <List<CartItem>, List<Branch>> tuple = new Tuple <List<CartItem>, List<Branch>> (cartItems, branches);
            return View(tuple);
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(User userRegistration)
        {
            ViewBag.SignUpErrorMessage = "";

            if(ModelState.IsValid)
            {
                if (userRegistration.Email == null || userRegistration.Email.Equals(""))
                {
                    ViewBag.SignUpErrorMessage = "Email cannot be empty!";
                    return View();
                }

                var userFindQuery = from usr in _context.User
                                    where usr.Email == userRegistration.Email
                                    select usr;

                User existUser = await userFindQuery.FirstOrDefaultAsync();

                if (existUser != null)
                {
                    ViewBag.SignUpErrorMessage = "User with this email alreay exist! you must sign up with another one.";
                    return View();
                }

                userRegistration.IsAdmin = "N";
                return await InsertUser(userRegistration);
            }

            return View();
        }

        private async Task<ActionResult> InsertUser(User user)
        {
            try
            {
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            catch (Exception e)
            {
                ViewBag.SignUpErrorMessage = "Something happened while trying to create the user!";
                return View("SignUp");
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveCart([FromBody] List<CartItem> cartItems)
        {
            int? userId = HttpContext.Session.GetInt32("userId");

            if (userId != null)
            {
                try
                {
                    RemoveAllUserCartItem(userId);

                    foreach (CartItem ci in cartItems)
                    {
                        ci.UserId = userId.GetValueOrDefault();
                        _context.CartItem.Add(ci);
                    }
                    
                    await _context.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status200OK);
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
          
        }

        [HttpPost]
        public async Task<ActionResult> CheckoutOrder([FromBody] Order order)
        {
            int? userId = HttpContext.Session.GetInt32("userId");

            if (userId != null)
            {
                try
                {
                    order.OrderDate = DateTime.Now;
                    order.UserId = userId.GetValueOrDefault();
                    _context.Order.Add(order);

                    RemoveAllUserCartItem(userId);

                    await _context.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status200OK);
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

        }

        private void RemoveAllUserCartItem(int? userId)
        {
            var removeOldCart = from ci in _context.CartItem
                                where ci.UserId == userId
                                select ci;

            _context.CartItem.RemoveRange(removeOldCart);
        }


    }
}