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
using VientVendreMVC.MappingViewModelToDTO;
using VientVendreMVC.ServiceReference1;

namespace VientVendreMVC.Areas.User.Controllers
{
    [Authorize(Roles = "A,U")]
    public class AddOrEditMyAnnonceController : Controller
    {

        // variables
        private AdminServiceClient proxy;
        private AnnonceDTO _annonce;
        private ProductDTO _product;
        private MapClient MapClient;
        private ProductViewModel productVM;



        //constructors
        public AddOrEditMyAnnonceController()
        {
            proxy = new AdminServiceClient();
            _annonce = new AnnonceDTO();
            _product = new ProductDTO();            
            MapClient = new MapClient();
            productVM = new ProductViewModel();
            MappingAnnonceViewModelAndAnnonceDTO mappingAnnonceVMToDto = new MappingAnnonceViewModelAndAnnonceDTO();
        }


        // GET: User/AddOrEditMyAnnonce
        [HttpGet]
        public ActionResult Index()
        {
            AnnonceViewModel annVM = new AnnonceViewModel();

            foreach(var reg in proxy.GetAllRegion())
            {
                annVM.Regions.Add(reg.RegionName);
            }

           TempData["NumberOfPhoto"] = 0;
            return View(annVM);
        }

        
        //Post Method for annonce
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AnnonceViewModel annonce,FormCollection form)
        {
            TempData["annonceName"] = annonce.AnnonceTitle;
            try
            {
                if (ModelState.IsValid)
                {

                    _annonce = MapClient.AnnonceVMAndDTOMapping.MapAnnonceViewModelToAnnonceDTO(annonce);

                    int iduser = proxy.GetAllUser().Where(u => u.Email == User.Identity.Name).FirstOrDefault().UserID;
                    _annonce.UserDTOID = iduser;//must be mapped with connected user

                    int i = 0;
                    foreach(string key in form)
                    {
                        TempData["NbreProduct"] = annonce.NombreProduct;
                        TempData["TotalProduct"] = annonce.NombreProduct;
                        var value = key;
                        if(i>=4)
                        {
                            _annonce.RegionsDTOID.Add(int.Parse(value));
                        }
                        i++;
                    }

                    TempData["annonceDTO"] = _annonce;    
                    return RedirectToAction("CreateProduct",new {nbreProduct=annonce.NombreProduct }); 
                }
            }
            catch(Exception ex)
            {
                throw; 
            }
            return RedirectToAction("CreateProduct");
           // return View();
        }

        

        /////////////////////////////////////////////////////////////////////////////////////
        //                                 NEW                                             //
        /////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public ActionResult CreateProduct(int nbreProduct)
        {
            ProductViewModel prodVM = new ProductViewModel();
            TempData["NbreProduct"] = nbreProduct;
            foreach(var cat in proxy.GetAllCategory())
            {
                prodVM.Categories.Add(cat.CategoryName);
            }
            
            if (TempData["ProductNumber"] == null)
                TempData["ProductNumber"] = 0;
            else
            {
                int p = int.Parse(TempData["ProductNumber"].ToString());
                p++;
                TempData["ProductNumber"] = p;
            }

            return View(prodVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(ProductViewModel productVM,FormCollection form)
        {
 
            int a = (int)TempData["NbreProduct"];//renvoyer la page create product autant de nombre de produits déclarer dans la page annonce.

            if (TempData["ProductVm"] != null)
            {
                productVM = TempData["ProductVm"] as ProductViewModel;
            }

            if (a > 0)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        _product.CategoriesID = new List<int>();
                        int i = 0;
                        _product = MapClient.ProductVMAndDTOMapping.MapProductViewModelToProductDTO(productVM);
                        _product.IsApproved = "P"; //l'etat devrai etre toujours confirmé du produits.
                        foreach (string key in form)
                        {
                            var value = key;
                            if (i >= 4)
                            {
                                int r;
                               if( int.TryParse(value, out r)!= false) 
                                _product.CategoriesID.Add(r);
                            }
                            i++;
                        }
                        _annonce = (AnnonceDTO)TempData["annonceDTO"];
                        // _product = MapClient.ProductVMAndDTOMapping.MapProductViewModelToProductDTO(productVM);
                        _annonce.Products.Add(_product);
                        TempData["annonceDTO"] = _annonce;
                        a--;
                        TempData["NbreProduct"] = a;
                        if (a == 0)
                        {
                            proxy.CreateAnnonce(_annonce); //add annonce after finish operation  

                            int TotalProduct = int.Parse(TempData["TotalProduct"].ToString());
                            int NumberOfPhoto = int.Parse(TempData["NumberOfPhoto"].ToString());

                            for (int j = 0; j <= TotalProduct; j++)
                                {
                                    for (int k = 0; k <= NumberOfPhoto; k++)
                                    {
                                        PhotoInfoDTO photoDTO = new PhotoInfoDTO();
                                        if (TempData["Photo" + j.ToString() + "_" + k.ToString()] != null)
                                            {
                                                photoDTO = TempData["Photo" + j.ToString() + "_" + k.ToString()] as PhotoInfoDTO;
                                                proxy.CreatePhotoInfo(photoDTO);
                                            }
                                    }

                                }
                              
                            
                        }
                        return RedirectToAction("CreateProduct", new { nbreProduct = a });
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                return RedirectToAction("CreateProduct");
            }
            else
                
            return RedirectToAction("CreateProduct",new { nbreProduct = a });
            
        }




       
        /////////////////////////////////////////////////////////////////////////////////////
         // upload photo to server
        ////////////////////////////////////////////////////////////////////////////////
       
      

        [HttpPost]
        public void Upload(HttpPostedFileBase file)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var filej = Request.Files[i];

                var fileNamej = Path.GetFileName(filej.FileName);
                file = filej;
             
            }
                    
            string result = UploadFileToService(file);

            ////////add photo info to database
            PhotoInfoDTO photoDTO = new PhotoInfoDTO();
            photoDTO.PhotoName = file.FileName;
            photoDTO.UploadedOn = DateTime.Now;

            int k = int.Parse(TempData["NumberOfPhoto"].ToString());
          


            int NbrePrduct = int.Parse(TempData["TotalProduct"].ToString());
            TempData["TotalProduct"] = NbrePrduct;
         
            int p = int.Parse(TempData["ProductNumber"].ToString());
            TempData["ProductNumber"] = p;
            
            int OrderProduct = p+1;

            if (proxy.GetAllProduct().Count() != 0)
                photoDTO.ProductID = proxy.GetAllProduct().Last().ProductID + OrderProduct;
            else
                photoDTO.ProductID = 1;

            //photoDTO.ProductID = proxy.GetAllProduct().Last().ProductID + OrderProduct;
            TempData["Photo" + OrderProduct.ToString() + "_" + k.ToString()] = photoDTO;

            k++;
            TempData["NumberOfPhoto"] = k;
            //display result in the view       
            ViewBag.Result = result;

        }




        private string UploadFileToService(HttpPostedFileBase file)
        {

            PhotoInfoDTO photoDTO = new PhotoInfoDTO();
            ///////add photoInfo to database
            photoDTO.PhotoName = file.FileName;
            photoDTO.UploadedOn = DateTime.Now;

 

            ///////////////////////////////////
            string annonceName = TempData["annonceName"] as string;
            TempData["annonceName"] = annonceName;
            string url = ConfigurationManager.AppSettings["serviceUrl"];
            var user = proxy.GetAllUser().Where(u => u.Email == User.Identity.Name).FirstOrDefault();
            string requestUrl = string.Format("{0}/FileUpload/{1}/{2}/{3}", url, user.UserName, annonceName, file.FileName);
            
            TempData["fileName"] = file.FileName; //pour la récuperation du nom du ficher ajouter

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

        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////::
        /// get photo from server
        /// //////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>



        private Byte[] GetPhotos()
        {
            try
            {
                //recuperation du nom a partir du tempdata:
                string fileName=TempData["fileName"] as string;
                string annonceName = TempData["annonceName"] as string;
                TempData["annonceName"] = annonceName;
                // Create the REST request.
                string url = ConfigurationManager.AppSettings["serviceUrl"];
                string requestUrl = string.Format("{0}/GetImage/{1}/{2}/{3}", url, "c", annonceName, fileName);

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