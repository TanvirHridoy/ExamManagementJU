using CertificationMS.ContextModels;
using CertificationMS.Models;
using CertificationMS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CertificationMS.Controllers
{
    public class UserController : Controller
    {
        public IConfiguration _Config { get; set; }
        private string? Session;
        private EmpMenus menu = new EmpMenus();

        public static List<UserRole> UserRoleList01 = null;
        public static List<UserGroup> UserGroupList01 = null;
        public static List<HRMMenus> HRMMenuList01 = null;
        CertificateMSV2Context _Db;

        public UserController(IConfiguration configuration, CertificateMSV2Context context, IHttpContextAccessor httpContext)
        {

            menu = httpContext.HttpContext.Session.GetMenu("User", "User");
            _Config = configuration;
            _Db = context;
        }


        public async Task<IActionResult> Index(string message = "")
        {
            UsersViewModel viewModel = new UsersViewModel();

            viewModel.ListDeg =  _Db.PrmDesignations.ToList(); 
            viewModel.List = await _Db.TblUsers.Select(e => new UsersModel
            {
                Status = e.Status,
                Designation = e.Designation,
                EmailAddress = e.EmailAddress,
                GroupId = e.GroupId,
                LoginId = e.LoginId,
                Name = e.Name,
                UserId = e.UserId
            })
            .ToListAsync();
            viewModel.message = message;
            
            return View(viewModel);
        }

        public async Task<ActionResult> Create( UserCreateForm user = null,string message="")
        {
            UserCreateModel model = new UserCreateModel();
            model.ListDesignations = await _Db.PrmDesignations.ToListAsync();
            model.ListSection = await _Db.Sections.ToListAsync();
            model.ListGroup = await _Db.TblGroups.ToListAsync();
            if (user != null)
            {
                model.User = user;
            }
            model.message = message;
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Create(UserCreateForm User)
        {
            TblUser tblUser = new TblUser();
            TblUser? U = await _Db.TblUsers.AsNoTracking().SingleOrDefaultAsync(e => e.LoginId == User.LoginId);
            var r = User;
            var file = Request.Form.Files["User.Photo"];
            if (file != null)
            {
                if (file.Length > 0)
                {
                    User.Photo = ConvertToBytes(file);
                }
            }
            if (U != null)
            {
                return RedirectToAction("Create", new { user = User, message = "Failed!!! This Login ID is already exists" });
            }
            tblUser.Comment = User.Comment;
            tblUser.CreatedBy = "";
            tblUser.CreatedDate = DateTime.Now;
            tblUser.Designation = User.Designation;
            tblUser.EmailAddress = User.EmailAddress;
            tblUser.Name = User.Name;
            tblUser.Photo = User.Photo;
            tblUser.SectionId = User.SectionId;
            tblUser.Status = true;
            tblUser.UpdatedBy = "";
            tblUser.Phone = User.Phone;
            tblUser.NeverExperied = false;
            tblUser.Password = Hashgenerator.GetPassHash(User.Password);
            tblUser.IsLockedOut = false;
            tblUser.GroupId = User.GroupId;
            tblUser.ChangePasswordAtFirstLogin = false;
            tblUser.LoginId = User.LoginId;
            try
            {
                await _Db.TblUsers.AddAsync(tblUser);
                await _Db.SaveChangesAsync();
                return RedirectToAction("Index",new { message="Successfully Created User for:"+User.Name});
            }
            catch(Exception ex)
            {
                return RedirectToAction("Create", new { user = User, message = "Failed!!! "+ex.Message });

            }
        }
        public byte[] ConvertToBytes(IFormFile image)
        {
            byte[] s = null;
            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                s = ms.ToArray();
            }
            return s;
        }
        public IActionResult Edit(string userid)
        {
            return View();
        }

        //public IActionResult Profile()
        //{
        //    string userId = HmsConst.LoginResp.EmpId;

        //    HRMUser user = new HRMUser();

        //    var emp = HmsConst.LoginResp.empInfo;

        //    user.EmpId = userId;
        //    user.Name = emp.Name;
        //    user.Designation = emp.SDesig;

        //    return View(user);

        //}

        //[HttpPost]
        //public async Task<IActionResult> UpdateProfile(HRMUser user)
        //{

        //    {
        //        await HmsConst.CreateToken(HttpContext, _Config);
        //    }
        //    try
        //    {
        //        user.PrevPassword = Hashgenerator.GetPassHash(user.PrevPassword);
        //        user.Password = Hashgenerator.GetPassHash(user.Password);

        //        var res = await _client.PostAsJsonAsync<HRMUser>("HUsers", user);
        //        HRMUserVM model = new HRMUserVM();

        //        model = await res.Content.ReadFromJsonAsync<HRMUserVM>();
        //        //  Get All Menu List

        //        return Json(model);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Json("");
        //        throw;
        //    }
        //}
        public string EncMD5(string password)
        {
            password = password == null ? "" : password;
            MD5 md5 = new MD5CryptoServiceProvider();
            UTF8Encoding encoder = new UTF8Encoding();
            byte[] originalBytes = encoder.GetBytes(password);
            Byte[] encodedBytes = md5.ComputeHash(originalBytes);
            password = BitConverter.ToString(encodedBytes).Replace("-", "");
            var result = password.ToLower();
            return result;
        }
    }

}