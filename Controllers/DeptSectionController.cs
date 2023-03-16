using EMSJu.ContextModels;
using EMSJu.Models;
using EMSJu.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EMSJu.Controllers
{
    [SessionTimeout]
    public class DeptSectionController : Controller
    {
        public readonly CertificateMSV2Context _Db;

        public IConfiguration _config;
        private string? Session;
        private EmpMenus? menu = new EmpMenus();
        LoggedInModel LoginResp = new LoggedInModel();
        public DeptSectionController(CertificateMSV2Context Db, IConfiguration configuration, IHttpContextAccessor HttpContext)
        {

            LoginResp=HttpContext.HttpContext.Session.GetObject<LoggedInModel>("User");
            menu = LoginResp.EmpMenuList.FindLast(e => e.MenuName.EndsWith("Dept"));
            _Db = Db;
            _config = configuration;
        }
        // GET: DeptSectionController
        public async Task<ActionResult> Index(int id=0, Status message=null)
        {

            if (menu==null) { return RedirectToAction("LogOut", "Login"); }
            DeptSectionViewModel viewModel = new DeptSectionViewModel();
            viewModel.departments = await _Db.Departments.ToListAsync();
            viewModel.studentTypes = await _Db.StudentTypes.ToListAsync();
            viewModel.programs = await _Db.Programs.ToListAsync();
            viewModel.message = message != null ? message.MessageText : "";
            if (id == 0)
            {
                viewModel.Applications = await _Db.CertApplications
                .Select(e => new DeptSectionListModels
                {
                    Id = e.Id,
                    ApplyDate = e.ApplyDate,
                    MajorSubjectID = e.MajorSubject,
                    ProgramId = e.ProgramId,
                    StudentId = e.StudentId,
                    StudentName = e.StudentName,
                    StudentTypeID = e.StudentType,
                    AppStatus = e.ApvStatusDept == 1 ? "Pending" : e.ApvStatusDept == 2 ? "Approved" : e.ApvStatusDept == 3 ? "Rejected" : "Unknown"
                }
            ).ToListAsync();
            }
            else
            {
                viewModel.Department = viewModel.departments.SingleOrDefault(e => e.Id == id).DeptSname;
                viewModel.Applications = await _Db.CertApplications
                .Where(f =>  f.MajorSubject==id)
                .Select(e => new DeptSectionListModels
                {
                    Id = e.Id,
                    ApplyDate = e.ApplyDate,
                    MajorSubjectID = e.MajorSubject,
                    ProgramId = e.ProgramId,
                    StudentId = e.StudentId,
                    StudentName = e.StudentName,
                    StudentTypeID = e.StudentType,
                    AppStatus = e.ApvStatusDept == 1 ? "Pending" : e.ApvStatusDept == 2 ? "Approved" : e.ApvStatusDept == 3 ? "Rejected" : "Unknown"
                }
            ).ToListAsync();
            }
            
            return View(viewModel);
        }

        public async Task<ActionResult> Details(int id)
        {

            if (menu.OPEdit==false) { return RedirectToAction("LogOut", "Login"); }
            ApplicationDetailsViewModel viewModel = new ApplicationDetailsViewModel();
            viewModel.Application = await _Db.CertApplications.SingleAsync(e => e.Id == id);
            viewModel.ApvStatusLst = await _Db.ApvStatuses.ToListAsync();
            viewModel.departments = await _Db.Departments.ToListAsync();
            viewModel.studentTypes = await _Db.StudentTypes.ToListAsync();
            viewModel.programs = await _Db.Programs.ToListAsync();
            viewModel.Campuses = await _Db.Campuses.ToListAsync();
            viewModel.Convocations = await _Db.Convocations.ToListAsync();
            return View(viewModel);
        }

        // GET: DeptSectionController/Create
        [HttpPost]
        public async Task<ActionResult> Approve(int id)
        {

            if (menu.OPEdit==false) { return RedirectToAction("LogOut", "Login"); }
            MailHelper mail = new MailHelper(_config);
            var application = await _Db.CertApplications.FindAsync(id);
                try
                {
                    application.ApprovedByDept = HttpContext.Session.GetUserId("User");
                    application.ApvStatusDept = 2;
                    application.ApvDeptDate = DateTime.Now;
                    await _Db.SaveChangesAsync();
                    mail.SendEmailToDept("ACC", application.StudentId.ToString());
                    return RedirectToAction("Index", "DeptSection", new {id=application.MajorSubject, message= new Status { MessageText = "Successfully Approved " + application.StudentName + "'s application" } });
                }
                catch(Exception ex)
                {
                    var msg = ex.Message;
                    return RedirectToAction("Index", "DeptSection",new {id=application.MajorSubject, message= new Status { MessageText = "Failed To Approved " + application.StudentName + "'s application" } } );
                }
        }
        [HttpPost]
        public async Task<ActionResult> Reject(int id)
        {
            MailHelper mail = new MailHelper(_config);
            if (menu.OPEdit==false) { return RedirectToAction("LogOut", "Login"); }
            var application = await _Db.CertApplications.FindAsync(id);
            try
            {
                application.ApprovedByDept = HttpContext.Session.GetUserId("User");
                application.ApvStatusDept = 3;
                application.ApvDeptDate = DateTime.Now;
                await _Db.SaveChangesAsync();
                mail.SendEmailRejection(application.ExtraThree, "Department", application.Id.ToString());
                return RedirectToAction("Index", "DeptSection",new {id=application.MajorSubject,messsage = new Status { MessageText = "Successfully Rejected " + application.StudentName + "'s application" } });
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return RedirectToAction("Index", "DeptSection",new {id=application.MajorSubject,message= new Status { MessageText = "Failed To Rejected " + application.StudentName + "'s application" } } );
            }
        }
        // POST: DeptSectionController/Create
        

        // GET: DeptSectionController/Edit/5
        public ActionResult Edit(int id)
        {

            if (menu.OPEdit==false) { return RedirectToAction("LogOut", "Login"); }
            return View();
        }

        // POST: DeptSectionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {

            if (menu.OPEdit==false) { return RedirectToAction("LogOut", "Login"); }
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       
    }
}
