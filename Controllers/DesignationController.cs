using CertificationMS.ContextModels;
using CertificationMS.Models;
using CertificationMS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CertificationMS.Controllers
{
    [SessionTimeout]
    public class DesignationController : Controller
    {
        public IConfiguration _Config { get; set; }
        private EmpMenus menu = new EmpMenus();
        public readonly CertificateMSV2Context _Db;

        public DesignationController(CertificateMSV2Context Db, IConfiguration configuration, IHttpContextAccessor HttpContext)
        {
            menu = HttpContext.HttpContext.Session.GetMenu("User", "DesignationSetup");
            _Db = Db;
            _Config = configuration;
        }

        public async Task<ActionResult> Index(string message)
        {
            DesignationViewModel viewModel = new DesignationViewModel();

            if (menu == null) { return RedirectToAction("Index", "Admin"); }

            viewModel.message = message == null ? "" : message;
            viewModel.List = await _Db.PrmDesignations.ToListAsync();
            return View(viewModel);
        }

        public IActionResult create()
        {
            if (menu.OPAdd == false) { return RedirectToAction(nameof(Index)); }
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PrmDesignation Data)
        {
            if (menu.OPAdd == false) { return RedirectToAction(nameof(Index)); }
            try
            {
                _Db.PrmDesignations.Add(Data);
                await _Db.SaveChangesAsync();
                return RedirectToAction("Index", new { message = "Successfully Added " + Data.Name });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { message = "Failed to add" + Data.Name });
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (menu.OPEdit == false) { return RedirectToAction(nameof(Index)); }
            PrmDesignation obj = await _Db.PrmDesignations.SingleOrDefaultAsync(e => e.Id == id);
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PrmDesignation Data)
        {
            if (menu.OPEdit == false) { return RedirectToAction(nameof(Index)); }
            try
            {
                _Db.Entry(Data).State = EntityState.Modified;
                await _Db.SaveChangesAsync();
                return RedirectToAction("Index", new { message = "Successfully Updated " + Data.Name });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "Failed to Update" + Data.Name });
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (menu.OPDelete == false) { return RedirectToAction(nameof(Index)); }
            var prmDesignation = await _Db.PrmDesignations.FindAsync(id);
            try
            {
                _Db.PrmDesignations.Remove(prmDesignation);
                await _Db.SaveChangesAsync();
                return RedirectToAction("Index", new { message = "SuccessFully Deleted:" + prmDesignation.Name });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { message = ex.Message });
            }
        }

    }
}
