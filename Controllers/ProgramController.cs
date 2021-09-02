using CertificationMS.ContextModels;
using CertificationMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CertificationMS.Controllers
{
    public class ProgramController : Controller
    {
        private readonly CertificateMSContext _Db;

        public ProgramController(CertificateMSContext Db)
        {
            _Db = Db;
        }

        public async Task<IActionResult> List(string message)
        {

            ProgramViewModel model = new ProgramViewModel();
            var list = await _Db.Programs.ToListAsync();
            model.list = list;
            model.message = message;
            return View(model);
        }


        public IActionResult Create()
        {
            return View();
        }


        public async Task<IActionResult> Add(CertificationMS.ContextModels.Program obj)
        {
            try
            {
                _Db.Programs.Add(obj);
                await _Db.SaveChangesAsync();
                return RedirectToAction("List", new
                {
                    message = "Successfully Added " + obj.ProgramName

                });

            }
            catch (Exception)
            {
                return RedirectToAction("List", new
                {
                    message = "Failed to add " + obj.ProgramName

                });
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _Db.Programs.FindAsync(id);
            try
            {
                _Db.Programs.Remove(item);
                await _Db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "Successfully Delete " + item.ProgramName });
            }
            catch
            {
                return RedirectToAction("List", new { message = " Delete failed " + item.ProgramName });

            }

        }





        public async Task<IActionResult> Edit(int id)
        {
            ContextModels.Program  obj = await _Db.Programs.SingleOrDefaultAsync(e => e.Id == id);
            return View(obj);
        }
        public async Task<IActionResult> Update(ContextModels.Program obj)
        {
            try
            {
                _Db.Entry(obj).State = EntityState.Modified;
                await _Db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "success " + obj.ProgramName });
            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = "Failed to Update" + obj.ProgramName });
            }
        }


    }
}
