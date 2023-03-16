using EMSJu.ContextModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EMSJu.Controllers
{
    public class CertApplicationController : Controller
    {
        public readonly CertificateMSV2Context _db;

        public CertApplicationController(CertificateMSV2Context Db)
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
