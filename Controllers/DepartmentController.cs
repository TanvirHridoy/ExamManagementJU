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
    [SessionTimeout]
    public class DepartmentController : Controller
    {

        private readonly CertificateMSV2Context _Db;
        private EmpMenus menu = new EmpMenus();
        public DepartmentController(CertificateMSV2Context Db, IHttpContextAccessor HttpContext)
        {
            menu = HttpContext.HttpContext.Session.GetMenu("User", "DepartmentSetup");

            _Db = Db;
        }
        public async Task<IActionResult> List(string message)
        {
            
            if (menu==null) { return RedirectToAction("LogOut", "Login"); }
            DepartmentViewModel model = new DepartmentViewModel();
            var deplist = await _Db.Departments.ToListAsync();
            model.deptlist = deplist;
            model.message = message;
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if(menu.OPEdit==false){ return RedirectToAction("Logout", "Login"); }
            Department obj = await _Db.Departments.SingleOrDefaultAsync(e => e.Id == id);
            return View(obj);
        }

        public async Task<IActionResult> Update(Department obj)
        {
            if (menu.OPEdit==false) { return RedirectToAction("Logout", "Login"); }

            try
            {
                _Db.Entry(obj).State = EntityState.Modified;
                await _Db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "Successfully Added " + obj.DeptName });
            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = "Failed to Update" + obj.DeptName });
            }
        }

        public IActionResult Create()
        {
            if (menu.OPAdd==false) { return RedirectToAction("Logout", "Login"); }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (menu.OPDelete==false) { return RedirectToAction("Logout", "Login"); }

            var department = await _Db.Departments.FindAsync(id);
            try
            {
                _Db.Departments.Remove(department);
                await _Db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "Successfully Delete " + department.DeptName });
            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = "  Failed Delete " + department.DeptName });
            }
        }
        public async Task<IActionResult> Add(Department obj)
        {
            if (menu.OPAdd==false) { return RedirectToAction("Logout", "Login"); }
            try
            {
                if (obj.Id == 0)
                {
                    _Db.Departments.Add(obj);
                    await _Db.SaveChangesAsync();

                }
                return RedirectToAction("List", new { message = "Successfully Added " + obj.DeptName });
            }
            catch (Exception)
            {
                return RedirectToAction("List", new
                {
                    message = "Failed to add " + obj.DeptName

                });
            }
        }


    }
}
