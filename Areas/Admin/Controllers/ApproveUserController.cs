using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VientVendreMVC.Areas.Security.Models;

namespace VientVendreMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class ApproveUserController : Controller
    {
        private ServiceReference1.AdminServiceClient proxy;

        public ApproveUserController()
        {
            proxy = new ServiceReference1.AdminServiceClient();
        }
        // GET: Admin/AddOrEditUser
        [HttpGet]
        public ActionResult Index(string status)
        {
            ViewBag.Status = (status == null ? "P" : status);
            List<UserViewModel> listUserstVM = new List<UserViewModel>();
            if (status == null)
            {
                status = "P";
            }
            foreach (var user in proxy.GetAllUser().Where(p => p.IsApproved == status))
            {
                UserViewModel userVM = new UserViewModel();
                userVM.UserID = user.UserID;
                userVM.LastName = user.UserLastName;
                userVM.DateOfBirth = user.DateOfBirth;
                userVM.Address = user.Address;
                userVM.Email = user.Email;                //prodVM.Description =  have to add description in ProdutDTO !!!
                userVM.Sex = user.sex;
                userVM.IsApproved = user.IsApproved;
                listUserstVM.Add(userVM);
            }
            return View(listUserstVM);
        }

        public ActionResult Approve(int id)
        {
            try
            {
                var userDTO = proxy.GetUserByID(id);
                userDTO.IsApproved = "A";
                proxy.UpdateUser(userDTO);
                TempData["Msg"] = "Approved Successfully";

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Approval Failed" + ex.Message;
                return RedirectToAction("Index");
            }

        }


        public ActionResult Reject(int id)
        {
            try
            {
                var userDTO = proxy.GetUserByID(id);
                userDTO.IsApproved = "R";
                proxy.UpdateUser(userDTO);
                TempData["Msg"] = "Rejected Successfully";

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Reject Failed" + ex.Message;
                return RedirectToAction("Index");
            }

        }
    }
}