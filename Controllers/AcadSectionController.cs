using CertificationMS.ContextModels;
using CertificationMS.Models;
using CertificationMS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Controllers
{
    [SessionTimeout]
    public class AcadSectionController : Controller
    {
        public readonly CertificateMSV2Context _Db;
        public IConfiguration _config;
        private EmpMenus menu = new EmpMenus();
        public AcadSectionController(CertificateMSV2Context Db, IConfiguration configuration, IHttpContextAccessor HttpContext)
        {
            menu = HttpContext.HttpContext.Session.GetMenu("User", "AcadSection");
            _Db = Db;
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
                .Where(f => f.ApvStatusAcc == 2 && f.ApvStatusExam == 1 && f.ApvStatusLib == 2 && f.ApvStatusDept == 2)
                .Select(e => new DeptSectionListModels
                {
                    Id = e.Id,
                    ApplyDate = e.ApplyDate,
                    MajorSubjectID = e.MajorSubject,
                    ProgramId = e.ProgramId,
                    StudentId = e.StudentId,
                    StudentName = e.StudentName,
                    StudentTypeID = e.StudentType,
                    AppStatus = e.ApvStatusAcad == 1 ? "Pending" : e.ApvStatusAcad == 2 ? "Approved" : e.ApvStatusAcad == 3 ? "Rejected" : "Unknown"
                }
            ).ToListAsync();
            return View(viewModel);
        }

        public async Task<ActionResult> Details(int id)
        {
            if (menu.OPEdit)
            {
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
            else { return RedirectToAction("LogOut", "Login"); }
            
        }

        // GET: DeptSectionController/Create
        [HttpPost]
        public async Task<ActionResult> Approve(int id)
        {

            if (menu.OPEdit)
            {
                MailHelper mail = new MailHelper(_config);
                var application = await _Db.CertApplications.FindAsync(id);
                try
                {
                    application.ApprovedByAcad = HttpContext.Session.GetUserId("User");
                    application.ApvStatusAcad = 2;
                    application.ApvAcaddate = DateTime.Now;
                    await _Db.SaveChangesAsync();
                    mail.SendEmail("kmhridoynub@gmail.com", "Exam Section", "Certificate Application Came", application.StudentId);
                    return RedirectToAction("Index", "AcadSection", new Status { MessageText = "Successfully Approved " + application.StudentName + "'s application" });
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    return RedirectToAction("Index", "AcadSection", new Status { MessageText = "Failed To Approved " + application.StudentName + "'s application" });
                }
            
            }
            else { return RedirectToAction("LogOut", "Login"); }
            
        }
        [HttpPost]
        public async Task<ActionResult> Reject(int id)
        {

            if (menu.OPEdit)
            {
                var application = await _Db.CertApplications.FindAsync(id);
                try
                {
                    application.ApprovedByAcad = HttpContext.Session.GetUserId("User");
                    application.ApvStatusAcad = 3;
                    application.ApvAcaddate = DateTime.Now;
                    await _Db.SaveChangesAsync();

                    return RedirectToAction("Index", "AcadSection", new Status { MessageText = "Successfully Rejected " + application.StudentName + "'s application" });
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    return RedirectToAction("Index", "AcadSection", new Status { MessageText = "Failed To Rejected " + application.StudentName + "'s application" });
                }
            }
            else { return RedirectToAction("LogOut", "Login"); }
           
        }
    }
}
