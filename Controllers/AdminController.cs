using CertificationMS.Models;
using CertificationMS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Controllers
{
    public class AdminController : Controller
    {
        private LoggedInModel LoginResp = new LoggedInModel();

        private string? Session;
        // GET: AdminController
        public ActionResult Index()
        {
            try
            {
                LoginResp= HttpContext.Session.GetObject<LoggedInModel>("User");
            }
            catch
            {
                return RedirectToAction("LogOut", "Login");
            }
           
            return View();
        }

        public ActionResult RetriveImage()
        {
            try
            {
                LoginResp= HttpContext.Session.GetObject<LoggedInModel>("User");
                
            }
            catch
            {
                return RedirectToAction("LogOut", "Login");
            }
            var file = LoginResp.empInfo.Photo;

            if (file == null)
            {
                return null;
            }
            else if (file.Length > 0)
            {
                byte[] Image = file;
                return File(Image, "image/jpg");
            }
            else
            {
                return null;
            }
        }
    }
}
