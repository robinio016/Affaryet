using DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using VientVendreMVC.Areas.Common.Models;

namespace VientVendreMVC.Areas.Common.Controllers
{
    [AllowAnonymous]
    public class ProductInfoController : Controller
    {

        private ServiceReference1.AdminServiceClient proxy = new ServiceReference1.AdminServiceClient();
        // GET: Common/ProductInfo
        public ActionResult Index(int id)
        {
            TempData["prod"] = id;
            List<ProductViewModel> ProductsVM = new List<ProductViewModel>();

            var product=proxy.GetProductByID(id);
            foreach(var photoInfo in proxy.GetAllPhotoInfo().Where(p=>p.ProductID == id))
            {
                ProductViewModel ProductVM = new ProductViewModel();
                
                //TempData["productName"] = product.ProductName; plustot nom annonce
                TempData["fileName"] = photoInfo.PhotoName;
                //il faut mapper le id de user dans le dto partie services
                int idAnn=proxy.GetProductByID(id).AnnonceDTOID;
                string annName = proxy.GetAnnonceByID(idAnn).AnnonceTitle;
                TempData["productName"] = annName;
                int idUser = proxy.GetAnnonceByID(idAnn).UserDTOID.Value;
                string userName = proxy.GetUserByID(idUser).UserName;
                
                ProductVM.im = GetPhotos(userName, annName, photoInfo.PhotoName);
                ProductsVM.Add(ProductVM);
            }
            ///////////////////////get comment///////////////////////
            //consult the last product to get the comment from it 
            //just a logic not good but !!
            //////here i get also user information and some others information to display to user
            ProductViewModel LastProductVM = new ProductViewModel();
            var prod = proxy.GetProductByID(id);
            
            var ann = proxy.GetAnnonceByID(prod.AnnonceDTOID);
            ////////get regions
            var regs = ann.Regions;
            
            foreach(var reg in regs)
            {
                LastProductVM.regionsName.Add(reg.RegionName);
            }
            //////////
            var user = proxy.GetUserByID(ann.UserDTOID.Value);
            LastProductVM.OwnerUserPhone = user.PhoneNumber;
            LastProductVM.OwnerUserAddress = user.Address;
            LastProductVM.OwnerUserEmail = user.Email;
            LastProductVM.ProductPrice = prod.ProductPrice;
            LastProductVM.Description = ann.Description; //i use annpnce but i shall add description in product,BOL,DTO,VM...
            foreach (var c in proxy.GetAllComment().Where(c => c.ProductID == id))
            {
                LastProductVM.comm.Add(c);
                int k = 1;
                k = c.UserID.Value;
                string username = proxy.GetUserByID(k).UserName;
                

                LastProductVM.UserName.Add(username);
                
            }
            ProductsVM.Add(LastProductVM);
            ///////////////////////////////////////////////////////////





            return View(ProductsVM);
        }

        /// <summary>
        /// get photo from server
        /// </summary>
        /// <returns></returns>

        private byte[] GetPhotos(string userName, string annName, string PhotoName)
        {
            try
            {
                //recuperation du nom a partir du tempdata:
                string fileName = PhotoName;//TempData["fileName"] as string;               

                 string productName=TempData["productName"] as string;             

                // Create the REST request.
                string url = ConfigurationManager.AppSettings["serviceUrl"];
                string requestUrl = string.Format("{0}/GetImage/{1}/{2}/{3}", url,userName, annName, fileName);

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



        /////////////////////////add comment/////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(FormCollection form)
        {
            CommentUserProdDTO com = new CommentUserProdDTO();
            com.Comment = form.Keys[1].ToString();
            string value = "";
            foreach (var key in form.Keys)
            {
                value = form[key.ToString()];
                // etc.
            }
            com.Comment = value;
            com.PostDate = DateTime.Now;
            com.UserID=proxy.GetAllUser().Where(u => u.Email == User.Identity.Name).FirstOrDefault().UserID;
            if(TempData["prod"] != null)
            {
                com.ProductID = int.Parse(TempData["prod"].ToString());
                TempData["prod"] = com.ProductID;
            }
            else
            {
                com.ProductID = 1117;//can eleminate this coondition 1117 correspond to an existing prod
            }
            proxy.createComment(com);
            return RedirectToAction("Index", new { id=com.ProductID.ToString()});
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        
        public ActionResult ContactUser(FormCollection form)
        {
            string idprod = "1";
            if (TempData["prod"]!=null)
            {
                idprod = TempData["prod"].ToString();
            }

            var prod = proxy.GetProductByID(int.Parse(idprod));
            var ann = proxy.GetAnnonceByID(prod.AnnonceDTOID);
            
            var user = proxy.GetUserByID(ann.UserDTOID.Value);

            ////////////////////
            string smtpAddress = "smtp.mail.yahoo.com";
            int portNumber = 587;
            bool enableSSL = true;

            string emailFrom = "robinio016@gmail.com";
            string password = "classmate";
            string emailTo = user.Email;
            
            string subject = Request.Form["Subject"];
            string body = form["Description"];

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                    ViewBag.Result = "Message envoyé avec succès";
                }

            }
            catch (Exception ex)
            {
                ViewBag.Result = "Erreur : " + ex.Message;
            }
            ///////////////////


            return RedirectToAction("Index", new { id = idprod });
        }
    }
}