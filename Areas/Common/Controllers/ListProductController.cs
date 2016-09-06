using DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VientVendreMVC.Areas.User.Models;

namespace VientVendreMVC.Areas.Common.Controllers
{
    [AllowAnonymous]
    public class ListProductController : Controller
    {
        private ServiceReference1.AdminServiceClient proxy;
        public ListProductController()
        {
            proxy = new ServiceReference1.AdminServiceClient();
        }
        // GET: Common/ListProduct
        public ActionResult Index(string Page)
        {
            List<ProductViewModel> listproductVM = new List<ProductViewModel>();
            foreach (var prodDTO in proxy.GetAllProduct().Where(p => p.IsApproved == "A"))
            {
                var photo = proxy.GetAllPhotoInfo().Where(p => p.ProductID == prodDTO.ProductID).FirstOrDefault();
                ProductViewModel pVM = new ProductViewModel();
                ////////////////////////////////////////
                int idAnn = proxy.GetProductByID(prodDTO.ProductID).AnnonceDTOID;
                string annName = proxy.GetAnnonceByID(idAnn).AnnonceTitle;
                int idUser = proxy.GetAnnonceByID(idAnn).UserDTOID.Value;
                string userName = proxy.GetUserByID(idUser).UserName;
                ////////////////////////////////////////
                pVM.ProductName = prodDTO.ProductName;
                pVM.ProductPrice = prodDTO.ProductPrice;
                pVM.ProductID = prodDTO.ProductID;
                
                if(photo != null)
                    pVM.im = GetPhotos(userName, annName, photo.PhotoName);
                //if(photo.Description!=null)
                //    pVM.Description = photo.Description;

                listproductVM.Add(pVM);
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

        public ActionResult GetProductByCategory(string id,string Page)
        {
            List<ProductViewModel> listproductVM = new List<ProductViewModel>();
            foreach (var prodDTO in proxy.GetProductByCategory(int.Parse(id==null?"1":id)))
            {
                var photo = proxy.GetAllPhotoInfo().Where(p => p.ProductID == prodDTO.ProductID).FirstOrDefault();
                ProductViewModel pVM = new ProductViewModel();
                pVM.ProductName = prodDTO.ProductName;
                pVM.ProductPrice = prodDTO.ProductPrice;
                pVM.ProductID = prodDTO.ProductID;
                //////////////////////////////////////
                //get annonces name and username folder
                ////////////////////////////////////////
                int idAnn = proxy.GetProductByID(prodDTO.ProductID).AnnonceDTOID;
                
                string annName = proxy.GetAnnonceByID(idAnn).AnnonceTitle;
                int idUser = proxy.GetAnnonceByID(idAnn).UserDTOID.Value;
                string userName = proxy.GetUserByID(idUser).UserName;
                ////////////////////////////////////////
                if (photo != null)
                    pVM.im = GetPhotos(userName, annName, photo.PhotoName);
                //if(photo.Description!=null)
                //    pVM.Description = photo.Description;

                listproductVM.Add(pVM);
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

        public ActionResult GetProductByRegion(string id,string Page)
        {
            ViewBag.IdRegion = id;
            List<ProductViewModel> listproductVM = new List<ProductViewModel>();
            foreach (var prodDTO in proxy.GetProductByRegion(int.Parse(id == null ? "1" : id)))
            {
                var photo = proxy.GetAllPhotoInfo().Where(p => p.ProductID == prodDTO.ProductID).FirstOrDefault();
                ProductViewModel pVM = new ProductViewModel();
                pVM.ProductName = prodDTO.ProductName;
                pVM.ProductPrice = prodDTO.ProductPrice;
                pVM.ProductID = prodDTO.ProductID;
                //////////////////////////////////////
                //get annonces name and username folder
                ////////////////////////////////////////
                int idAnn = proxy.GetProductByID(prodDTO.ProductID).AnnonceDTOID;
                string annName = proxy.GetAnnonceByID(idAnn).AnnonceTitle;
                int idUser = proxy.GetAnnonceByID(idAnn).UserDTOID.Value;
                string userName = proxy.GetUserByID(idUser).UserName;
                ////////////////////////////////////////
                if (photo != null)
                    pVM.im = GetPhotos(userName, annName, photo.PhotoName);
                //if(photo.Description!=null)
                //    pVM.Description = photo.Description;

                listproductVM.Add(pVM);
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


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //je pense getProdcut(srting nomProduit,int idCategory,in idRegion) suffirait afin de ne pas ajouter beacoup de View////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //public ActionResult GetProductByRegion()
        //{
        //    return View();
        //}

        //public ActionResult GetProductByCategory()
        //{
        //    return View();
        //}


        private Byte[] GetPhotos(string UserName,string ProductName,string PhotoName)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchEngine(string Page,FormCollection form)
        {
            string value="";
            foreach (var key in form.Keys)
            {
                 value = form[key.ToString()];
                // etc.
            }
            List<ProductDTO> listProduct = new List<ProductDTO>();
            if (!String.IsNullOrEmpty(value))
            {
                foreach(var p in proxy.GetAllProduct().Where(p=>p.IsApproved=="A"))
                {if(p.ProductName != null)
                    {
                        if (p.ProductName.Contains(value))
                        {
                            listProduct.Add(p);
                        }
                    }
                   
                }

            }

            List<ProductViewModel> listproductVM = new List<ProductViewModel>();
            foreach (var prodDTO in listProduct)
            {
                var photo = proxy.GetAllPhotoInfo().Where(p => p.ProductID == prodDTO.ProductID).FirstOrDefault();
                ProductViewModel pVM = new ProductViewModel();
                ////////////////////////////////////////
                int idAnn = proxy.GetProductByID(prodDTO.ProductID).AnnonceDTOID;
                string annName = proxy.GetAnnonceByID(idAnn).AnnonceTitle;
                int idUser = proxy.GetAnnonceByID(idAnn).UserDTOID.Value;
                string userName = proxy.GetUserByID(idUser).UserName;
                ////////////////////////////////////////
                pVM.ProductName = prodDTO.ProductName;
                pVM.ProductPrice = prodDTO.ProductPrice;
                pVM.ProductID = prodDTO.ProductID;

                if (photo != null)
                    pVM.im = GetPhotos(userName, annName, photo.PhotoName);
                //if(photo.Description!=null)
                //    pVM.Description = photo.Description;

                listproductVM.Add(pVM);
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
    }
}