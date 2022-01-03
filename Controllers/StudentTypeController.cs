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
    public class StudentTypeController : Controller
    {
        private readonly CertificateMSV2Context _Db;
        private string? Session;
        private EmpMenus menu = new EmpMenus();
        public StudentTypeController(CertificateMSV2Context Db)
        {
            _Db = Db;
            
        }
        public IActionResult create()
        {
            try
            {
                menu = HttpContext.Session.GetMenu("User", "StudentTypeSetup");
            }
            catch
            {
                return RedirectToAction("LogIn", "Login");
            }
            if (menu.OPAdd==false) { return RedirectToAction("LogIn", "Login"); }
            return View();
        }
        public async Task<IActionResult> List(string message)
        {
            try
            {
                menu = HttpContext.Session.GetMenu("User", "StudentTypeSetup");
            }
            catch
            {
                return RedirectToAction("LogIn", "Login");
            }
            if (menu==null) { return RedirectToAction("LogIn", "Login"); }
            StudentTypeViewModel model = new StudentTypeViewModel();
            var list = await _Db.StudentTypes.ToListAsync();
            model.list = list;
            model.message = message;
            return View(model);
        }


        public async Task<IActionResult> Add(StudentType obj)
        {
            try
            {
                menu = HttpContext.Session.GetMenu("User", "StudentTypeSetup");
            }
            catch
            {
                return RedirectToAction("LogIn", "Login");
            }
            if (menu.OPAdd==false) { return RedirectToAction("LogIn", "Login"); }
            try
            {
                _Db.StudentTypes.Add(obj);
                await _Db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "Successfully Added " + obj.Type });        
            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = "Failed" + obj.Type });
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                menu = HttpContext.Session.GetMenu("User", "StudentTypeSetup");
            }
            catch
            {
                return RedirectToAction("LogIn", "Login");
            }
            if (menu.OPDelete==false) { return RedirectToAction("LogIn", "Login"); }
            var st= await _Db.StudentTypes.FindAsync(id);
            try
            {
                _Db.StudentTypes.Remove(st);
                await _Db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "Successfully Delete " + st.Type });
            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = "  Failed Delete " + st.Type });
            }
        }
         public async Task<IActionResult> Edit(int id)
        {
            try
            {
                menu = HttpContext.Session.GetMenu("User", "StudentTypeSetup");
            }
            catch
            {
                return RedirectToAction("LogIn", "Login");
            }
            if (menu.OPEdit==false) { return RedirectToAction("LogIn", "Login"); }
            StudentType obj =await _Db.StudentTypes.SingleOrDefaultAsync(e => e.Id == id);
            return View(obj);

        }

        public async Task<IActionResult>Update(StudentType obj)
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
                return RedirectToAction("List", new { message = "Successfully Updated" });
            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = "Failed Updated" });
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
