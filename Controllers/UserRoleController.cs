using EMSJu.Models;
using EMSJu.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EMSJu.Controllers
{
    [SessionTimeout]
    public class UserRoleController : Controller
    {
        public IConfiguration _Config { get; set; }
        private string? Session;
        private EmpMenus menu = new EmpMenus();

        public static List<UserRole> UserRoleList01 = null;
        public static List<UserGroup> UserGroupList01 = null;
        public static List<HRMMenus> HRMMenuList01 = null;
        public UserRoleController(IConfiguration configuration, IHttpContextAccessor httpContext)
        {
            menu = httpContext.HttpContext.Session.GetMenu("User", "MenusSetup");

            _Config = configuration;
        }
        public async Task<IActionResult> IndexAsync()
        {

            RoleFormViewModel model = new RoleFormViewModel();
            Storeproc storeproc = new Storeproc();
            SqlConnection sqlConnection2 = new SqlConnection(_Config.GetConnectionString("AppCon"));

            try
            {
                //Gets Attendance Data
                SqlParameter[] parameters2 = new SqlParameter[3];
                SqlParameter ProcId = new SqlParameter(parameterName: "@ProcID", dbType: System.Data.SqlDbType.NVarChar);
                ProcId.Value = "AUDITMENUS_INFO01";
                SqlParameter desc01 = new SqlParameter(parameterName: "@Desc01", dbType: System.Data.SqlDbType.NVarChar);
                desc01.Value = "AllRoleInfo";

                SqlParameter desc02 = new SqlParameter(parameterName: "@Desc02", dbType: System.Data.SqlDbType.NVarChar);
                desc02.Value = "0";

                parameters2[0] = desc01;
                parameters2[1] = ProcId;
                parameters2[2] = desc02;


                var result2 = storeproc.GetDataSet(sqlConnection2, "[dbo].[SP_HRM_MENU_CONFIG_01]", parameters2);
                if (result2.Tables[0].Columns.Contains("errornumber"))
                {
                    return View();
                }
                else
                {

                    model.RoleList = DtToList.ConvertDataTable<UserRole>(result2.Tables[0]);
                    UserRoleList01 = model.RoleList;
                   return View(model);
                }
            }
            catch (Exception ex)
            {
                return View();
                throw;
            }
        }

        public async Task<IActionResult> EditAsync(string role_id)
        {
            if (role_id == null)
                return RedirectToAction("Index");
            if (UserRoleList01 == null)
                return RedirectToAction("Index");


            var single_usr_role = UserRoleList01.Where(x => x.RoleId == int.Parse(role_id)).ToList();

            RoleFormViewModel roleFormView = new RoleFormViewModel();
            List<UserRole> userRolesList = new List<UserRole>();
            userRolesList = UserRoleList01;
            List<HRMMenus> empMenuList = new List<HRMMenus>();
            Storeproc storeproc = new Storeproc();
            Storeproc storeproc2 = new Storeproc();
            SqlConnection sqlConnection1 = new SqlConnection(_Config.GetConnectionString("AppCon"));
            SqlConnection sqlConnection2 = new SqlConnection(_Config.GetConnectionString("AppCon"));

            try
            {
                //Gets Attendance Data
                SqlParameter[] parameters2 = new SqlParameter[3];
                SqlParameter ProcId = new SqlParameter(parameterName: "@ProcID", dbType: System.Data.SqlDbType.NVarChar);
                ProcId.Value = "AUDITMENUS_INFO01";
                SqlParameter desc01 = new SqlParameter(parameterName: "@Desc01", dbType: System.Data.SqlDbType.NVarChar);
                desc01.Value = "ALL";

                SqlParameter desc02 = new SqlParameter(parameterName: "@Desc02", dbType: System.Data.SqlDbType.NVarChar);
                desc02.Value = "0";

                parameters2[0] = desc01;
                parameters2[1] = ProcId;
                parameters2[2] = desc02;


                var result2 = storeproc.GetDataSet(sqlConnection2, "[dbo].[SP_HRM_MENU_CONFIG_01]", parameters2);
                if (result2.Tables[0].Columns.Contains("errornumber"))
                {
                    return View();
                }
                else
                {

                    empMenuList = DtToList.ConvertDataTable<HRMMenus>(result2.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                return View();
                throw;
            }
            List<HRMMenus> userMenuList01 = new List<HRMMenus>();
            try
            {
                //Gets Attendance Data
                SqlParameter[] parameters = new SqlParameter[3];
                SqlParameter ProcId2 = new SqlParameter(parameterName: "@ProcID", dbType: System.Data.SqlDbType.NVarChar);
                ProcId2.Value = "AUDITMENUS_INFO01";
                SqlParameter desc012 = new SqlParameter(parameterName: "@Desc01", dbType: System.Data.SqlDbType.NVarChar);
                desc012.Value = "MenuInRole01";

                SqlParameter desc022 = new SqlParameter(parameterName: "@Desc02", dbType: System.Data.SqlDbType.NVarChar);
                desc022.Value = role_id;

                parameters[0] = desc012;
                parameters[1] = ProcId2;
                parameters[2] = desc022;


                var result2 = storeproc.GetDataSet(sqlConnection1, "[dbo].[SP_HRM_MENU_CONFIG_01]", parameters);
                if (result2.Tables[0].Columns.Contains("errornumber"))
                {
                    return View();
                }
                else
                {

                    userMenuList01 = DtToList.ConvertDataTable<HRMMenus>(result2.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                return View();
                throw;
            }
            foreach (var item in empMenuList)
            {
                item.IsAssignedMenu = false;
                item.IsAddAssigned = false;
                item.IsEditAssigned = false;
                item.IsDeleteAssigned = false;
                item.IsCancelAssigned = false;
                item.IsPrintAssigned = false;
            }

            foreach (var item in empMenuList)
            {
                foreach (var item2 in userMenuList01)
                {
                    if (item.MenuId == item2.MenuId)
                    {
                        item.IsAssignedMenu = true;
                        item.ParentMenuId = item2.ParentMenuId;
                        if (item2.IsAddAssigned == true) item.IsAddAssigned = true;
                        if (item2.IsEditAssigned == true) item.IsEditAssigned = true;
                        if (item2.IsCancelAssigned == true) item.IsCancelAssigned = true;
                        if (item2.IsDeleteAssigned == true) item.IsDeleteAssigned = true;
                        if (item2.IsPrintAssigned == true) item.IsPrintAssigned = true;

                        break;
                    }
                }
            }

            roleFormView.RoleList = single_usr_role;
            roleFormView.MenuList = empMenuList;

            return View(roleFormView);
        }
        public async Task<IActionResult> Create()
        {
            
                Storeproc storeproc = new Storeproc();
               // var res = await _client.GetFromJsonAsync<List<HRMMenus>>("UMenus/0/ALL");
                RoleFormViewModel model = new RoleFormViewModel();
                SqlConnection sqlConnection = new SqlConnection(_Config.GetConnectionString("AppCon"));
                try
                {
                    //Gets Attendance Data
                    SqlParameter[] parameters2 = new SqlParameter[3];
                    SqlParameter ProcId = new SqlParameter(parameterName: "@ProcID", dbType: System.Data.SqlDbType.NVarChar);
                    ProcId.Value = "AUDITMENUS_INFO01";
                    SqlParameter desc01 = new SqlParameter(parameterName: "@Desc01", dbType: System.Data.SqlDbType.NVarChar);
                    desc01.Value = "ALL";

                    SqlParameter desc02 = new SqlParameter(parameterName: "@Desc02", dbType: System.Data.SqlDbType.NVarChar);
                    desc02.Value = "0";

                    parameters2[0] = desc01;
                    parameters2[1] = ProcId;
                    parameters2[2] = desc02;


                    var result2 = storeproc.GetDataSet(sqlConnection, "[dbo].[SP_HRM_MENU_CONFIG_01]", parameters2);
                    if (result2.Tables[0].Columns.Contains("errornumber"))
                    {
                        return View();
                    }
                    else
                    {

                        model.MenuList = DtToList.ConvertDataTable<HRMMenus>(result2.Tables[0]);
                    }
                }
                catch (Exception ex)
                {
                    return View();
                    throw;
                }
                return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUserRoleAsync(RoleFormViewModel model)
        {
            Storeproc storeproc = new Storeproc();
            SqlParameter[] parameters = new SqlParameter[4];

            var selectedMenuList = model.MenuList.Where(x => x.IsAddAssigned != false || x.IsEditAssigned != false || x.IsPrintAssigned != false || x.IsDeleteAssigned != false || x.IsCancelAssigned != false || x.ParentMenuId == 0).ToList();
            var roleId = model.RoleList.Select(x => x.RoleId.ToString()).FirstOrDefault();

            try
            {
                RoleFormViewModel model2 = new RoleFormViewModel();
                SqlConnection sqlConnection = new SqlConnection(_Config.GetConnectionString("AppCon"));
                SqlParameter ProcId = new SqlParameter(parameterName: "@ProcID", dbType: SqlDbType.NVarChar);
                ProcId.Value = "UPDATE_ROLEINF";

                SqlParameter Dxml01 = new SqlParameter(parameterName: "@Pxml01", dbType: SqlDbType.NVarChar);
                Dxml01.Value = ListToXml.ToXml<List<UserRole>>(model.RoleList, "ds");

                SqlParameter Dxml02 = new SqlParameter(parameterName: "@Pxml02", dbType: SqlDbType.NVarChar);
                Dxml02.Value = ListToXml.ToXml<List<HRMMenus>>(selectedMenuList, "ds");

                SqlParameter Desc01 = new SqlParameter(parameterName: "@Desc01", dbType: SqlDbType.NVarChar);
                Desc01.Value = roleId;

                parameters[0] = ProcId;
                parameters[1] = Dxml01;
                parameters[2] = Dxml02;
                parameters[3] = Desc01;

                var ds = storeproc.GetDataSet(sqlConnection, "SP_HRM_MENU_CONFIG_01", parameters);

                if (ds.Tables[0].Columns.Contains("errornumber"))
                {
                    string? msg = ds.Tables[0].Rows[0]["errormessage"].ToString();

                    if (msg.Contains("duplicate key"))
                    {

                        model2.Message01 = "Can't Insert Duplicate Role Name (" + model.RoleList.Select(x => x.RoleName).FirstOrDefault() + ")";
                        model2.Message02 = "danger";
                        
                    }
                    else
                    {
                        model2.Message01 = "Something went wrong!";
                        model2.Message02 = "danger";
                       
                    }
                }
                else
                {
                    model2.Message01 = ds.Tables[0].Rows[0]["msg"].ToString();
                    model2.Message02 = ds.Tables[0].Rows[0]["msg02"].ToString();
                    if (ds.Tables[1] != null)
                        model2.RoleList = DtToList.ConvertDataTable<UserRole>(ds.Tables[1]);
                    
                }

                UserRoleList01 = model2.RoleList;
                // model.MenuList = res.; // AuditDataAccess<AuditMenus>.AuditGetDataList01(ProcessID: "AUDITMENUS_INFO01", _param01: "ALL", _param02: "", _param03: "", _param04: "", _param05: ""); ;// AuditALLMenuList01;
                return Json(model2);
            }
            catch (System.Exception Ex)
            {
                return Json("");
                throw;
            }
        }

       
    }
}
