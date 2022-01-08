using CertificationMS.ContextModels;
using CertificationMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Controllers
{
    public class HomeController : Controller
    {
        public readonly CertificateMSV2Context _Db;
        public HomeController(CertificateMSV2Context Db)
        {
            _Db = Db;
        }

        public IActionResult Test()
        {

            return View();
        }

        public IActionResult Track()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TrackApplication(int TrackingID)
        {
            List<ApvStatus> statuses = new List<ApvStatus>();
            statuses = _Db.ApvStatuses.ToList();
            var cert = _Db.CertApplications.Where(e => e.Id == TrackingID)
            .SingleOrDefault();
            TrackingViewModel viewModel = new TrackingViewModel();
            if (cert != null)
            {
                
                viewModel.TrackId = cert.Id.ToString();
                viewModel.ApvStatusDept = statuses.Where(f => f.Id == cert.ApvStatusDept).Select(i => i.Name).SingleOrDefault();
                viewModel.ApvStatusAcad = statuses.Where(f => f.Id == cert.ApvStatusAcad).Select(i => i.Name).SingleOrDefault();
                viewModel.ApvStatusAcc = statuses.Where(f => f.Id == cert.ApvStatusAcc).Select(i => i.Name).SingleOrDefault();
                viewModel.ApvStatusLib = statuses.Where(f => f.Id == cert.ApvStatusLib).Select(i => i.Name).SingleOrDefault();
                viewModel.ApvStatusExam = statuses.Where(f => f.Id == cert.ApvStatusExam).Select(i => i.Name).SingleOrDefault();
                viewModel.ApplyDate = cert.ApplyDate.ToString("dd-MMM-yyyy");
                viewModel.ApvAcadDate = cert.ApvAcaddate==null?"---":cert.ApvAcaddate?.ToString("dd-MMM-yyyy");
                viewModel.ApvAccDate = cert.ApvAccDate == null ? "---" : cert.ApvAccDate?.ToString("dd-MMM-yyyy");
                viewModel.ApvDeptDate = cert.ApvDeptDate == null ? "---" : cert.ApvDeptDate?.ToString("dd-MMM-yyyy");
                viewModel.ApvExamDate = cert.ApvExamDate == null ? "---" : cert.ApvExamDate?.ToString("dd-MMM-yyyy");
                viewModel.ApvLibDate = cert.ApvLibDate == null ? "---" : cert.ApvLibDate?.ToString("dd-MMM-yyyy");
                viewModel.DeliveryDate = cert.DeliveryDate == null ? "---" : cert.DeliveryDate?.ToString("dd-MMM-yyyy");
              
            }
            return Json(viewModel);
        }

        public async Task<IActionResult> Index()
        {
            formViewModel viewModel = new formViewModel();
            try
            {
                viewModel.CampusLst = await _Db.Campuses.ToListAsync();
                viewModel.convLst = await _Db.Convocations.ToListAsync();
                viewModel.deptLst = await _Db.Departments.ToListAsync();
                viewModel.programLst = await _Db.Programs.ToListAsync();
                viewModel.StudentTypeLst = await _Db.StudentTypes.ToListAsync();
            }
            catch(Exception ex)
            {
                var r = ex.Message;
            }
           
            
            return View(viewModel);
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
        [HttpPost]
        public IActionResult ApplyForCertificate(CertApplication application)
        {
            CertApplication app = application;
            var file = Request.Form.Files["ImageData"];
            if (file != null)
            {
                if (file.Length > 0)
                {
                    app.ExtraOne = ConvertToBytes(file);
                }
            }

            app.ApplyDate = DateTime.Now;
            if(app.FromNubCampus!=null && app.ToNubCampus != null)
            {
                app.ChangeNubCampus = true;
            }
            
            
            app.ApvStatusDept = 1;
            app.ApvStatusAcad = 1;
            app.ApvStatusAcc = 1;
            app.ApvStatusExam = 1;
            app.ApvStatusLib = 1;
            _Db.CertApplications.Add(app);
            try
            {
                _Db.SaveChanges();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            var TrackingId = _Db.CertApplications.Where(e=>e.StudentId==app.StudentId && e.ProgramId==app.ProgramId && e.StudentName==app.StudentName).OrderBy(e=>e.ApplyDate).LastOrDefault();
            return RedirectToAction("ShowTracking", new Message { TrackingID = TrackingId.Id.ToString() });
        }

        public async Task<ActionResult> RetriveImage(int ID)
        {
            var cert = await _Db.CertApplications.SingleAsync(e => e.Id == ID);
            var file = cert.ExtraOne;
            if (file == null)
            {
                return null;
            }
            else if (file.Length > 0)
            {
                byte[] Image = file;
                return File(Image, "image/jpg");
            }
            else
            {
                return null;
            }
        }
        // aniruddho hcnaged again by hridoy

        public IActionResult ShowTracking(Message message)
        {
            return View(message);
        }


        public List<Department> LoadDept()
        {
            List<Department> Deptlist = new List<Department>();
            Deptlist = _Db.Departments.ToList();
            return Deptlist;
        }


        public List<Convocation> LoadConvocation()
        {
            List<Convocation> Convocationlist = new List<Convocation>();
            Convocationlist = _Db.Convocations.ToList();
            //     Convocationlist.Insert(0, new Convocation { Id = 0, Name = "Please Select" });
            return Convocationlist;

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
