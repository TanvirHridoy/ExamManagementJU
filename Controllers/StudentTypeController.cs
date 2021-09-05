using CertificationMS.ContextModels;
using CertificationMS.Models;
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


        private readonly CertificateMSContext _Db;

        public StudentTypeController(CertificateMSContext Db)
        {
            _Db = Db;
        }
        public IActionResult create()
        {
            return View();
        }
        public async Task<IActionResult> List(string message)
        {

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
            StudentType obj =await _Db.StudentTypes.SingleOrDefaultAsync(e => e.Id == id);
            return View(obj);

        }

        public async Task<IActionResult>Update(StudentType obj)
        {
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
            return View();
        }
    }
}
