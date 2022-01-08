using CertificationMS.ContextModels;
using CertificationMS.Models;
using CertificationMS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CertificationMS.Controllers
{
    [SessionTimeout]
    public class CampusController : Controller
    {
        private readonly CertificateMSV2Context _Db;
        private string? Session;
        private EmpMenus menu = new EmpMenus();

        public CampusController(CertificateMSV2Context Db, IHttpContextAccessor HttpContext)
        {
            menu = HttpContext.HttpContext.Session.GetMenu("User", "CampusSetup");

            _Db = Db;
        }

        public IActionResult create()
        {
           
            if (menu.OPAdd==false) { return RedirectToAction("LogOut", "Login"); }
            return View();
        }

        public async Task<IActionResult> Add(Campus obj)
        {
           
            if (menu.OPAdd==false) { return RedirectToAction("LogOut", "Login"); }
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
            
            if (menu==null) { return RedirectToAction("LogOut", "Login"); }
            CampusViewModel model = new CampusViewModel();
            var Campuslist = await _Db.Campuses.ToListAsync();
            model.list = Campuslist;
            model.message = message;
            return View(model);

        }

        public async Task<IActionResult> Edit(int id)
        {
            if (menu.OPEdit==false) { return RedirectToAction("LogOut", "Login"); }
            Campus obj = await _Db.Campuses.SingleOrDefaultAsync(e => e.Id == id);
            return View(obj);
        }

        public async Task<IActionResult> Update(Campus obj)
        {
            
            if (menu.OPEdit==false) { return RedirectToAction("LogOut", "Login"); }
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
            if (menu.OPDelete==false) { return RedirectToAction("LogOut", "Login"); }
            var campus = await _Db.Campuses.FindAsync(id);
            try
            {
                _Db.Campuses.Remove(campus);
                await _Db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "Successfully Deleted " + campus.CampusName });

            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = "Failed To Delete " + campus.CampusName });
            }

        }
    }
}
