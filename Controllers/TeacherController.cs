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
    public class TeacherController : Controller
    {
        private readonly CertificateMSV2Context _Db;
        private string? Session;
        private EmpMenus menu = new EmpMenus();
        public TeacherController(CertificateMSV2Context Db, IHttpContextAccessor HttpContext)
        {
            menu = HttpContext.HttpContext.Session.GetMenu("User", "Student");

            _Db = Db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> List(string message)
        {
            if (menu == null) { return RedirectToAction("LogOut", "Login"); }
            TeacherViewModel model = new TeacherViewModel();
            var TecaherList = await _Db.TblTeachers.ToListAsync();
            model.list = TecaherList;
            model.message = message;

            int x = (int)Gender.Male;
            return View(model);
        }

        public ActionResult Create()
        {
            TblTeacher model = new TblTeacher();
            if (menu.OPAdd == false) { return RedirectToAction(nameof(List)); }
            return View(model);
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TblTeacher Model)
        {
            if (menu.OPAdd == false) { return RedirectToAction(nameof(List)); }
           
            try
            {
                await _Db.TblTeachers.AddAsync(Model);
                await _Db.SaveChangesAsync();
                return RedirectToAction(nameof(List), new { message = $"Successfully!!! Added Teacher:{Model.Name}" });
            }
            catch
            {
                return RedirectToAction(nameof(List), new { message = $"Failed!!! Couldn't Add Teacher {Model.Name}" });
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (menu.OPEdit == false) { return RedirectToAction(nameof(List)); }
            TblTeacher model = new TblTeacher();
            model = await _Db.TblTeachers.SingleOrDefaultAsync(e => e.TeacherId == id);
            if (model.TeacherId == 0) { return RedirectToAction(nameof(List)); }
            return View(model);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TblTeacher Model)
        {
            if (menu.OPEdit == false) { return RedirectToAction(nameof(List)); }
            try
            {
                _Db.TblTeachers.Update(Model);
                await _Db.SaveChangesAsync();
                return RedirectToAction(nameof(List), new { message = $"Successfully!!! Updated Teacher:{Model.Name}" });
            }
            catch(Exception ex)
            {
                return RedirectToAction(nameof(List), new { message = $"Failed!!! Couldn't Update Teacher  {Model.Name}" });
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
            if (menu.OPDelete == false) { return RedirectToAction(nameof(List)); }
            var model = await _Db.TblTeachers.FindAsync(id);

            try
            {
                _Db.TblTeachers.Remove(model);
                await _Db.SaveChangesAsync();
                return RedirectToAction(nameof(List), new { message = $"Successfully!!! Deleted Teacher :{model.Name}" });

            }
            catch
            {
                return RedirectToAction(nameof(List), new { message = $"Failed!!! Couldn't Delete Teacher {model.Name}" });

            }

        }
    }
}
