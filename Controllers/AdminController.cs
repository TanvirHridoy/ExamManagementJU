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
        private string? Session;
        // GET: AdminController
        public ActionResult Index()
        {
            Session = HttpContext.Session.GetString("northern");
            if (Session == String.Empty || Session == null)
            {
                return RedirectToAction("LogIn", "Login");
            }
            return View();
        }
    }
}
