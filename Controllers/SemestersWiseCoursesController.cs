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
        [HttpGet]
        public async Task<JsonResult> GetSemesterWisedataBySemesterId(int semesterId)
        {
            var data = await _Db.SemesterWiseCourses.Include(e => e.Teacher).Include(e => e.Course).Include(e => e.Semester).Where(e => e.SemesterId == semesterId).Select(e => new SemWiseDataVM
            {
                CourseCode = e.Course.CourseCode,
                CourseId = e.CourseId,
                CourseName = e.Course.CourseName,
                SemesterId = e.SemesterId,
                SemesterName = e.Semester.SemisterName,
                TeacherCode = e.Teacher.ShortCode,
                TeacherId = e.TeacherId,
                TeacherName = e.Teacher.Name,
                Capacity = e.Capacity,
                Id = e.Id
            }).ToListAsync();
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> Index(MappVmPost vM)
        {
            //if (menu.OPAdd == false) { return RedirectToAction("Logout", "Login"); }
            try
            {
                var count = vM.Id.Count();
                if (count > 0)
                {
                    List<SemesterWiseCourse> models = new List<SemesterWiseCourse>();
                    var courses = await _Db.SemesterWiseCourses.Where(e => e.SemesterId == vM.SemesterId).ToListAsync();

                    var removedList = courses.Where(e => vM.Id.Contains(e.Id) == false).ToList();
                    courses.RemoveAll(e => vM.Id.Contains(e.Id) == false);
                    _Db.SemesterWiseCourses.RemoveRange(removedList);
                    await _Db.SaveChangesAsync();

                    for (int i = 0; i < count; i++)
                    {
                        if (vM.Id[i] == 0)
                        {
                            SemesterWiseCourse model = new SemesterWiseCourse();
                            model.SemesterId = vM.SemesterId;
                            model.CourseId = vM.CourseId[i];
                            model.TeacherId = vM.TeacherId[i];
                            model.Capacity = vM.Capacity[i];
                            model.Id = 0;
                            models.Add(model);
                        }
                        else
                        {
                            var index = courses.FindIndex(e => e.Id == vM.Id[i]);
                            courses[index].SemesterId = vM.SemesterId;
                            courses[index].CourseId = vM.CourseId[i];
                            courses[index].TeacherId = vM.TeacherId[i];
                            courses[index].Capacity = vM.Capacity[i];
                        }
                    }

                    _Db.SemesterWiseCourses.AddRange(models);
                    await _Db.SaveChangesAsync();
                }
                return RedirectToAction("List", new { message = "Successfully Added Semesters Wise Courses" });
            }
            catch (Exception ex)
            {
                return RedirectToAction("List", new
                {
                    message = "Failed to add " + "Semesters Wise Courses"

                });
            }
        }


        public async Task<ActionResult> SaveSemesterCourse(List<MappVm> model)
        {
            var list = model.Select(e => new SemesterWiseCourse
            {
                Id = 0,
                CourseId = e.CourseId,
                SemesterId = e.SemesterId,
                Status = true,
                TeacherId = e.TeacherId
            }).ToList();
            await _Db.SemesterWiseCourses.AddRangeAsync(list);
            await _Db.SaveChangesAsync();
            return RedirectToAction(nameof(List));
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
                Id = e.Id,
                CourseId = e.CourseId,
                TeacherId = e.TeacherId,
                SemesterId = e.SemesterId,
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
        public async Task<IActionResult> GetAdmit()
        {

            SemestersWiseCoursesViewModel model = new SemestersWiseCoursesViewModel();
            model.semesterVM = _Db.TblSemisters.ToList();
            model.StudentInfoVm = _Db.StudentInfos.ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> admit_print(SemestersWiseCoursesViewModel model)
        {
            var list2 = await _Db.StudentCourseMappings
                .Join(_Db.SemesterWiseCourses, scm => scm.SemesterWiseCourseId, sc => sc.Id, (scm, sc) => new { scm, sc })
                .Join(_Db.TblTeachers, sct => sct.sc.TeacherId, t => t.TeacherId, (sct, t) => new { sct.scm, sct.sc, t })
                .Join(_Db.TblCourses, sctt => sctt.sc.CourseId, c => c.CourseId, (sctt, c) => new { sctt.scm, sctt.sc, sctt.t, c })
                .Join(_Db.ExamDetails, scttc => scttc.sc.Id, ed => ed.SemesterWiseCourseId, (scttc, ed) => new { scttc.scm, scttc.sc, scttc.t, scttc.c, ed })
                .Where(e=>e.scm.StudentId==model.StudentId)
                .Select(x => new SemesterWiseCourseView
                {
                     Course=x.c,
                     Teacher=x.t,
                     Exam=x.ed,
                     ExamDate=x.ed.ExamDate
                }).ToListAsync();

            //var list = _Db.StudentCourseMappings.Include(e => e.SemesterWiseCourse).ThenInclude(e => e.Course).Include(e => e.SemesterWiseCourse.Teacher).Include(e => e.SemesterWiseCourse.ExamDetails).Where(s => s.StudentId == model.StudentId && s.SemesterWiseCourse.ExamDetails != null).ToList();
            SemesterWiseCourseView datamodel = new SemesterWiseCourseView();
            datamodel.SemesterName = _Db.TblSemisters.FirstOrDefault(d => d.SemisterId == model.SemesterId).SemisterName;
            datamodel.Student = _Db.StudentInfos.Include(e => e.Batch).FirstOrDefault(d => d.Id == model.StudentId);
            List<SemesterWiseCourseView> model2 = new List<SemesterWiseCourseView>();
            //foreach (var semester in list)
            //{
            //    SemesterWiseCourseView data = new SemesterWiseCourseView();
            //    //var result = _Db.SemesterWiseCourses.FirstOrDefault(s => s.Id == semester.SemesterWiseCourseId);
            //    data.Teacher = semester.SemesterWiseCourse.Teacher;
            //    data.Course = semester.SemesterWiseCourse.Course;
            //    data.Exam = _Db.ExamDetails.FirstOrDefault(f => f.SemesterWiseCourseId == semester.SemesterWiseCourseId);
            //    data.ExamDate = data.Exam.ExamDate;
            //    model2.Add(data);
            //}
            datamodel.datalist = list2;
            return View(datamodel);
        }
    }
}
