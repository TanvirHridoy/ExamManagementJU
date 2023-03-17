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
    public class ExamDetailsController : Controller
    {

        private readonly CertificateMSV2Context _Db;
        private string? Session;
        private EmpMenus menu = new EmpMenus();
        public ExamDetailsController(CertificateMSV2Context Db, IHttpContextAccessor HttpContext)
        {
            menu = HttpContext.HttpContext.Session.GetMenu("User", "Student");

            _Db = Db;
        }
        public async Task<ActionResult> List(string message="")
        {
            ExamDetailsVM model = new ExamDetailsVM()
            {
                message = message
            };
            model.ExamMasterList = await _Db.ExamMasters.Include(e => e.Semester).ToListAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> GetExamWisedataByExamId(int ExamId)
        {
            var data = await _Db.VwExamDetails.Where(e=>e.ExamMasterId==ExamId).ToListAsync();
            return Json(data);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateData(ExamDetailsPostVM model)
        {
            return RedirectToAction("List", new { message = "Updated SuccessFully" });
        }
    }
}
