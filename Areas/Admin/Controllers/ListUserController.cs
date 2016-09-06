using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VientVendreMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class ListUserController : Controller
    {
        // GET: Admin/ListUser
        public ActionResult Index()
        {
            return View();
        }
    }
}