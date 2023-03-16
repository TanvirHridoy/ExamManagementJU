using EMSJu.ContextModels;
using EMSJu.Models;
using EMSJu.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSJu.Controllers
{
    public class ReportController : Controller
    {
        private readonly CertificateMSV2Context _Db;
        private string? Session;
        private EmpMenus menu = new EmpMenus();

        public ReportController(CertificateMSV2Context Db, IHttpContextAccessor HttpContext)
        {
            menu = HttpContext.HttpContext.Session.GetMenu("User", "Report");

            _Db = Db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetExamWiseStudent()
        {
            SemisterVm Vm = new SemisterVm();
            Vm.semisters= _Db.TblSemisters.ToList();

            return View(Vm);
        }
        [HttpGet]
        public JsonResult GetSemisterWiseCourses(int Id)
        {
            var data = _Db.VwAttendances.Where(x => x.SemisterId == Id).ToList();
            return Json(data);
        }
        [HttpGet]
       public JsonResult GetStudent(int id)
        {
            var res = _Db.VwExamWiseStudents.Where(x => x.CourseId == id).ToList();
            return Json(res);
        }


    }
}
