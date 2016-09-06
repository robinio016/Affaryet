using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VientVendreMVC.Areas.Admin.Models;

namespace VientVendreMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class ListCategoryController : Controller
    {
        private ServiceReference1.AdminServiceClient proxy;
        public ListCategoryController()
        {
            proxy = new ServiceReference1.AdminServiceClient();
        }
        // GET: Admin/ListCategory
        public ActionResult Index()
        {

            List<CategoryViewModel> catVMS = new List<CategoryViewModel>();
            foreach(var category in proxy.GetAllCategory())
            {
                CategoryViewModel catVM = new CategoryViewModel();
                catVM.CategoryName = category.CategoryName;
                catVMS.Add(catVM);             
            }

            return View(catVMS);
        }
    }
}