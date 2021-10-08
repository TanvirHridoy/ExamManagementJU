using CertificationMS.ContextModels;
using CertificationMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Controllers
{
    public class DeptSectionController : Controller
    {
        public readonly CertificateMSContext _Db;

        public DeptSectionController(CertificateMSContext Db)
        {
            _Db = Db;
        }
        // GET: DeptSectionController
        public async Task<ActionResult> Index()
        {
            DeptSectionViewModel viewModel = new DeptSectionViewModel();
            viewModel.departments = await _Db.Departments.ToListAsync();
            viewModel.studentTypes = await _Db.StudentTypes.ToListAsync();
            viewModel.programs = await _Db.Programs.ToListAsync();

            
            viewModel.Applications = await _Db.CertApplications
                .Where(f=>f.ApvStatusAcad==1 && f.ApvStatusAcc==1 && f.ApvStatusExam==1 && f.ApvStatusLib==1)
                .Select(e => new DeptSectionListModels
            {
                Id = e.Id,
                ApplyDate = e.ApplyDate,
                MajorSubjectID = e.MajorSubject,
                ProgramId = e.ProgramId,
                StudentId = e.StudentId,
                StudentName = e.StudentName,
                StudentTypeID = e.StudentType
            }
            ).ToListAsync();
            return View(viewModel);
        }

        // GET: DeptSectionController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            ApplicationDetailsViewModel viewModel = new ApplicationDetailsViewModel();
            viewModel.Application = await _Db.CertApplications.SingleAsync(e => e.Id == id);
            viewModel.ApvStatusLst = await _Db.ApvStatuses.ToListAsync();
            viewModel.departments = await _Db.Departments.ToListAsync();
            viewModel.studentTypes = await _Db.StudentTypes.ToListAsync();
            viewModel.programs = await _Db.Programs.ToListAsync();
            viewModel.Campuses = await _Db.Campuses.ToListAsync();
            viewModel.Convocations = await _Db.Convocations.ToListAsync();
            return View(viewModel);
        }

        // GET: DeptSectionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeptSectionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DeptSectionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DeptSectionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DeptSectionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DeptSectionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
