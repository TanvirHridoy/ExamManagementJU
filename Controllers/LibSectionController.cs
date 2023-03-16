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
    public class LibSectionController : Controller
    {
        public readonly CertificateMSV2Context _Db;
        public IConfiguration _config;
        private EmpMenus menu = new EmpMenus();
        public LibSectionController(CertificateMSV2Context Db, IConfiguration configuration, IHttpContextAccessor HttpContext)
        {
            _Db = Db;
            menu = HttpContext.HttpContext.Session.GetMenu("User", "LibSection");
            _config = configuration;
        }
        // GET: AccountsSectionController
        public async Task<ActionResult> Index(Status message = null)
        {
            if (menu==null) { return RedirectToAction("LogOut", "Login"); }
            AccSectionViewModel viewModel = new AccSectionViewModel();
            viewModel.departments = await _Db.Departments.ToListAsync();
            viewModel.studentTypes = await _Db.StudentTypes.ToListAsync();
            viewModel.programs = await _Db.Programs.ToListAsync();
            viewModel.message = message != null ? message.MessageText : "";

            viewModel.Applications = await _Db.CertApplications
                .Where(f => f.ApvStatusAcad == 1 && f.ApvStatusExam == 1 && f.ApvStatusAcc == 2 && f.ApvStatusDept == 2)
                .Select(e => new DeptSectionListModels
                {
                    Id = e.Id,
                    ApplyDate = e.ApplyDate,
                    MajorSubjectID = e.MajorSubject,
                    ProgramId = e.ProgramId,
                    StudentId = e.StudentId,
                    StudentName = e.StudentName,
                    StudentTypeID = e.StudentType,
                    AppStatus = e.ApvStatusLib == 1 ? "Pending" : e.ApvStatusLib == 2 ? "Approved" : e.ApvStatusLib == 3 ? "Rejected" : "Unknown"
                }
            ).ToListAsync();
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
                application.ApprovedByLib = HttpContext.Session.GetUserId("User");
                application.ApvStatusLib = 2;
                application.ApvLibDate = DateTime.Now;
                await _Db.SaveChangesAsync();
                mail.SendEmailToDept("ACAD", application.StudentId.ToString());
                return RedirectToAction("Index", "LibSection", new Status { MessageText = "Successfully Approved " + application.StudentName + "'s application" });
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return RedirectToAction("Index", "LibSection", new Status { MessageText = "Failed To Approved " + application.StudentName + "'s application" });
            }
        }
        [HttpPost]
        public async Task<ActionResult> Reject(int id)
        {
    
            if (menu.OPEdit==false) { return RedirectToAction("LogOut", "Login"); }
            var application = await _Db.CertApplications.FindAsync(id);
            try
            {
                application.ApprovedByLib = HttpContext.Session.GetUserId("User");
                application.ApvStatusLib = 3;
                application.ApvLibDate = DateTime.Now;
                await _Db.SaveChangesAsync();
                MailHelper mail = new MailHelper(_config);
                mail.SendEmailRejection(application.ExtraThree, "Library", application.Id.ToString());
                return RedirectToAction("Index", "LibSection", new Status { MessageText = "Successfully Rejected " + application.StudentName + "'s application" });
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return RedirectToAction("Index", "LibSection", new Status { MessageText = "Failed To Rejected " + application.StudentName + "'s application" });
            }
        }
    }
}
