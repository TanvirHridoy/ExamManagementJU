using CertificationMS.ContextModels;
using CertificationMS.Models;
using CertificationMS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Controllers
{
    [SessionTimeout]
    public class ConvocationController : Controller
    {
        public readonly CertificateMSV2Context _Db;
        private string? Session;
        private EmpMenus menu = new EmpMenus();
        public ConvocationController(CertificateMSV2Context Db, IHttpContextAccessor HttpContext)
        {
            menu = HttpContext.HttpContext.Session.GetMenu("User", "ConvocationSetup");

            _Db = Db;
        }


        public async Task<IActionResult> List(string message)
        {
            if (menu==null) { return RedirectToAction("LogOut", "Login"); }
            ConvocationViewModel model = new ConvocationViewModel();
            var Cnvctlist= await _Db.Convocations.ToListAsync();
            model.list = Cnvctlist;
            model.message = message;
            return View(model);

        }

        public IActionResult create()
        {
            
            return View();
        }
        public async Task<IActionResult> Add(Convocation obj)
        {
            
            try
            {
                _Db.Convocations.Add(obj);
               await _Db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "Successfully Added" + obj.Name });


            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = "Failed To Add" + obj.Name });
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (menu.OPEdit!) { return RedirectToAction("Logout", "Login"); }
            Convocation obj = await _Db.Convocations.SingleOrDefaultAsync(e=>e.Id==id);
            return View(obj);
        }
        public async Task<IActionResult> Update(Convocation obj)
        {
            
            if (menu.OPEdit!) { return RedirectToAction("Logout", "Login"); }
            try
            {
                _Db.Entry(obj).State = EntityState.Modified;
                await _Db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "Successfully Added" + obj.Name });

            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = "Failed" + obj.Name });
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            if (menu.OPDelete!) { return RedirectToAction("Logout", "Login"); }
            var convocation = await _Db.Convocations.FindAsync(id);
            try
            {
                _Db.Convocations.Remove(convocation);
                await _Db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "Successfully Deleted" });
            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = "Failed to deleted"});
            }
        }



        
    }
}
