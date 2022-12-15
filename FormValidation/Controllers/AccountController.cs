using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FormValidation.Models;
using System.Web.Security;

namespace FormValidation.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Membership membership)
        {
            using (var context=new OfficeDataEntities())
            {
                bool isValid = context.User.Any(x => x.UserName == membership.UserName && x.Password == membership.Password);

                if (isValid==true)
                {
                    FormsAuthentication.SetAuthCookie(membership.UserName, false);
                    return RedirectToAction("Index", "Employees");
                }

                ModelState.AddModelError("","Invalid Username or Password");
                return View();
            }
        }


        public ActionResult SignUp()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                using (var context = new OfficeDataEntities())
                {
                    context.User.Add(user);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}