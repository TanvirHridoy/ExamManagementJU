using CertificationMS.ContextModels;
using CertificationMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            return View(viewModel);
        }
        // aniruddho hcnaged again by hridoy
        public IActionResult Privacy()
        {
            return View();
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
