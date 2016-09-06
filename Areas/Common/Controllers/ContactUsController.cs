using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using VientVendreMVC.Areas.Common.Models;

namespace VientVendreMVC.Areas.Common.Controllers
{
    public class ContactUsController : Controller
    {
        // GET: Common/ContactUs
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] //to allow html tag 
        public ActionResult Index(ContactUsViewModel contactUs )
        {
            string smtpAddress = "smtp.mail.yahoo.com";
            int portNumber = 587;
            bool enableSSL = true;

            string emailFrom = contactUs.Email;
            string password = contactUs.Password;
            string emailTo = "robinio016@gmail.com";
            string subject = contactUs.Subject;
            string body = contactUs.Description;

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
            catch(Exception ex)
            {
                ViewBag.Result = "Erreur : "+ex.Message;
            }
            return View();
        }
    }
}