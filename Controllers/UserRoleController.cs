//using CertificationMS.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Threading.Tasks;

//namespace CertificationMS.Controllers
//{
//    public class UserRoleController : Controller
//    {
//        public HttpClient _client = new HttpClient();
//        public IConfiguration _Config { get; set; }
//        private string? Session;
//        private EmpMenus menu = new EmpMenus();

//        public static List<UserRole> UserRoleList01 = null;
//        public static List<UserGroup> UserGroupList01 = null;
//        public static List<HRMMenus> HRMMenuList01 = null;
//        public UserRoleController(IConfiguration configuration)
//        {
//            if (HmsConst.LoginResp != null)
//            {
//                menu = HmsConst.LoginResp.EmpMenuList.Where(e => e.MenuName == "MenusSetup").SingleOrDefault();


//                HRMMenuList01 = Utility.HRMMenus(HmsConst.LoginResp.EmpMenuList);

//            }
//            _Config = configuration;
//            string RootUrl = _Config.GetValue<string>("ApiUrl");
//            _client.BaseAddress = new Uri(RootUrl);
//        }
//        public async Task<IActionResult> IndexAsync()
//        {

//            RoleFormViewModel model = new RoleFormViewModel();

            
            
//            try
//            {
//                var res = await _client.GetFromJsonAsync<List<UserRole>>("UMenus/0/AllRoleInfo");
              
//                model.RoleList = res;
//                UserRoleList01 = res;
//                return View(model);
//            }
//            catch (Exception ex)
//            {
//                return View();
//                throw;
//            }
//        }
//        public async Task<IActionResult> EditAsync(string role_id)
//        {
//            if (role_id == null)
//                return RedirectToAction("Index");
//            if (UserRoleList01 == null)
//                return RedirectToAction("Index");


//            var single_usr_role = UserRoleList01.Where(x => x.RoleId == int.Parse(role_id)).ToList();

//            RoleFormViewModel roleFormView = new RoleFormViewModel();
//            List<UserRole> userRolesList = new List<UserRole>();
//            userRolesList = UserRoleList01;
//            List<HRMMenus> empMenuList = new List<HRMMenus>();
//            empMenuList = await _client.GetFromJsonAsync<List<HRMMenus>>("UMenus/0/ALL");

//            List<HRMMenus> userMenuList01 = await _client.GetFromJsonAsync<List<HRMMenus>>("UMenus/"+ role_id + "/MenuInRole01"); ;//AuditDataAccess<HRMMenus>.AuditGetDataList01(_spName: "SP_AUDIT_GetUserMenus", ProcessID: "GET_USERCONFIG", _param01: "MenuInRole", _param02: role_id);

//            foreach (var item in empMenuList)
//            {
//                item.IsAssignedMenu = false;
//                item.IsAddAssigned = false;
//                item.IsEditAssigned = false;
//                item.IsDeleteAssigned = false;
//                item.IsCancelAssigned = false;
//                item.IsPrintAssigned = false;
//            }

//            foreach (var item in empMenuList)
//            {
//                foreach (var item2 in userMenuList01)
//                {
//                    if (item.MenuId == item2.MenuId)
//                    {
//                        item.IsAssignedMenu = true;
//                        item.ParentMenuId = item2.ParentMenuId;
//                        if (item2.IsAddAssigned == true) item.IsAddAssigned = true;
//                        if (item2.IsEditAssigned == true) item.IsEditAssigned = true;
//                        if (item2.IsCancelAssigned == true) item.IsCancelAssigned = true;
//                        if (item2.IsDeleteAssigned == true) item.IsDeleteAssigned = true;
//                        if (item2.IsPrintAssigned == true) item.IsPrintAssigned = true;

//                        break;
//                    }
//                }
//            }

//            roleFormView.RoleList = single_usr_role;
//            roleFormView.MenuList = empMenuList;

//            return View(roleFormView);
//        }
//        public async Task<IActionResult> Create()
//        {

           
//            try
//            {
//                var res = await _client.GetFromJsonAsync<List<HRMMenus>>("UMenus/0/ALL");
//                RoleFormViewModel model = new RoleFormViewModel();

//                // Get All Menu List

//                model.MenuList = res; // AuditDataAccess<AuditMenus>.AuditGetDataList01(ProcessID: "AUDITMENUS_INFO01", _param01: "ALL", _param02: "", _param03: "", _param04: "", _param05: ""); ;// AuditALLMenuList01;
//                return View(model);
//            }
//            catch (Exception ex)
//            {
//                return View();
//                throw;
//            }




//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> UpdateUserRoleAsync(RoleFormViewModel model)
//        {
            
           
//            try
//            {
//                var res = await _client.PostAsJsonAsync<RoleFormViewModel>("URoles", model);
//                RoleFormViewModel model2 = new RoleFormViewModel();

//                model2 = await res.Content.ReadFromJsonAsync<RoleFormViewModel>();
//                //  Get All Menu List
//                UserRoleList01 = model2.RoleList;
//                // model.MenuList = res.; // AuditDataAccess<AuditMenus>.AuditGetDataList01(ProcessID: "AUDITMENUS_INFO01", _param01: "ALL", _param02: "", _param03: "", _param04: "", _param05: ""); ;// AuditALLMenuList01;
//                return Json(model2);
//            }
//            catch (Exception ex)
//            {
//                return Json("");
//                throw;
//            }

//        }
//    }
//}
