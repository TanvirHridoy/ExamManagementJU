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
    public class ExamMasterController : Controller
    {

        private readonly CertificateMSV2Context _Db;
        private string? Session;
        private EmpMenus menu = new EmpMenus();
        public ExamMasterController(CertificateMSV2Context Db, IHttpContextAccessor HttpContext)
        {
            menu = HttpContext.HttpContext.Session.GetMenu("User", "Student");

            _Db = Db;
        }
        // GET: ExamMasterController
        public async Task<ActionResult> List(string message)
        {
            ExamMasterVM model = new ExamMasterVM()
            {
                list = await _Db.ExamMasters.Include(e => e.Semester).ToListAsync(),
                message = message

            };
            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            ExamMasterCreateVM model = new ExamMasterCreateVM()
            {
                Exam = new ExamMaster(),
                SemesterList = await _Db.TblSemisters.ToListAsync()
            };
            if (menu.OPAdd == false) { return RedirectToAction(nameof(List)); }
            return View(model);
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ExamMasterCreateVM Model)
        {
            if (menu.OPAdd == false) { return RedirectToAction(nameof(List)); }

            try
            {
                await _Db.ExamMasters.AddAsync(Model.Exam);
                await _Db.SaveChangesAsync();
                return RedirectToAction(nameof(List), new { message = $"Successfully!!! Added Exam Name:{Model.Exam.ExamName}" });
            }
            catch
            {
                return RedirectToAction(nameof(List), new { message = $"Failed!!! Couldn't Add Exam Name {Model.Exam.ExamName}" });
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (menu.OPEdit == false) { return RedirectToAction(nameof(List)); }
            ExamMasterCreateVM model = new ExamMasterCreateVM()
            {
                Exam = await _Db.ExamMasters.SingleOrDefaultAsync(e => e.Id == id),
                SemesterList = await _Db.TblSemisters.ToListAsync()
            };
            if (model.Exam == null) { return RedirectToAction(nameof(List)); }
            return View(model);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ExamMasterCreateVM Model)
        {
            if (menu.OPEdit == false) { return RedirectToAction(nameof(List)); }
            try
            {
                _Db.ExamMasters.Update(Model.Exam);
                await _Db.SaveChangesAsync();
                return RedirectToAction(nameof(List), new { message = $"Successfully!!! Updated Exam Name:{Model.Exam.ExamName}" });
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(List), new { message = $"Failed!!! Couldn't Update Exam Name  {Model.Exam.ExamName}" });
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
            if (menu.OPDelete == false) { return RedirectToAction(nameof(List)); }
            var model = await _Db.ExamMasters.FindAsync(id);

            try
            {
                _Db.ExamMasters.Remove(model);
                await _Db.SaveChangesAsync();
                return RedirectToAction(nameof(List), new { message = $"Successfully!!! Deleted  Exam Name :{model.ExamName}" });

            }
            catch
            {
                return RedirectToAction(nameof(List), new { message = $"Failed!!! Couldn't Delete Course Name {model.ExamName}" });

            }

        }

    }
}
