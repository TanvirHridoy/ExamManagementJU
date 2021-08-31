using CertificationMS.ContextModels;
using CertificationMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Controllers
{
    public class ApprovalStatusController : Controller
    {

        public readonly CertificateMSContext _db;



        public ApprovalStatusController (CertificateMSContext Db)
        {
            _db = Db;
        }

        public async Task<IActionResult> List(string message)
        {
            ApvStatusViewModel model = new ApvStatusViewModel();
            var ApvStatusList = await _db.ApvStatuses.ToListAsync();
            model.list = ApvStatusList;
            model.message = message;
            return View(model);
        }





        public IActionResult Index()
        {
            return View();
        }
    }
}
