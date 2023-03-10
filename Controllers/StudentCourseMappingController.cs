using CertificationMS.ContextModels;
using CertificationMS.Models;
using CertificationMS.Models.VM;
using CertificationMS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CertificationMS.Controllers
{
    public class StudentCourseMappingController : Controller
    {
        private readonly CertificateMSV2Context _Db;
        private string? Session;
        private EmpMenus menu = new EmpMenus();
        public StudentCourseMappingController(CertificateMSV2Context Db, IHttpContextAccessor HttpContext)
        {
            menu = HttpContext.HttpContext.Session.GetMenu("User", "Student");

            _Db = Db;
        }
        public IActionResult Index()
        {
            SemestersWiseCoursesViewModel model = new SemestersWiseCoursesViewModel();
            model.semesterVM = _Db.TblSemisters.ToList();
            model.StudentInfoVm = _Db.StudentInfos.ToList();
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> SemestersWiseCourses(int id, int StudentId)
        {

            List<SemesterWiseCourseView> model = new List<SemesterWiseCourseView>();
            var sdata = await _Db.StudentCourseMappings.Include(e => e.SemesterWiseCourse).Where(e => e.StudentId == StudentId && e.SemesterWiseCourse.SemesterId == id).ToListAsync();
            var list =await _Db.SemesterWiseCourses.Where(s => s.SemesterId == id).ToListAsync();
            foreach (var semester in list)
            {
                SemesterWiseCourseView data = new SemesterWiseCourseView();
                data.SemesterWiseCourseId = semester.Id;
                data.SemesterName = _Db.TblSemisters.FirstOrDefault(d => d.SemisterId == semester.SemesterId).SemisterName;
                data.TeacherName = _Db.TblTeachers.FirstOrDefault(d => d.TeacherId == semester.TeacherId).Name;
                data.CoursesName = _Db.TblCourses.FirstOrDefault(d => d.CourseId == semester.CourseId).CourseName;
                var res = sdata.FirstOrDefault(e=>e.SemesterWiseCourseId==semester.Id);
                if (res != null)
                {
                    data.IsCheck = true;
                }

                model.Add(data);
            }
            return Json(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(SemestersWiseCoursesViewModel vM)
        {
            try
            {
                if (vM.MappVm.Count() > 0)
                {
                    foreach (var item in vM.MappVm)
                    {
                        if (item.IsCheck == true)
                        {
                            var res = await _Db.StudentCourseMappings.AsNoTracking().Where(d => d.StudentId == vM.StudentId && d.SemesterWiseCourseId == item.SemesterWiseCourseId).SingleOrDefaultAsync();
                            if (res == null)
                            {
                                StudentCourseMapping mapping = new StudentCourseMapping();
                                mapping.StudentId = vM.StudentId;
                                mapping.SemesterWiseCourseId = item.SemesterWiseCourseId;
                                mapping.Status = 1;
                                mapping.IsComplete = false;
                                mapping.Id = 0;
                                await _Db.StudentCourseMappings.AddAsync(mapping);
                                await _Db.SaveChangesAsync();

                            }
                        }
                        else
                        {
                            var res = await _Db.StudentCourseMappings.Where(d => d.StudentId == vM.StudentId && d.SemesterWiseCourseId == item.SemesterWiseCourseId).SingleOrDefaultAsync();
                            if (res != null)
                            {
                                _Db.StudentCourseMappings.Remove(res);
                                await _Db.SaveChangesAsync();
                            }
                        }

                    }


                }
            }
            catch (System.Exception ex)
            {

                var msg = ex.Message;
            }
           

            return RedirectToAction(nameof(Index));
        }
    }
}
