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
    public class ApprovalStatusController : Controller
    {
        public readonly CertificateMSV2Context _db;
        private EmpMenus menu = new EmpMenus();
        public ApprovalStatusController(CertificateMSV2Context Db, IHttpContextAccessor HttpContext)
        {
            menu = HttpContext.HttpContext.Session.GetMenu("User", "StatusSetup");

            _db = Db;
        }

        public async Task<IActionResult> List(string message)
        {
            if (menu==null) { return RedirectToAction("LogOut", "Login"); }
            ApvStatusViewModel model = new ApvStatusViewModel();
            var ApvStatusList = await _db.ApvStatuses.ToListAsync();
            model.list = ApvStatusList;
            model.message = message;
            return View(model);
        }


        public IActionResult create()
        {
            
            if (menu.OPAdd==false) { return RedirectToAction("LogOut", "Login"); }
            return View();
        }


        public async Task<IActionResult> Add(ApvStatus obj)
        {
           
            if (menu.OPAdd==false) { return RedirectToAction("LogOut", "Login"); }
            try
            {
                _db.ApvStatuses.Add(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "Successfully Added " + obj.Name });

            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = "Failed to Add " + obj.Name });
            }

        }

        public async Task<IActionResult> Edit(int id)
        {
           
            if (menu.OPEdit==false) { return RedirectToAction("LogOut", "Login"); }
            ApvStatus obj = await _db.ApvStatuses.SingleOrDefaultAsync(e => e.Id == id);
            return View(obj);
        }

        public async Task<IActionResult> Update(ApvStatus obj)
        {
          
            if (menu.OPEdit==false) { return RedirectToAction("LogOut", "Login"); }
            try
            {
                _db.Entry(obj).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "Successfully Added" + obj.Name });
            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = "Failed" + obj.Name });
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            
            if (menu.OPDelete==false) { return RedirectToAction("LogOut", "Login"); }
            var apstatus = await _db.ApvStatuses.FindAsync(id);
            try
            {
                _db.ApvStatuses.Remove(apstatus);
                await _db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "SuccessfullyDelete " });
            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = " Failed to delete " });
            }
        }
    }
}


