using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VientVendreMVC.Areas.Admin.Models;

namespace VientVendreMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class AddOrEditRegionController : Controller
    {
        // GET: Admin/AddOrEditRegion
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(RegionViewModel region)
        {
            return View();
        }
    }
}