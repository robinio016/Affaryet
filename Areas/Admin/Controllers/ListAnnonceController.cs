using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VientVendreMVC.Areas.Admin.Controllers
{
    public class ListAnnonceController : Controller
    {
        // GET: Admin/ListAnnonce
        public ActionResult Index()
        {
            return View();
        }
    }
}