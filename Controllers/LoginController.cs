using CertificationMS.ContextModels;
using CertificationMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Controllers
{
    public class LoginController : Controller
    {

        public readonly CertificateMSV2Context _Db;
        public IConfiguration config { get; set; }
        public LoginController(CertificateMSV2Context Db,IConfiguration _config)
        {
            config=_config;
            _Db = Db;
        }
        public ActionResult LogIn(string Message = "")
        {
            LoginViewModel viewModel = new LoginViewModel();
            if (Message != null)
            {
                viewModel.Message =  Message=="" ? "" : Message;
            }

            return View(viewModel);
        }
        public async Task<ActionResult> CreateToken(LoginViewModel model)
        {
            string message = "";
            LoggedInModel LoginResp = new LoggedInModel();
            //Password hash maker
            model.user.Password = Hashgenerator.GetPassHash(model.user.Password);
            string userdata = JsonConvert.SerializeObject(model.user);
            
            try
            {
                Storeproc storeproc = new Storeproc();
                SqlConnection sqlConnection = new SqlConnection(config.GetConnectionString("AppCon"));
                SqlParameter[] parameters = new SqlParameter[2];
                SqlParameter Empid = new SqlParameter(parameterName: "@EmployeeID", dbType: System.Data.SqlDbType.NVarChar);
                Empid.Value = model.user.EmployeeId;
                SqlParameter Password = new SqlParameter(parameterName: "@Password", dbType: System.Data.SqlDbType.NVarChar);
                Password.Value = model.user.Password;
                parameters[0] = Empid;
                parameters[1] = Password;
                var result = storeproc.GetDataSet(sqlConnection, "[dbo].[GetUserMenus]", parameters);

                LoginResp.EmpMenuList = DtToList.ConvertDataTable<EmpMenus>(result.Tables[0]);
                var r = DtToList.ConvertDataTable<EmpInfo>(result.Tables[1]);
                if (r.Count < 1 || r==null)
                {
                    message = "Invalid Credential";
                    return RedirectToAction("LogIn", new { message = message });
                }
                LoginResp.empInfo=r[0];
                if (LoginResp.EmpMenuList.Count<1 || LoginResp.EmpMenuList == null)
                {
                    message = "Sorry!!! You do not have access to this application";
                    return RedirectToAction("LogIn", new { message = message });
                }
               
                if (LoginResp.EmpMenuList.Count() != 0)
                {
                    HttpContext.Session.SetString("northern", userdata);
                    HmsConst.LoginResp = LoginResp;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    message = "you don't Have Permission";
                    return RedirectToAction("LogIn",new { message=message });
                }
            }
            catch (Exception ex)
            {
                message = "Invalid Credentials";
                return RedirectToAction("LogIn", new { message = message });
            }
        }

        // GET: LogInController/Create
        public ActionResult LogOut()
        {
            HttpContext.Session.Remove("northern");

            return RedirectToAction("LogIn");
        }
    }
}
