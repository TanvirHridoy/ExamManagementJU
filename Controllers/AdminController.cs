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
