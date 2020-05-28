using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShikShaq.Controllers
{
    public class LoginController : Controller
    {

        private ShikShaqContext _context;

        public LoginController ()
        {
            _context = new ShikShaqContext();
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(user loginUser)
        {
           
            using (_context)
            {
                var user = (from usr in _context.user
                            where usr.email == loginUser.email && usr.password == loginUser.password
                            select usr)
                            .FirstOrDefault<user>();

                if (user == null)
                {
                    ModelState.AddModelError("UserNotExist", "User does not exist!");
                } else
                {
                    Session["userId"] = user.id;
                    return RedirectToAction("Index", "Home");
                }
            }



            return View("Index");
        }
    }
}