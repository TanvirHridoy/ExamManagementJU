//using CertificationMS.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Net.Http.Json;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;

//namespace CertificationMS.Controllers
//{
//    public class UserController : Controller
//    {
//        public HttpClient _client = new HttpClient();
//        public IConfiguration _Config { get; set; }
//        private string? Session;
//        private EmpMenus menu = new EmpMenus();

//        public static List<UserRole> UserRoleList01 = null;
//        public static List<UserGroup> UserGroupList01 = null;
//        public static List<HRMMenus> HRMMenuList01 = null;

//        public UserController(IConfiguration configuration)
//        {
//            if (HmsConst.LoginResp != null)
//            {
//                menu = HmsConst.LoginResp.EmpMenuList.Where(e => e.MenuName == "MenusSetup").SingleOrDefault();
//            }
//            _Config = configuration;
//            string RootUrl = _Config.GetValue<string>("ApiUrl");
//            _client.BaseAddress = new Uri(RootUrl);
//        }


//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Create()
//        {


//            return View();
//        }

//        public IActionResult Edit(string userid)
//        {
//            return View();
//        }

//        public IActionResult Profile()
//        {
//            string userId = HmsConst.LoginResp.EmpId;

//            HRMUser user = new HRMUser();

//            var emp = HmsConst.LoginResp.empInfo;

//            user.EmpId = userId;
//            user.Name = emp.Name;
//            user.Designation = emp.SDesig;

//            return View(user);

//        }

//        [HttpPost]
//        public async Task<IActionResult> UpdateProfile(HRMUser user)
//        {
            
//            {
//                await HmsConst.CreateToken(HttpContext, _Config);
//            }
//            try
//            {
//                user.PrevPassword = Hashgenerator.GetPassHash(user.PrevPassword);
//                user.Password = Hashgenerator.GetPassHash(user.Password);

//                var res = await _client.PostAsJsonAsync<HRMUser>("HUsers", user);
//                HRMUserVM model = new HRMUserVM();

//                model = await res.Content.ReadFromJsonAsync<HRMUserVM>();
//                //  Get All Menu List
              
//                return Json(model);

//            }
//            catch (Exception ex)
//            {
//                return Json("");
//                throw;
//            }
//        }
//        public string EncMD5(string password)
//        {
//            password = password == null ? "" : password;
//            MD5 md5 = new MD5CryptoServiceProvider();
//            UTF8Encoding encoder = new UTF8Encoding();
//            byte[] originalBytes = encoder.GetBytes(password);
//            Byte[] encodedBytes = md5.ComputeHash(originalBytes);
//            password = BitConverter.ToString(encodedBytes).Replace("-", "");
//            var result = password.ToLower();
//            return result;
//        }
//    }

//}