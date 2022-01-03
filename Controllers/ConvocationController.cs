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
    public class ConvocationController : Controller
    {
        public readonly CertificateMSV2Context _Db;
        private string? Session;
        private EmpMenus menu = new EmpMenus();
        public ConvocationController(CertificateMSV2Context Db)
        {
            _Db = Db;
        }


        public async Task<IActionResult> List(string message)
        {
            try
            {
                menu = HttpContext.Session.GetMenu("User", "ConvocationSetup");
            }
            catch
            {
                return RedirectToAction("LogIn", "Login");
            }
            if (menu==null) { return RedirectToAction("LogIn", "Login"); }
            ConvocationViewModel model = new ConvocationViewModel();
            var Cnvctlist= await _Db.Convocations.ToListAsync();
            model.list = Cnvctlist;
            model.message = message;
            return View(model);

        }

        public IActionResult create()
        {
            try
            {
                menu = HttpContext.Session.GetMenu("User", "ConvocationSetup");
            }
            catch
            {
                return RedirectToAction("LogIn", "Login");
            }
            return View();
        }
        public async Task<IActionResult> Add(Convocation obj)
        {
            try
            {
                menu = HttpContext.Session.GetMenu("User", "ConvocationSetup");
            }
            catch
            {
                return RedirectToAction("LogIn", "Login");
            }
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
            try
            {
                menu = HttpContext.Session.GetMenu("User", "ConvocationSetup");
            }
            catch
            {
                return RedirectToAction("LogIn", "Login");
            }
            Convocation obj = await _Db.Convocations.SingleOrDefaultAsync(e=>e.Id==id);
            return View(obj);
        }
        public async Task<IActionResult> Update(Convocation obj)
        {
            try
            {
                menu = HttpContext.Session.GetMenu("User", "ConvocationSetup");
            }
            catch
            {
                return RedirectToAction("LogIn", "Login");
            }
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
            try
            {
                menu = HttpContext.Session.GetMenu("User", "ConvocationSetup");
            }
            catch
            {
                return RedirectToAction("LogIn", "Login");
            }
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
