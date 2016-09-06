using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VientVendreMVC.Areas.Security.Models;

namespace VientVendreMVC.Areas.Security.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        public ServiceReference1.AdminServiceClient proxy;
        public RegisterController()
        {
             proxy = new ServiceReference1.AdminServiceClient();
        }


        // GET: Security/Register
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection form)
        {
            UserDTO userDTO = new UserDTO();
            userDTO.Address = Request.Form["adresse"].ToString();
            userDTO.UserLastName = Request.Form["prenom"].ToString();
            userDTO.UserName = Request.Form["nom"].ToString();
            userDTO.Email = Request.Form["email"].ToString();
            userDTO.Password = Request.Form["password"].ToString();
            userDTO.PhoneNumber = int.Parse(Request.Form["phone"].ToString());
            userDTO.IsApproved = "P";
            
            userDTO.DateOfBirth = DateTime.Now;
            proxy.CreateUser(userDTO);
            
            return View();
        }
    }
}