using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;
using VientVendreMVC.Areas.User.Models;

namespace VientVendreMVC.Areas.User.Controllers
{
    [Authorize(Roles = "A,U")]
    public class ListMyProductController : Controller
    {
        private ServiceReference1.AdminServiceClient proxy;
        public ListMyProductController()
        {
            proxy = new ServiceReference1.AdminServiceClient();
        }


        // GET: User/ListMyProduct
        public ActionResult Index(string Page)
        {
            List<ProductViewModel> listproductVM = new List<ProductViewModel>();
            var userDTO = proxy.GetAllUser().Where(u => u.Email == User.Identity.Name).FirstOrDefault();
            int userID = userDTO.UserID;
            List<int> annIDS = new List<int>();
            foreach(var ann in proxy.GetAllAnnonce().Where(a=>a.UserDTOID==userID))
            {
                annIDS.Add(ann.AnnonceID);
            }

            foreach(var annid in annIDS)
            {
                var annName = proxy.GetAnnonceByID(annid).AnnonceTitle;
                foreach (var prodDTO in proxy.GetProductByAnnonce(annid))
                {
                    var photo = proxy.GetAllPhotoInfo().Where(p => p.ProductID == prodDTO.ProductID).FirstOrDefault();
                    ProductViewModel pVM = new ProductViewModel();
                    pVM.ProductName = prodDTO.ProductName;
                    pVM.ProductPrice = prodDTO.ProductPrice;
                    pVM.ProductID = prodDTO.ProductID;
                    if (photo != null)
                        pVM.im = GetPhotos(userDTO.UserName, annName, photo.PhotoName);
                    //if(photo.Description!=null)
                    //    pVM.Description = photo.Description;

                    listproductVM.Add(pVM);
                }
            }
            

            ///////////////////////////
            //for paging
            ViewBag.TotalPages = Math.Ceiling(listproductVM.Count() / 10.0);
            int page = int.Parse(Page == null ? "1" : Page);
            ViewBag.Page = page;
            listproductVM = (listproductVM.Skip((page - 1) * 10).Take(10)).ToList();
            ///////////////////////////
            return View(listproductVM);

        }

        private Byte[] GetPhotos(string UserName, string ProductName, string PhotoName)
        {
            try
            {

                // Create the REST request.
                string url = ConfigurationManager.AppSettings["serviceUrl"];
                string requestUrl = string.Format("{0}/GetImage/{1}/{2}/{3}", url, UserName, ProductName, PhotoName);

                WebRequest request = WebRequest.Create(requestUrl);
                // Get response  
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            stream.CopyTo(ms);
                            return ms.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}