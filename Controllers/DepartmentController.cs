using CertificationMS.ContextModels;
using CertificationMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CertificationMS.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly CertificateMSContext _Db;

        public DepartmentController(CertificateMSContext Db)
        {
            _Db = Db;
        }
       


        public async Task<IActionResult> List(string message)
        {
            DepartmentViewModel model = new DepartmentViewModel();
            var deplist = await _Db.Departments.ToListAsync();
            model.deptlist = deplist;
            model.message = message;
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Department obj = await _Db.Departments.SingleOrDefaultAsync(e => e.Id == id);
            return View(obj);
        }

        public async Task<IActionResult> Update(Department obj)
        {
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
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
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
