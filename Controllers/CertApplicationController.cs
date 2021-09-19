using CertificationMS.ContextModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Controllers
{
    public class CertApplicationController : Controller
    {
        public readonly CertificateMSContext _db;


        public CertApplicationController(CertificateMSContext Db)
        {
            _db = Db;
        }

    public async Task<IActionResult> Add(CertApplication Obj)
        {
            _db.CertApplications.Add(Obj);
            await _db.SaveChangesAsync();
            return null;
        }



        public IActionResult Index()
        {
            return View();
        }

       



    }
}
