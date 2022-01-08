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
    public class AccSectionController : Controller
    {
       
        public readonly CertificateMSV2Context _Db;
        public IConfiguration _config;
        private EmpMenus menu = new EmpMenus();
        public AccSectionController(CertificateMSV2Context Db, IConfiguration configuration, IHttpContextAccessor HttpContext)
        {
            menu = HttpContext.HttpContext.Session.GetMenu("User", "AccSection");
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
                .Where(f => f.ApvStatusAcad == 1  && f.ApvStatusExam == 1 && f.ApvStatusLib == 1 && f.ApvStatusDept == 2)
                
                .Select(e => new DeptSectionListModels
                {
                    Id = e.Id,
                    ApplyDate = e.ApplyDate,
                    MajorSubjectID = e.MajorSubject,
                    ProgramId = e.ProgramId,
                    StudentId = e.StudentId,
                    StudentName = e.StudentName,
                    StudentTypeID = e.StudentType,
                    AppStatus = e.ApvStatusAcc == 1 ? "Pending" : e.ApvStatusAcc == 2 ? "Approved" : e.ApvStatusAcc == 3 ? "Rejected" : "Unknown"
                }
            ).ToListAsync();
            viewModel.Applications = viewModel.Applications.OrderByDescending(e => e.AppStatus.StartsWith("P")).ToList();
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
        public async Task<ActionResult> Approve(IFormCollection form, int id)
        {

           
            if (menu.OPEdit==false) { return RedirectToAction("LogOut", "Login"); }
            var TotalPayable= form["Application.TotalPayable"];
            var FeeForCertificate = form["Application.FeeForCertificate"];
            var TotalPaid = form["Application.TotalPaid"];

            MailHelper mail = new MailHelper(_config);
            var application = await _Db.CertApplications.FindAsync(id);
            try
            {
                application.TotalPayable =Convert.ToDecimal( TotalPayable);
                application.TotalPaid =  Convert.ToDecimal(TotalPaid);
                application.FeeForCertificate = Convert.ToDecimal(FeeForCertificate);
                application.ApprovedByAcc = HttpContext.Session.GetUserId("User");
                application.ApvStatusAcc = 2;
                application.ApvAccDate = DateTime.Now;
                await _Db.SaveChangesAsync();
                mail.SendEmail("kmhridoynub@gmail.com", "Library Section", "Certificate Application Came", application.StudentId);
                return RedirectToAction("Index","AccSection", new Status { MessageText = "Successfully Approved " + application.StudentName + "'s application" });
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return RedirectToAction("Index", "AccSection", new Status { MessageText = "Failed To Approved " + application.StudentName + "'s application" });
            }
        }
        [HttpPost]
        public async Task<ActionResult> Reject(IFormCollection form, int id)
        {

            if (menu.OPEdit==false) { return RedirectToAction("LogOut", "Login"); }
            var TotalPayable = form["Application.TotalPayable"];
            var FeeForCertificate = form["Application.FeeForCertificate"];
            var TotalPaid = form["Application.TotalPaid"];
            var application = await _Db.CertApplications.FindAsync(id);
            try
            {
                
                application.TotalPayable = Convert.ToDecimal(TotalPayable);
                application.TotalPaid = Convert.ToDecimal(TotalPaid);
                application.FeeForCertificate = Convert.ToDecimal(FeeForCertificate);
                application.ApprovedByAcc = HttpContext.Session.GetUserId("User");
                application.ApvStatusAcc = 3;
                application.ApvAccDate = DateTime.Now;
                await _Db.SaveChangesAsync();

                return RedirectToAction("Index", "AccSection", new Status { MessageText = "Successfully Rejected " + application.StudentName + "'s application" });
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return RedirectToAction("Index", "AccSection", new Status { MessageText = "Failed To Rejected " + application.StudentName + "'s application" });
            }
        }
    }
}
