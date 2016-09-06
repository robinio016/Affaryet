using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VientVendreMVC.Areas.Security.Models;

namespace VientVendreMVC.Areas.Security.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Security/Login
        public ActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel login)
        {
            try
            {
                if (Membership.ValidateUser(login.Email, login.Password))
                {
                    FormsAuthentication.SetAuthCookie(login.Email, false);
                    return RedirectToAction("Index", "Home", new { area = "Common" });
                }
                else
                {
                    TempData["Msg"] = "Login Failed";
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                TempData["Msg"] = "Login Failed" + ex;
                return RedirectToAction("Index");
            }

        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", new { area = "Common" });
        }
    }
}