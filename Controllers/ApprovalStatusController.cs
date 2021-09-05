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


        public IActionResult create()
        {
            return View();
        }


        public  async Task<IActionResult> Add(ApvStatus obj)
        {
            try
            {
                _db.ApvStatuses.Add(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "Successfully Added " + obj.Name});

            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = "Failed to Add " + obj.Name });
            }

        }
        
        public async Task<IActionResult> Edit(int id)
        {
            ApvStatus obj = await _db.ApvStatuses.SingleOrDefaultAsync(e => e.Id == id);
            return View(obj);
        }

        public async Task<IActionResult> Update(ApvStatus obj)
        {
            try
            {
                _db.Entry(obj).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "Successfully Added" + obj.Name });
            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = "Failed" + obj.Name });
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var apstatus = await _db.ApvStatuses.FindAsync(id);
            try
            {
                _db.ApvStatuses.Remove(apstatus);
                await _db.SaveChangesAsync();
                return RedirectToAction("List", new { message = "SuccessfullyDelete " });
            }
            catch (Exception)
            {
                return RedirectToAction("List", new { message = " Failed to delete "  });
            }
        }










    }

   

}


