using CertificationMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Controllers
{
    public class AdminController : Controller
    {

        private readonly deptcontext _Db;

        public AdminController(deptcontext Db)
        {
            _Db = Db;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Department(string message)
        {
            DepartmentViewModel model = new DepartmentViewModel();
            var deplist = await _Db.Department.ToListAsync();
            model.deptlist = deplist;
            model.message = message;
            return View(model);

        }

        public async Task<IActionResult> EditDepartment(int id)
        {
            department obj = await _Db.Department.SingleOrDefaultAsync(e => e.id == id);
            return View(obj);
        }

        public async Task<IActionResult> UpdateDepartment(department obj)
        {
            try
            {
                _Db.Entry(obj).State = EntityState.Modified;
                await _Db.SaveChangesAsync();
                return RedirectToAction("Department", new { message = "suucces " + obj.deptname });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "Failed to Update" + obj.deptname });
            }
        }



        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _Db.Department.FindAsync(id);
            try
            {
                _Db.Department.Remove(department);
                await _Db.SaveChangesAsync();
                return RedirectToAction("Department", new { message = "Succsefully Delete " + department.deptname });
            }
            catch(Exception ex)
            {
                return RedirectToAction("Department", new { message = " Delete failed " + department.deptname });
            }

            
        }


        public IActionResult create()
        {
           
            return View();
        }



        public async Task<IActionResult> AddPerson(department obj)
        {
           

            try
            {
                if (obj.id == 0)
                {
                    _Db.Department.Add(obj);
                    await _Db.SaveChangesAsync();

                }
                return RedirectToAction("Department", new { message = "Successfully Added " + obj.deptname });
            }
            catch (Exception Ex)
            {
                return RedirectToAction("Department", new { message = "Failed to add " + obj.deptname
                
              
           
                
                
      });
            }
        }


    }
}
