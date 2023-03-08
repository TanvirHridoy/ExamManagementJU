using CertificationMS.ContextModels;
using CertificationMS.Models;
using CertificationMS.Models.VM;
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
    public class SemestersWiseCoursesController : Controller
    {
        private readonly CertificateMSV2Context _Db;
        private string? Session;
        private EmpMenus menu = new EmpMenus();

        public SemestersWiseCoursesController(CertificateMSV2Context Db, IHttpContextAccessor HttpContext)
        {
            menu = HttpContext.HttpContext.Session.GetMenu("User", "Student");

            _Db = Db;
        }
        public IActionResult Index()
        {

            SemestersWiseCoursesVM vM = new SemestersWiseCoursesVM();  
            vM.coursesVMs = _Db.TblCourses.ToList();
            vM.teacherVM = _Db.TblTeachers.ToList();
            vM.semesterVM = _Db.TblSemisters.ToList();
            return View(vM);

        }
        [HttpPost]
        public async Task<IActionResult> Index(SemestersWiseCoursesVM vM)
        {
            if (menu.OPAdd == false) { return RedirectToAction("Logout", "Login"); }
            try
            {
                if (vM.MappVm.Count() > 0)
                {
                    List<SemesterWiseCourse> models = new List<SemesterWiseCourse>();
                    foreach (var m in vM.MappVm)
                    {
                        var res = _Db.SemesterWiseCourses.Where(f => f.SemesterId == m.SemesterId && f.TeacherId == m.TeacherId && f.CourseId == m.CourseId).Count();
                        if (res == 0)
                        {
                            SemesterWiseCourse model = new SemesterWiseCourse();
                            model.SemesterId = m.SemesterId;
                            model.CourseId = m.CourseId;
                            model.TeacherId = m.TeacherId;
                            models.Add(model);
                        }
                    }
                    _Db.SemesterWiseCourses.AddRange(models);
                    await _Db.SaveChangesAsync();
                }
                return RedirectToAction("List", new { message = "Successfully Added Semesters Wise Courses" });
            }
            catch (Exception)
            {
                return RedirectToAction("List", new
                {
                    message = "Failed to add " + "Semesters Wise Courses"

                });
            }
        }

        public async Task<IActionResult> List(Status message = null)
        {
            SemesterTeacherCoursesListVM vM = new SemesterTeacherCoursesListVM();
            vM.coursesVMs = await _Db.TblCourses.ToListAsync();
            vM.teacherVM = await _Db.TblTeachers.ToListAsync();
            vM.semesterVM = await _Db.TblSemisters.ToListAsync();
            vM.message = message != null ? message.MessageText : "";
            vM.cc = await _Db.SemesterWiseCourses.Select(e => new SemesterWiseCourse
            {
                Id = e.Id ,
                CourseId = e.CourseId , 
                TeacherId = e.TeacherId ,
                SemesterId = e.SemesterId ,
            }).ToListAsync();
            return View(vM);    
        }
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _Db.SemesterWiseCourses.FindAsync(id);
            _Db.Remove(res);
            await _Db.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> admit_print(long id)
        {
           
            return View();
        }
    }
}
