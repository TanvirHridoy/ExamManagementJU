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
        public readonly CertificateMSContext _Db;

        public HomeController(CertificateMSContext Db)
        {
            _Db = Db;
        }

        public IActionResult Test()
        {

            return View();
        }


        public async Task<IActionResult> Index()
        {
            formViewModel viewModel = new formViewModel();
            viewModel.CampusLst = await _Db.Campuses.ToListAsync();
            viewModel.convLst = await _Db.Convocations.ToListAsync();
            viewModel.deptLst = await _Db.Departments.ToListAsync();
            viewModel.programLst = await _Db.Programs.ToListAsync();
            viewModel.StudentTypeLst = await _Db.StudentTypes.ToListAsync();
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
            var g = Guid.NewGuid();
            app.TrackId = g.ToString();
            _Db.CertApplications.Add(app);
            try
            {
                _Db.SaveChanges();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return RedirectToAction("ShowTracking", new Message { TrackingID = g.ToString() });
        }

        public async Task<ActionResult> RetriveImage(int ID)
        {

            var cert = await _Db.CertApplications.SingleAsync(e => e.Id == ID);
            var file = cert.ExtraOne;
            if (file.Length > 0)
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
