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
    public class CourseController : Controller
    {
        private readonly CertificateMSV2Context _Db;
        private string? Session;
        private EmpMenus menu = new EmpMenus();
        public CourseController(CertificateMSV2Context Db, IHttpContextAccessor HttpContext)
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
            CourseViewModel model = new CourseViewModel();
            var courseList = await _Db.TblCourses.ToListAsync();
            model.list = courseList;
            model.message = message;
            return View(model);
        }

        public ActionResult Create()
        {
            TblCourse model = new TblCourse();
            if (menu.OPAdd == false) { return RedirectToAction(nameof(List)); }
            return View(model);
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TblCourse Model)
        {
            if (menu.OPAdd == false) { return RedirectToAction(nameof(List)); }

            try
            {
                await _Db.TblCourses.AddAsync(Model);
                await _Db.SaveChangesAsync();
                return RedirectToAction(nameof(List), new { message = $"Successfully!!! Added Course Name:{Model.CourseName}" });
            }
            catch
            {
                return RedirectToAction(nameof(List), new { message = $"Failed!!! Couldn't Add Course Name {Model.CourseName}" });
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (menu.OPEdit == false) { return RedirectToAction(nameof(List)); }
            TblCourse model = new TblCourse();
            model = await _Db.TblCourses.SingleOrDefaultAsync(e => e.CourseId == id);
            if (model.CourseId == 0) { return RedirectToAction(nameof(List)); }
            return View(model);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TblCourse Model)
        {
            if (menu.OPEdit == false) { return RedirectToAction(nameof(List)); }
            try
            {
                _Db.TblCourses.Update(Model);
                await _Db.SaveChangesAsync();
                return RedirectToAction(nameof(List), new { message = $"Successfully!!! Updated Course Name:{Model.CourseName}" });
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(List), new { message = $"Failed!!! Couldn't Update Course Name  {Model.CourseName}" });
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
            if (menu.OPDelete == false) { return RedirectToAction(nameof(List)); }
            var model = await _Db.TblCourses.FindAsync(id);

            try
            {
                _Db.TblCourses.Remove(model);
                await _Db.SaveChangesAsync();
                return RedirectToAction(nameof(List), new { message = $"Successfully!!! Deleted  Course Name :{model.CourseName}" });

            }
            catch
            {
                return RedirectToAction(nameof(List), new { message = $"Failed!!! Couldn't Delete Course Name {model.CourseName}" });

            }

        }
    }
}
