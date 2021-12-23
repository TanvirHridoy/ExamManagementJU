using CertificationMS.ContextModels;
using CertificationMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Controllers
{
    public class CampusController : Controller
    {
        private readonly CertificateMSV2Context _Db;
        private string? Session;
        private EmpMenus menu = new EmpMenus();

        public CampusController(CertificateMSV2Context Db)
        {
            _Db = Db;
            if (HmsConst.LoginResp != null)
            {
                menu = HmsConst.LoginResp.EmpMenuList.Where(e => e.MenuName == "CampusSetup").SingleOrDefault();
            }
        }

        public IActionResult create()
        {
            Session = HttpContext.Session.GetString("northern");
            if (Session == String.Empty || Session == null)
            {
                return RedirectToAction("LogIn", "Login");
            }
            if (menu.OPAdd==false) { return RedirectToAction("LogIn", "Login"); }
            return View();
        }

        public async Task<IActionResult> Add(Campus obj)
        {
            Session = HttpContext.Session.GetString("northern");
            if (Session == String.Empty || Session == null)
            {
                return RedirectToAction("LogIn", "Login");
            }
            if (menu.OPAdd==false) { return RedirectToAction("LogIn", "Login"); }
            try
            {
                _Db.Campuses.Add(obj);
                await _Db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "Successfully Added" + obj.CampusName });

            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = "Falied to Added" + obj.CampusName });
            }
       
            
        }
       public async Task<IActionResult> List(string message)
        {
            Session = HttpContext.Session.GetString("northern");
            if (Session == String.Empty || Session == null)
            {
                return RedirectToAction("LogIn", "Login");
            }
            if (menu==null) { return RedirectToAction("LogIn", "Login"); }
            CampusViewModel model = new CampusViewModel();
            var Campuslist = await _Db.Campuses.ToListAsync();
            model.list = Campuslist;
            model.message = message;
            return View(model);
            
        }


        public async Task<IActionResult> Edit(int id)
        {
            Session = HttpContext.Session.GetString("northern");
            if (Session == String.Empty || Session == null)
            {
                return RedirectToAction("LogIn", "Login");
            }
            if (menu.OPEdit==false) { return RedirectToAction("LogIn", "Login"); }
            Campus obj = await _Db.Campuses.SingleOrDefaultAsync(e => e.Id == id);
            return View(obj);
        }

        public async Task<IActionResult> Update(Campus obj)
        {
            Session = HttpContext.Session.GetString("northern");
            if (Session == String.Empty || Session == null)
            {
                return RedirectToAction("LogIn", "Login");
            }
            if (menu.OPEdit==false) { return RedirectToAction("LogIn", "Login"); }
            try
            {
                _Db.Entry(obj).State = EntityState.Modified;
                await _Db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "Successfully Update " + obj.CampusName });
            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = "Failed to Update" + obj.CampusName });
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            Session = HttpContext.Session.GetString("northern");
            if (Session == String.Empty || Session == null)
            {
                return RedirectToAction("LogIn", "Login");
            }
            if (menu.OPDelete==false) { return RedirectToAction("LogIn", "Login"); }
            var campus = await _Db.Campuses.FindAsync(id);
            try
            {
                _Db.Campuses.Remove(campus);
                await _Db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "Successfully Deleted " + campus.CampusName });

            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = "   Failed  To Delete " + campus.CampusName });
            }

        }

        

        public IActionResult Index()
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
