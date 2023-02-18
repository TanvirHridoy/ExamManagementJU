using CertificationMS.ContextModels;
using CertificationMS.Models;
using CertificationMS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CertificationMS.Controllers
{
    public class StudentController : Controller
    {
        private readonly CertificateMSV2Context _Db;
        private string? Session;
        private EmpMenus menu = new EmpMenus();

        public StudentController(CertificateMSV2Context Db, IHttpContextAccessor HttpContext)
        {
            menu = HttpContext.HttpContext.Session.GetMenu("User", "Student");

            _Db = Db;
        }
        // GET: StudentController
        public async Task<ActionResult> List(string message)
        {
            if (menu == null) { return RedirectToAction("LogOut", "Login"); }
            StudentViewModel model = new StudentViewModel();
            var Campuslist = await _Db.StudentInfos.ToListAsync();
            model.list = Campuslist;
            model.message = message;
            return View(model);
        }


        // GET: StudentController/Create
        public ActionResult Create()
        {
            StudentInfo model = new StudentInfo();
            if (menu.OPAdd == false) { return RedirectToAction(nameof(List)); }
            return View(model);
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StudentInfo Model)
        {
            if (menu.OPAdd == false) { return RedirectToAction(nameof(List)); }
            var exists = await _Db.StudentInfos.AsNoTracking().Where(e => e.StudentId == Model.StudentId.Trim()).ToListAsync();
            if (exists.Count > 0)
            {
                return RedirectToAction(nameof(List), new { message = $"Failed!!! Sorry Student ID {Model.StudentId} Already Exists" });
            }
            try
            {
                await _Db.AddAsync(Model);
                await _Db.SaveChangesAsync();
                return RedirectToAction(nameof(List), new { message = $"Successfully!!! Added Student with ID:{Model.StudentId}" });
            }
            catch
            {
                return RedirectToAction(nameof(List), new { message = $"Failed!!! Couldn't Add Student ID {Model.StudentId}" });
            }
        }


        public async Task<ActionResult> CreateStudent()
        {
            StudentCreateVm data = new StudentCreateVm()
            {
                BatchInfos = await _Db.BatchInfos.ToListAsync(),
                Programs = await _Db.Programs.ToListAsync()
            };
            return View(data);
        }

        [HttpPost]
        public async Task<ActionResult> CreateStudent([FromForm] StudentCreateVm model)
        {
            var file = Request.Form.Files.Count > 0 ? Request.Form.Files[0] : null;
            StudentInfo student = new StudentInfo()
            {
                BatchId = model.Student.BatchId,
                FileName = file.FileName,
                Photo = ConvertToBytes(file),
                Id = 0,
                Name = model.Student.Name,
                ProgramId = model.Student.ProgramId,
                StudentId = model.Student.StudentId
            };
            await _Db.StudentInfos.AddAsync(student);
            await _Db.SaveChangesAsync();
            return RedirectToAction(nameof(CreateStudent));
        }

        [HttpGet]
        public async Task<ActionResult> GetStudentInfoByStudentId(string studentId)
        {
            var student = await _Db.StudentInfos.Include(e => e.Program).Include(e => e.Batch)
                .Select(e => new StudentInfoVm()
                {
                    BatchName = e.Batch.BatchNo,
                    ProgramName = e.Program.ProgramName,
                    FileName = e.FileName,
                    Id = e.Id,
                    Name = e.Name,
                    Photo = e.Photo,
                    StudentId = e.StudentId
                })
                .SingleOrDefaultAsync(e => e.StudentId == studentId);
            return Ok(student);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllStudent()
        {
            var studentLst = await _Db.StudentInfos.Include(e => e.Batch).Include(e => e.Program)
           .Select(e => new StudentInfoVm()
           {
               BatchName = e.Batch.BatchNo,
               ProgramName = e.Program.ProgramName,
               FileName = e.FileName,
               Id = e.Id,
               Name = e.Name,
               Photo = e.Photo,
               StudentId = e.StudentId
           }).ToListAsync();
            return Ok(studentLst);
        }
        public byte[] ConvertToBytes(IFormFile image)
        {
            byte[] s = null;
            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                s = ms.ToArray();
            }
            return s;
        }

        // GET: StudentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (menu.OPEdit == false) { return RedirectToAction(nameof(List)); }
            StudentInfo model = new StudentInfo();
            model = await _Db.StudentInfos.SingleOrDefaultAsync(e => e.Id == id);
            if (model.Id == 0) { return RedirectToAction(nameof(List)); }
            return View(model);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StudentInfo Model)
        {
            if (menu.OPEdit == false) { return RedirectToAction(nameof(List)); }
            try
            {
                _Db.Update(Model);
                await _Db.SaveChangesAsync();
                return RedirectToAction(nameof(List), new { message = $"Successfully!!! Updated Student with ID:{Model.StudentId}" });
            }
            catch
            {
                return RedirectToAction(nameof(List), new { message = $"Failed!!! Couldn't Update Student ID {Model.StudentId}" });
            }
        }

        // GET: StudentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (menu.OPDelete == false) { return RedirectToAction(nameof(List)); }
            var model = await _Db.StudentInfos.FindAsync(id);

            try
            {
                _Db.StudentInfos.Remove(model);
                await _Db.SaveChangesAsync();
                return RedirectToAction(nameof(List), new { message = $"Successfully!!! Deleted Student with ID:{model.StudentId}" });

            }
            catch
            {
                return RedirectToAction(nameof(List), new { message = $"Failed!!! Couldn't Delete Student ID {model.StudentId}" });

            }

        }


    }
}
