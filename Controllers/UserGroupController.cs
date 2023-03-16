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
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EMSJu.Controllers
{
    [SessionTimeout]
    public class UserGroupController : Controller
    {
        public HttpClient _client = new HttpClient();
        public IConfiguration _Config { get; set; }
        private string? Session;
        private EmpMenus menu = new EmpMenus();

        public static List<UserRole> UserRoleList01 = null;
        public static List<UserGroup> UserGroupList01 = null;
        public static List<HRMMenus> HRMMenuList01 = null;
        public UserGroupController(IConfiguration configuration, IHttpContextAccessor httpContext)
        {
            menu = httpContext.HttpContext.Session.GetMenu("User", "MenusSetup");
            _Config = configuration;
            Inital();
        }


        private async Task Inital()
        {
            if (UserGroupList01 == null || UserGroupList01.Count == 0)
                GetDatas("0", "AllUserGroupInfo");

            if (UserRoleList01 == null) GetDatas("0", "AllRoleInfo");
        }


        public async Task<IActionResult> Index()
        {
            if (menu==null) { return RedirectToAction("Logout", "login"); }
            RoleFormViewModel model = new RoleFormViewModel();

            model.RoleList = UserRoleList01;
            if (UserGroupList01 == null || UserRoleList01 == null)
                await Inital();

            model.GroupList = UserGroupList01;

            model.MenuList = HRMMenuList01;
            return View(model);
        }
        public async Task<IActionResult> Create()
        {
            if (!menu.OPAdd) { return RedirectToAction("Logout", "login"); }
            RoleFormViewModel model = new RoleFormViewModel();
            GetDatas("0", "AllRoleInfo");
            model.RoleList = UserRoleList01;

            foreach (var item in model.RoleList)
                item.IsAssignedRole = false;

            return View(model);
        }
        public IActionResult Edit(string grp_id)
        {
            if (!menu.OPEdit) { return RedirectToAction("Logout", "login"); }
            var single_usr_grp = UserGroupList01.Where(x => x.GroupId == int.Parse(grp_id)).ToList();

            RoleFormViewModel roleFormView = new RoleFormViewModel();
            List<UserRole> userRolesList = new List<UserRole>();
            userRolesList = UserRoleList01;
            foreach (var item in userRolesList)
                item.IsAssignedRole = false;

            foreach (var item in single_usr_grp)
            {
                foreach (var r1 in userRolesList.Where(w => w.RoleId == item.RoleId)) { r1.IsAssignedRole = true; }
            }
            roleFormView.RoleList = userRolesList;
            roleFormView.GroupList = single_usr_grp;

            return View(roleFormView);
        }


        [HttpPost]
        public ActionResult UpdateUserGroupAsync(RoleFormViewModel model)
        {
            if (!menu.OPEdit) { return RedirectToAction("Logout", "login"); }
            Storeproc storeproc = new Storeproc();
            SqlParameter[] parameters = new SqlParameter[4];
            try
            {
                model.RoleList = model.RoleList.Where(x => x.IsAssignedRole == true).ToList();
                var gropuId = model.GroupList.Select(x => x.GroupId.ToString()).FirstOrDefault();

                RoleFormViewModel model1 = new RoleFormViewModel();
                SqlConnection sqlConnection = new SqlConnection(_Config.GetConnectionString("AppCon"));
                SqlParameter ProcId = new SqlParameter(parameterName: "@ProcID", dbType: SqlDbType.NVarChar);
                ProcId.Value = "UPDATE_GROUPINF";

                SqlParameter Dxml01 = new SqlParameter(parameterName: "@Pxml01", dbType: SqlDbType.NVarChar);
                Dxml01.Value = ListToXml.ToXml<List<UserGroup>>(model.GroupList, "ds");

                SqlParameter Dxml02 = new SqlParameter(parameterName: "@Pxml02", dbType: SqlDbType.NVarChar);
                Dxml02.Value = ListToXml.ToXml<List<UserRole>>(model.RoleList, "ds");

                SqlParameter Desc01 = new SqlParameter(parameterName: "@Desc01", dbType: SqlDbType.NVarChar);
                Desc01.Value = gropuId;

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

                        model1.Message01 = "Can't Insert Duplicate Role Name (" + model.RoleList.Select(x => x.RoleName).FirstOrDefault() + ")";
                        model1.Message02 = "danger";
                    }
                    else
                    {
                        model1.Message01 = "Something went wrong!";
                        model1.Message02 = "danger";
                    }
                }
                else
                {
                    model1.Message01 = ds.Tables[0].Rows[0]["msg"].ToString();
                    model1.Message02 = ds.Tables[0].Rows[0]["msg02"].ToString();
                    if (ds.Tables[1] != null)
                        model1.GroupList = DtToList.ConvertDataTable<UserGroup>(ds.Tables[1]);
                }
                
                //  Get All Menu List
                UserGroupList01 = model1.GroupList;

                if (UserGroupList01.Count > 0)
                    return RedirectToAction("Index");
                else
                    return View(model);

            }
            catch (Exception ex)
            {
                return Json("");
                throw;
            }
        }

       
        private void GetDatas(string menuid, string type)
        {
            if (menu==null) { return; }
            List<HRMMenus> menus = new List<HRMMenus>();
            List<UserRole> roles = new List<UserRole>();
            List<UserGroup> groups = new List<UserGroup>();
            Storeproc storeproc = new Storeproc();
            SqlConnection sqlConnection2 = new SqlConnection(_Config.GetConnectionString("AppCon"));

            try
            {
                //Gets Attendance Data
                SqlParameter[] parameters2 = new SqlParameter[3];
                SqlParameter ProcId = new SqlParameter(parameterName: "@ProcID", dbType: System.Data.SqlDbType.NVarChar);
                ProcId.Value = "AUDITMENUS_INFO01";
                SqlParameter desc01 = new SqlParameter(parameterName: "@Desc01", dbType: System.Data.SqlDbType.NVarChar);
                desc01.Value = type;

                SqlParameter desc02 = new SqlParameter(parameterName: "@Desc02", dbType: System.Data.SqlDbType.NVarChar);
                desc02.Value = menuid;

                parameters2[0] = desc01;
                parameters2[1] = ProcId;
                parameters2[2] = desc02;


                var result2 = storeproc.GetDataSet(sqlConnection2, "[dbo].[SP_HRM_MENU_CONFIG_01]", parameters2);
                if (result2.Tables[0].Columns.Contains("errornumber"))
                {
                    return;
                }
                else
                {
                    if (type == "AllRoleInfo")
                    {
                        UserRoleList01 = DtToList.ConvertDataTable<UserRole>(result2.Tables[0]);
                        
                    }
                    else if (type == "AllUserGroupInfo")
                    {
                        UserGroupList01 = DtToList.ConvertDataTable<UserGroup>(result2.Tables[0]);
                       
                    }
                    else
                    {
                        menus = DtToList.ConvertDataTable<HRMMenus>(result2.Tables[0]);
                        
                    }
                }

            }
            catch (System.Exception ex)
            {
                return;
            }
        }


    }
}
