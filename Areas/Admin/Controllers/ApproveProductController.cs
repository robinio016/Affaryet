using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VientVendreMVC.Areas.Admin.Models;
using VientVendreMVC.Areas.User.Models;

namespace VientVendreMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class ApproveProductController : Controller
    {
        private ServiceReference1.AdminServiceClient proxy;
        public ApproveProductController()
        {
            proxy = new ServiceReference1.AdminServiceClient();
        }
        // GET: Admin/AddOrEditProduct
        [HttpGet]
        public ActionResult Index(string status)
        {
            ViewBag.Status = (status == null ? "P" : status);
            List<ProductViewModel> listProductVM = new List<ProductViewModel>();
            if(status == null)
            {
                status = "P";
            }
            foreach (var prod in proxy.GetAllProduct().Where(p=>p.IsApproved==status))
            {
                ProductViewModel prodVM = new ProductViewModel();
                prodVM.ProductID = prod.ProductID;
                prodVM.ProductName = prod.ProductName;
                prodVM.IsApproved = prod.IsApproved;
                //prodVM.Description =  have to add description in ProdutDTO !!!
                prodVM.ProductPrice = prod.ProductPrice;

                listProductVM.Add(prodVM);
            }
            return View(listProductVM);
        }

        public ActionResult Approve(int id)
        {
            try
            {
                var prodDTO = proxy.GetProductByID(id);
                prodDTO.IsApproved = "A";
                proxy.UpdateProduct(prodDTO);
                TempData["Msg"] = "Approved Successfully";

                return RedirectToAction("Index");
                
            }
            catch(Exception ex)
            {
                TempData["Msg"] = "Approval Failed" + ex.Message;
                return RedirectToAction("Index");
            }
            
        }


        public ActionResult Reject(int id)
        {
            try
            {
                var prodDTO = proxy.GetProductByID(id);
                prodDTO.IsApproved = "R";
                proxy.UpdateProduct(prodDTO);
                TempData["Msg"] = "Rejected Successfully";

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Reject Failed" + ex.Message;
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public void ApproveOrRejectAll(List<int> Ids,string status)
        {
            proxy.ApproveOrRejectAll(Ids.ToArray(), status);
        }

        public ActionResult Delete(int id)
        {
            //supprimer les photos avant le produit
            foreach(var photo in proxy.GetAllPhotoInfo().Where(p=>p.ProductID == id))
            {
                proxy.DeletePhotoInfo(photo.PhotoInfoID);
            }
            proxy.DeleteProduct(id); 
            return RedirectToAction("Index");
        }
    }

}