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
        public async Task<ActionResult> List(string message = "")
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
            var data = await _Db.VwExamDetails.Where(e => e.ExamMasterId == ExamId).ToListAsync();
            return Json(data);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateData(ExamDetailsPostVM model)
        {
            try
            {
                var data = await _Db.ExamDetails.Where(e => e.ExamMasterId == model.ExamId).ToListAsync();

                var removedata = data.Where(e => model.Id.Contains(e.Id) == false).ToList();

                var dutydata = await _Db.ExamDuties.Where(e => removedata.Select(x => x.Id).Contains(e.ExamDetailsId)).ToListAsync();
                _Db.ExamDuties.RemoveRange(dutydata);
                await _Db.SaveChangesAsync();
                _Db.ExamDetails.RemoveRange(removedata);
                await _Db.SaveChangesAsync();
                data.RemoveAll(e => removedata.Select(e => e.Id).Contains(e.Id));
                for (int i = 0; i < model.Id.Length; i++)
                {
                    var index = data.FindIndex(e => e.Id == model.Id[i]);
                    if (index <0)
                    {
                        ExamDetail detail = new ExamDetail()
                        {
                            Duration = model.Duration[i],
                            ExamDate = model.ExamDate[i],
                            ExamMasterId = model.ExamId,
                            Id = 0,
                            SemesterWiseCourseId = model.SCMId[i],
                            Status = false
                        };
                       await _Db.ExamDetails.AddAsync(detail);
                    }
                    else
                    {
                        data[index].Duration = model.Duration[i];
                        data[index].ExamDate = model.ExamDate[i];
                    }

                }
                await _Db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return RedirectToAction("List", new { message = "Failed To Update" });
            }

            return RedirectToAction("List", new { message = "Updated SuccessFully" });
        }
    }
}
