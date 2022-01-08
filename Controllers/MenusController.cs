using CertificationMS.ContextModels;
using CertificationMS.Models;
using CertificationMS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CertificationMS.Controllers
{
    [SessionTimeout]
    public class MenusController : Controller
    {
        public HttpClient _client = new HttpClient();
        public IConfiguration _Config { get; set; }
        private string? Session;
        private EmpMenus menu = new EmpMenus();
        private CertificateMSV2Context _Db;
        public MenusController(IConfiguration configuration, CertificateMSV2Context context, IHttpContextAccessor httpContext)
        {
            _Db=context;
            menu = httpContext.HttpContext.Session.GetMenu("User", "MenusSetup");
            _Config = configuration;

        }

        public async Task<ActionResult> Index(string message)
        {
            MenusViewModel viewModel = new MenusViewModel();
            
            if (menu == null) { return RedirectToAction("Index", "Dashboard"); }
            viewModel.message = message == null ? "" : message;
            viewModel.List = await _Db.TblMenus.ToListAsync();
            return View(viewModel);
        }

        public async Task<ActionResult> Create()
        {
            MenuCreateViewModel model = new MenuCreateViewModel();
            
            if (menu.OPAdd)
            {
                model.List = await _Db.TblMenus.ToListAsync();
                return View(model);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MenuCreateViewModel Data)
        {
            if (menu.OPAdd)
            {
                Data.Menu.MenuCaptionBng=Data.Menu.MenuCaptionBng==null ? "" : Data.Menu.MenuCaptionBng;
                Data.Menu.MenuIconClass=Data.Menu.MenuIconClass==null ? "" : Data.Menu.MenuIconClass;
                try
                {
                    await _Db.TblMenus.AddAsync(Data.Menu);
                    await _Db.SaveChangesAsync();
                    return RedirectToAction("Index", new { message = "Successfully Added Menus:" + Data.Menu.MenuName });
                }
                catch
                {
                    return RedirectToAction(nameof(Create));
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

        }


        public async Task<ActionResult> Edit(int id)
        {
            string MenusId = id.ToString();
            MenuCreateViewModel ViewModel = new MenuCreateViewModel();
           
            if (menu.OPEdit)
            {

                ViewModel.List = await _Db.TblMenus.ToListAsync();
                var Menus = ViewModel.List.SingleOrDefault(e => e.MenuId.ToString()==MenusId);
                if (Menus != null)
                {
                    ViewModel.Menu = Menus;

                }
                else
                {
                    return RedirectToAction("Index", new { message = "Sorry!!! Not Found" });
                }

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Index", new { message = "Sorry!!! You dont Have Permission " });

            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MenuCreateViewModel Data)
        {
           
            if (menu.OPEdit)
            {
                Data.Menu.MenuCaptionBng=Data.Menu.MenuCaptionBng==null ? "" : Data.Menu.MenuCaptionBng;
                Data.Menu.MenuIconClass=Data.Menu.MenuIconClass==null ? "" : Data.Menu.MenuIconClass;

                try
                {
                    var UModel = await _Db.TblMenus.AsNoTracking().Where(e => e.MenuId == Data.Menu.MenuId).SingleOrDefaultAsync();
                    UModel = Data.Menu;
                    if (UModel == null)
                    {
                        return RedirectToAction("Index", new { message = "Failed to Updated Menus: " + Data.Menu.MenuName });
                    }
                    else
                    {

                        _Db.TblMenus.Update(UModel);
                        await _Db.SaveChangesAsync();
                        return RedirectToAction("Index", new { message = "Successfully Updated Menus: " + Data.Menu.MenuName });
                    }


                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", new { message = "Failed to Updated Menus: " + Data.Menu.MenuName });
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            
            if (menu.OPDelete)
            {
                try
                {
                    var Menu = await _Db.TblMenus.FindAsync(id);
                    _Db.TblMenus.Remove(Menu);
                    await _Db.SaveChangesAsync();
                    return RedirectToAction("Index", new { message = "Successfully Deleted Menu" });

                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", new { message = "Failed to Delete Menus" });
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
