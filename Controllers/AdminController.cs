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
                return RedirectToAction("Index", new { message = "Successfully Added" + obj.deptname });
            }
            catch (Exception Ex)
            {
                return RedirectToAction("Index", new { message = "Failed to add" + obj.deptname
                
              
           
                
                
      });
            }
        }


    }
}
