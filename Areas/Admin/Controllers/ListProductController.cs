using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VientVendreMVC.Areas.Admin.Controllers
{
    public class ListProductController : Controller
    {
        // GET: Admin/ListProduct
        public ActionResult Index()
        {
            return View();
        }
    }
}