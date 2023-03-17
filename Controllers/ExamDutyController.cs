using EMSJu.ContextModels;
using EMSJu.Models;
using EMSJu.Models.VM;
using EMSJu.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EMSJu.Controllers
{
    public class ExamDutyController : Controller
    {
        private readonly CertificateMSV2Context _Db;
        private string? Session;
        private EmpMenus menu = new EmpMenus();
        public ExamDutyController(CertificateMSV2Context Db, IHttpContextAccessor HttpContext)
        {
            menu = HttpContext.HttpContext.Session.GetMenu("User", "Student");

            _Db = Db;
        }
        public async Task<ActionResult> Index(string message = "")
        {
            ExamDetailsVM model = new ExamDetailsVM()
            {
                message = message
            };
            model.ExamMasterList = await _Db.ExamMasters.Include(e => e.Semester).ToListAsync();
            model.TeacherList = await _Db.TblTeachers.ToListAsync();
            return View(model);
        }

        public async Task<JsonResult> GetDetailsByExamId(int ExamId)
        {
            var details = await _Db.VwExamDetails.Where(e => e.ExamMasterId == ExamId).ToListAsync();
            return Json(details);
        }
        public async Task<JsonResult> GetTeacherByDetailsId(int ExamDetailsId)
        {
            var details = await _Db.VwTeacherExamDuties.Where(e => e.ExamDetailId == ExamDetailsId).ToListAsync();
            return Json(details);
        }
    }
}
