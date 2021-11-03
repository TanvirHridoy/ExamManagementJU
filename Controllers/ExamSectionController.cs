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
    public class ExamSectionController : Controller
    {
        public readonly CertificateMSContext _Db;
        public IConfiguration _config;
        public ExamSectionController(CertificateMSContext Db, IConfiguration configuration)
        {
            _Db = Db;
            _config = configuration;
        }
        // GET: AccountsSectionController
        public async Task<ActionResult> Index(Status message = null)
        {
            AccSectionViewModel viewModel = new AccSectionViewModel();
            viewModel.departments = await _Db.Departments.ToListAsync();
            viewModel.studentTypes = await _Db.StudentTypes.ToListAsync();
            viewModel.programs = await _Db.Programs.ToListAsync();
            viewModel.message = message != null ? message.MessageText : "";

            viewModel.Applications = await _Db.CertApplications
                .Where(f => f.ApvStatusAcc == 2 && f.ApvStatusAcad == 2 && f.ApvStatusLib == 2 && f.ApvStatusDept == 2)
                .Select(e => new DeptSectionListModels
                {
                    Id = e.Id,
                    ApplyDate = e.ApplyDate,
                    MajorSubjectID = e.MajorSubject,
                    ProgramId = e.ProgramId,
                    StudentId = e.StudentId,
                    StudentName = e.StudentName,
                    StudentTypeID = e.StudentType,
                    AppStatus = e.ApvStatusExam == 1 ? "Pending" : e.ApvStatusExam == 2 ? "Approved" : e.ApvStatusExam == 3 ? "Rejected" : "Unknown"
                }
            ).ToListAsync();
            return View(viewModel);
        }

        public async Task<ActionResult> Details(int id)
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

        // GET: DeptSectionController/Create
        [HttpPost]
        public async Task<ActionResult> Approve(int id)
        {
            MailHelper mail = new MailHelper(_config);
            var application = await _Db.CertApplications.FindAsync(id);
            try
            {
                application.ApprovedByExam = UserInfo.Id;
                application.ApvStatusExam = 2;
                application.ApvExamDate = DateTime.Now;
                await _Db.SaveChangesAsync();
                mail.SendEmail("kmhridoynub@gmail.com", "Delivery Notice", "Please come and Take your Certificate at:", application.StudentId);
                return RedirectToAction("Index", "ExamSection", new Status { MessageText = "Successfully Approved " + application.StudentName + "'s application" });
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return RedirectToAction("Index", "ExamSection", new Status { MessageText = "Failed To Approved " + application.StudentName + "'s application" });
            }
        }
        [HttpPost]
        public async Task<ActionResult> Reject(int id)
        {
            var application = await _Db.CertApplications.FindAsync(id);
            try
            {
                application.ApprovedByExam = UserInfo.Id;
                application.ApvStatusExam = 3;
                application.ApvExamDate = DateTime.Now;
                await _Db.SaveChangesAsync();

                return RedirectToAction("Index", "ExamSection", new Status { MessageText = "Successfully Rejected " + application.StudentName + "'s application" });
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return RedirectToAction("Index", "ExamSection", new Status { MessageText = "Failed To Rejected " + application.StudentName + "'s application" });
            }
        }
    }
}
