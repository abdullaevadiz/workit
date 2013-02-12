using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Entities;

namespace Site.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Users/

        [HttpGet]
        public ActionResult Login()
        {
            return View(new User { Email = string.Empty, Password = string.Empty });
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            return View(user);
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
