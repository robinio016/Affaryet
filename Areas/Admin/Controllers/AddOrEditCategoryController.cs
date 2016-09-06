using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VientVendreMVC.Areas.Admin.Models;

namespace VientVendreMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class AddOrEditCategoryController : Controller
    {
        // GET: Admin/AddOrEditCategory
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(CategoryViewModel category)
        {
            return View();
        }
    }
}