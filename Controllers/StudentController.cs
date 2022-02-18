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
    public class StudentController : Controller
    {
        private readonly CertificateMSV2Context _Db;
        private string? Session;
        private EmpMenus menu = new EmpMenus();

        public StudentController(CertificateMSV2Context Db, IHttpContextAccessor HttpContext)
        {
            menu = HttpContext.HttpContext.Session.GetMenu("User", "Student");

            _Db = Db;
        }
        // GET: StudentController
        public async  Task<ActionResult> List(string message)
        {
            if (menu == null) { return RedirectToAction("LogOut", "Login"); }
            StudentViewModel model = new StudentViewModel();
            var Campuslist = await _Db.StudentInfos.ToListAsync();
            model.list = Campuslist;
            model.message = message;
            return View(model);
        }


        // GET: StudentController/Create
        public ActionResult Create()
        {
            StudentInfo model = new StudentInfo();
            if (menu.OPAdd == false) { return RedirectToAction(nameof(List)); }
            return View(model);
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StudentInfo Model)
        {
            if (menu.OPAdd == false) { return RedirectToAction(nameof(List)); }
            var exists = await _Db.StudentInfos.AsNoTracking().Where(e => e.StudentId == Model.StudentId.Trim()).ToListAsync();
            if (exists.Count > 0)
            {
                return RedirectToAction(nameof(List),new { message=$"Failed!!! Sorry Student ID {Model.StudentId} Already Exists"});
            }
            try
            {
               await _Db.AddAsync(Model);
                await _Db.SaveChangesAsync();
                return RedirectToAction(nameof(List),new { message=$"Successfully!!! Added Student with ID:{Model.StudentId}"});
            }
            catch
            {
                return RedirectToAction(nameof(List), new { message = $"Failed!!! Couldn't Add Student ID {Model.StudentId}" });
            }
        }

        // GET: StudentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (menu.OPEdit == false) { return RedirectToAction(nameof(List)); }
            StudentInfo model = new StudentInfo();
            model = await _Db.StudentInfos.SingleOrDefaultAsync(e => e.Id == id);
            if (model.Id == 0) { return RedirectToAction(nameof(List)); }
            return View(model);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StudentInfo Model)
        {
            if (menu.OPEdit == false) { return RedirectToAction(nameof(List)); }
            try
            {
                _Db.Update(Model);
                await _Db.SaveChangesAsync();
                return RedirectToAction(nameof(List), new { message = $"Successfully!!! Updated Student with ID:{Model.StudentId}" });
            }
            catch
            {
                return RedirectToAction(nameof(List), new { message = $"Failed!!! Couldn't Update Student ID {Model.StudentId}" });
            }
        }

        // GET: StudentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (menu.OPDelete == false) { return RedirectToAction(nameof(List)); }
            var model = await _Db.StudentInfos.FindAsync(id);

            try
            {
                _Db.StudentInfos.Remove(model);
               await  _Db.SaveChangesAsync();
              return RedirectToAction(nameof(List), new { message = $"Successfully!!! Deleted Student with ID:{model.StudentId}" });

            }
            catch
            {
                return RedirectToAction(nameof(List), new { message = $"Failed!!! Couldn't Delete Student ID {model.StudentId}" });

            }

        }

       
    }
}
