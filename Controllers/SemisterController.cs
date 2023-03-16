using EMSJu.ContextModels;
using EMSJu.Models;
using EMSJu.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSJu.Controllers
{
    public class SemisterController : Controller
    {
        private readonly CertificateMSV2Context _Db;
        private string? Session;
        private EmpMenus menu = new EmpMenus();
        public SemisterController(CertificateMSV2Context Db, IHttpContextAccessor HttpContext)
        {
            menu = HttpContext.HttpContext.Session.GetMenu("User", "Student");

            _Db = Db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> List(string message)
        {
            if (menu == null) { return RedirectToAction("LogOut", "Login"); }
            SemisterViewModel model = new SemisterViewModel();
            var semlist = await _Db.TblSemisters.ToListAsync();
            model.list = semlist;
            model.message = message;
            return View(model);
        }

        public ActionResult Create()
        {
            TblSemister model = new TblSemister();
            if (menu.OPAdd == false) { return RedirectToAction(nameof(List)); }
            return View(model);
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TblSemister Model)
        {
            if (menu.OPAdd == false) { return RedirectToAction(nameof(List)); }

            try
            {
                await _Db.TblSemisters.AddAsync(Model);
                await _Db.SaveChangesAsync();
                return RedirectToAction(nameof(List), new { message = $"Successfully!!! Added Semister Name:{Model.SemisterName}" });
            }
            catch
            {
                return RedirectToAction(nameof(List), new { message = $"Failed!!! Couldn't Add Semister Name {Model.SemisterName}" });
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (menu.OPEdit == false) { return RedirectToAction(nameof(List)); }
            TblSemister model = new TblSemister();
            model = await _Db.TblSemisters.SingleOrDefaultAsync(e => e.SemisterId == id);
            if (model.SemisterId == 0) { return RedirectToAction(nameof(List)); }
            return View(model);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TblSemister Model)
        {
            if (menu.OPEdit == false) { return RedirectToAction(nameof(List)); }
            try
            {
                _Db.TblSemisters.Update(Model);
                await _Db.SaveChangesAsync();
                return RedirectToAction(nameof(List), new { message = $"Successfully!!! Updated SemisterName:{Model.SemisterName}" });
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(List), new { message = $"Failed!!! Couldn't Update SemisterName  {Model.SemisterName}" });
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
            if (menu.OPDelete == false) { return RedirectToAction(nameof(List)); }
            var model = await _Db.TblSemisters.FindAsync(id);

            try
            {
                _Db.TblSemisters.Remove(model);
                await _Db.SaveChangesAsync();
                return RedirectToAction(nameof(List), new { message = $"Successfully!!! Deleted Semister Name :{model.SemisterName}" });

            }
            catch
            {
                return RedirectToAction(nameof(List), new { message = $"Failed!!! Couldn't Delete Semister Name {model.SemisterName}" });

            }

        }
    }
}
