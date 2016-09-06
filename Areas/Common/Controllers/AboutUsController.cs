using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VientVendreMVC.Areas.Common.Controllers
{
    public class AboutUsController : Controller
    {
        // GET: Common/AboutUs
        public ActionResult Index()
        {
            return View();
        }
    }
}