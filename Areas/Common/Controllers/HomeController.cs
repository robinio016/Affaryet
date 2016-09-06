using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VientVendreMVC.Areas.Common.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private ServiceReference1.AdminServiceClient proxy = null;

        public HomeController()
        {
            proxy = new ServiceReference1.AdminServiceClient();
        }
        // GET: Common/Home
        public ActionResult Index()
        {
            
            var categories = proxy.GetAllCategory().Select(s=>s.CategoryName).Distinct();
            ViewBag.categories = categories;

            var regions = proxy.GetAllRegion().Select(s => s.RegionName).Distinct();
            ViewBag.regions = regions;

            return View();
        }


    }
}