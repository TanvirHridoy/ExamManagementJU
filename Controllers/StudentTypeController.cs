using EMSJu.ContextModels;
using EMSJu.Models;
using EMSJu.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSJu.Controllers
{
    [SessionTimeout]
    public class StudentTypeController : Controller
    {
        private readonly CertificateMSV2Context _Db;
        private string? Session;
        private EmpMenus menu = new EmpMenus();
        public StudentTypeController(CertificateMSV2Context Db, IHttpContextAccessor httpContext)
        {
            menu = httpContext.HttpContext.Session.GetMenu("User", "StudentTypeSetup");
            _Db = Db;
        }
        public IActionResult create()
        {
            if (menu.OPAdd==false) { return RedirectToAction("LogOut", "Login"); }
            return View();
        }
        public async Task<IActionResult> List(string message)
        {
            
            if (menu==null) { return RedirectToAction("LogOut", "Login"); }
            StudentTypeViewModel model = new StudentTypeViewModel();
            var list = await _Db.StudentTypes.ToListAsync();
            model.list = list;
            model.message = message;
            return View(model);
        }


        public async Task<IActionResult> Add(StudentType obj)
        {
            
            if (menu.OPAdd==false) { return RedirectToAction("LogOut", "Login"); }
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
            
            if (menu.OPDelete==false) { return RedirectToAction("LogOut", "Login"); }
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
           
            if (menu.OPEdit==false) { return RedirectToAction("LogOut", "Login"); }
            StudentType obj =await _Db.StudentTypes.SingleOrDefaultAsync(e => e.Id == id);
            return View(obj);

        }

        public async Task<IActionResult>Update(StudentType obj)
        {
            
            if (menu.OPEdit==false) { return RedirectToAction("LogOut", "Login"); }
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

    }
}
