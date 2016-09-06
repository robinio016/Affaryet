using DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VientVendreMVC.Areas.User.Models;
using VientVendreMVC.ServiceReference1;

namespace VientVendreMVC.Areas.User.Controllers
{
    [Authorize(Roles = "A,U")]
    public class AddOrEditMyProductController : Controller
    {
        private ProductDTO _productDTO;
        private ServiceReference1.AdminServiceClient proxy;
        public AddOrEditMyProductController()
        {
            _productDTO = new ProductDTO();
            proxy = new ServiceReference1.AdminServiceClient();
        }
        // GET: User/AddOrEditMyProduct
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ProductViewModel product )
        { 
            if(ModelState.IsValid)
            {
                _productDTO.ProductName = product.ProductName;
                _productDTO.ProductPrice = product.ProductPrice;
                //Annonce ann = new Annonce();
                
                  

                //ann = (Annonce)TempData["annonce"];
                _productDTO.Annonce.AnnonceID = proxy.GetAllAnnonce().Last().AnnonceID;

                

                proxy.CreateProduct(_productDTO);
                // ann.Description = des;

            }

            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            string result = UploadFileToService(file);
            //display result in the view       
            ViewBag.Result = result;
            return View("Index");
        }

        private string UploadFileToService(HttpPostedFileBase file)
        {
            


            string url = ConfigurationManager.AppSettings["serviceUrl"];
            string requestUrl = string.Format("{0}/FileUpload/{1}/{2}/{3}", url, "c", "c", file.FileName);

            ///////////////
            //var uri = new Uri(url);
            var uri = new Uri(requestUrl);

            //create HttpWebRequest
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.ContentType = "application/octet-stream";
            request.Method = WebRequestMethods.Http.Post;

            //set request stream (Content)
            using (var requestStream = request.GetRequestStream())
            {
                byte[] fileDataInByte = null;
                using (BinaryReader binaryReader = new BinaryReader(file.InputStream))
                {
                    fileDataInByte = binaryReader.ReadBytes(file.ContentLength);
                }
                requestStream.Write(fileDataInByte, 0, fileDataInByte.Length);
            }

            //Call REST and get a response back
            using (var response = request.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

    }
}