using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VientVendreMVC.Areas.User.Models;

namespace VientVendreMVC.Areas.User.Controllers
{
    [Authorize(Roles = "A,U")]
    public class ListMyAnnonceController : Controller
    {

        private ServiceReference1.AdminServiceClient proxy = new ServiceReference1.AdminServiceClient();
        // GET: User/ListMyAnnonce
        public ActionResult Index(string Page)
        {

            List<AnnonceViewModel> listAnnonceVM = new List<AnnonceViewModel>();
             foreach(var ann in proxy.GetAnnonceByUser(1))//get user id before
            {
                AnnonceViewModel annVM = new AnnonceViewModel();
                annVM.AnnonceTitle = ann.AnnonceTitle;
                annVM.Description = ann.Description;

                foreach (var reg in ann.Regions)
                    annVM.Regions.Add(reg.RegionName);
                listAnnonceVM.Add(annVM);
            }

            ///////////////////////////
            //for paging
            ViewBag.TotalPages = Math.Ceiling(listAnnonceVM.Count() / 10.0);
            int page = int.Parse(Page == null ? "1" : Page);
            ViewBag.Page = page;
            listAnnonceVM = (listAnnonceVM.Skip((page - 1) * 10).Take(10)).ToList();
            ///////////////////////////
            return View(listAnnonceVM);
        }
    }
}