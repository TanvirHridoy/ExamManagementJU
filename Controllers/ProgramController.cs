using CertificationMS.ContextModels;
using CertificationMS.Models;
using CertificationMS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Controllers
{
    public class ProgramController : Controller
    {
        private readonly CertificateMSV2Context _Db;
        private string? Session;
        private EmpMenus menu = new EmpMenus();
        public ProgramController(CertificateMSV2Context Db)
        {
            _Db = Db;
        }

        public async Task<IActionResult> List(string message)
        {
            try
            {
                menu = HttpContext.Session.GetMenu("User", "ProgramSetup");
            }
            catch
            {
                return RedirectToAction("LogIn", "Login");
            }
            if (menu==null) { return RedirectToAction("LogIn", "Login"); }
            ProgramViewModel model = new ProgramViewModel();
            var list = await _Db.Programs.ToListAsync();
            model.list = list;
            model.message = message;
            return View(model);
        }


        public IActionResult Create()
        {
            try
            {
                menu = HttpContext.Session.GetMenu("User", "ProgramSetup");
            }
            catch
            {
                return RedirectToAction("LogIn", "Login");
            }
            if (menu.OPAdd==false) { return RedirectToAction("LogIn", "Login"); } 
            return View();
        }


        public async Task<IActionResult> Add(CertificationMS.ContextModels.Program obj)
        {
            try
            {
                menu = HttpContext.Session.GetMenu("User", "ProgramSetup");
            }
            catch
            {
                return RedirectToAction("LogIn", "Login");
            }
            if (menu.OPAdd==false) { return RedirectToAction("LogIn", "Login"); }
            try
            {
                _Db.Programs.Add(obj);
                await _Db.SaveChangesAsync();
                return RedirectToAction("List", new
                {
                    message = "Successfully Added " + obj.ProgramName

                });

            }
            catch (Exception)
            {
                return RedirectToAction("List", new
                {
                    message = "Failed to add " + obj.ProgramName

                });
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                menu = HttpContext.Session.GetMenu("User", "ProgramSetup");
            }
            catch
            {
                return RedirectToAction("LogIn", "Login");
            }
            if (menu.OPDelete==false) { return RedirectToAction("LogIn", "Login"); }
            var item = await _Db.Programs.FindAsync(id);
            try
            {
                _Db.Programs.Remove(item);
                await _Db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "Successfully Delete " + item.ProgramName });
            }
            catch
            {
                return RedirectToAction("List", new { message = " Failed to Delete " + item.ProgramName });

            }

        }
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                menu = HttpContext.Session.GetMenu("User", "ProgramSetup");
            }
            catch
            {
                return RedirectToAction("LogIn", "Login");
            }
            if (menu.OPEdit==false) { return RedirectToAction("LogIn", "Login"); }
            ContextModels.Program  obj = await _Db.Programs.SingleOrDefaultAsync(e => e.Id == id);
            return View(obj);
        }
        public async Task<IActionResult> Update(ContextModels.Program obj)
        {
            try
            {
                menu = HttpContext.Session.GetMenu("User", "ProgramSetup");
            }
            catch
            {
                return RedirectToAction("LogIn", "Login");
            }
            if (menu.OPEdit==false) { return RedirectToAction("LogIn", "Login"); }
            try
            {
                _Db.Entry(obj).State = EntityState.Modified;
                await _Db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "Successfully Updated " + obj.ProgramName });
            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = "Failed to Update" + obj.ProgramName });
            }
        }


    }
}
