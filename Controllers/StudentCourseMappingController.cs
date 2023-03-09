using CertificationMS.ContextModels;
using CertificationMS.Models;
using CertificationMS.Models.VM;
using CertificationMS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult SemestersWiseCourses(int id)
        {
            List<SemesterWiseCourseView> model = new List<SemesterWiseCourseView>();

            var list = _Db.SemesterWiseCourses.Where(s => s.SemesterId == id).ToList();
            foreach (var semester in list)
            {
                SemesterWiseCourseView data = new SemesterWiseCourseView();
                data.SemesterWiseCourseId = semester.SemesterId;
                data.SemesterName = _Db.TblSemisters.FirstOrDefault(d => d.SemisterId == semester.SemesterId).SemisterName;
                data.TeacherName = _Db.TblTeachers.FirstOrDefault(d => d.TeacherId == semester.TeacherId).Name;
                data.CoursesName = _Db.TblCourses.FirstOrDefault(d => d.CourseId == semester.CourseId).CourseName;
                var res = _Db.StudentCourseMappings.FirstOrDefault(g => g.SemesterWiseCourseId == semester.Id);
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
            List<StudentCourseMapping> models = new List<StudentCourseMapping>();
            if (vM.MappVm.Count() > 0)
            {
                foreach (var item in vM.MappVm)
                {
                    if (item.IsCheck==true)
                    {
                        List<StudentCourseMapping> res = _Db.StudentCourseMappings.Where(d => d.StudentId == vM.StudentId && d.SemesterWiseCourseId == item.SemesterWiseCourseId).ToList();
                        if (res.Count()>0)
                        {
                            _Db.StudentCourseMappings.RemoveRange(res);
                            _Db.SaveChanges();
                        }
                        StudentCourseMapping mapping = new StudentCourseMapping();
                        mapping.StudentId = vM.StudentId;
                        mapping.SemesterWiseCourseId = item.SemesterWiseCourseId;
                        mapping.Status = 1;
                        mapping.IsComplete = false;
                        models.Add(mapping);
                    }

                }
                _Db.StudentCourseMappings.AddRange(models);
                _Db.SaveChanges();

            }
            
            return View();
        }
    }
}
