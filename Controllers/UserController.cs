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
    [SessionTimeout]
    public class UserController : Controller
    {
        public IConfiguration _Config { get; set; }
        private string? Session;
        private EmpMenus menu = new EmpMenus();

        public static List<UserRole> UserRoleList01 = null;
        public static List<UserGroup> UserGroupList01 = null;
        public static List<HRMMenus> HRMMenuList01 = null;
        CertificateMSV2Context _Db;
        public int UserId { get; set; }

        public UserController(IConfiguration configuration, CertificateMSV2Context context, IHttpContextAccessor httpContext)
        {

            menu = httpContext.HttpContext.Session.GetMenu("User", "User");
            UserId = httpContext.HttpContext.Session.GetUserId("User");
            _Config = configuration;
            _Db = context;
        }


        public async Task<IActionResult> Index(string message = "")
        {
            if (menu == null) { return RedirectToAction("Index", "Admin"); }
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
            if (menu.OPAdd != false) { return RedirectToAction("Index", "User"); }
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
            tblUser.CreatedBy = UserId.ToString();
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
        public async Task<ActionResult> Edit(int id)
        {
            UserCreateModel model = new UserCreateModel();
            model.ListDesignations = await _Db.PrmDesignations.ToListAsync();
            model.ListSection = await _Db.Sections.ToListAsync();
            model.ListGroup = await _Db.TblGroups.ToListAsync();
            try
            {
                model.User = await _Db.TblUsers.Select(e => new UserCreateForm {
                    UserId = e.UserId, Comment = e.Comment, Designation = e.Designation, EmailAddress = e.EmailAddress,
                    GroupId = e.GroupId, LoginId = e.LoginId, Name = e.Name, Password = e.Password, Phone = e.Phone,
                    Photo = e.Photo, SectionId = e.SectionId, Status = e.Status
                }).SingleOrDefaultAsync(e=>e.UserId==id);
                return View(model);
            }
            catch(Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
           
            //return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserCreateForm User)
        {
            TblUser? tblUser = new TblUser();
            tblUser = await _Db.TblUsers.AsNoTracking().SingleOrDefaultAsync(e => e.LoginId == User.LoginId);
            var r = User;
            var file = Request.Form.Files["User.Photo"];
            if (tblUser == null)
            {
                return RedirectToAction("Index", new {  message = "Failed!!!Something Weng Wrong" });
            }
            if (file != null)
            {
                if (file.Length > 0)
                {
                    User.Photo = ConvertToBytes(file);
                }
            }
            tblUser.Comment = User.Comment;
            tblUser.CreatedBy = UserId.ToString();
            tblUser.CreatedDate = DateTime.Now;
            tblUser.Designation = User.Designation;
            tblUser.EmailAddress = User.EmailAddress;
            tblUser.Name = User.Name;
            tblUser.Photo = User.Photo==null?tblUser.Photo:User.Photo;
            tblUser.SectionId = User.SectionId;
            tblUser.Status = true;
            tblUser.UpdatedBy = "";
            tblUser.Phone = User.Phone;
            tblUser.NeverExperied = false;
            tblUser.Password = User.Password!=null? Hashgenerator.GetPassHash(User.Password): tblUser.Password;
            tblUser.IsLockedOut = false;
            tblUser.GroupId = User.GroupId;
            tblUser.ChangePasswordAtFirstLogin = false;
            tblUser.LoginId = User.LoginId;
            try
            {
                 _Db.TblUsers.Update(tblUser);
                await _Db.SaveChangesAsync();
                return RedirectToAction("Index", new { message = "Successfully Created User for:" + User.Name });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { message = "Failed!!!Something Weng Wrong" });
            }
        }
        public async Task<ActionResult> Delete(int Id)
        {
            return RedirectToAction(nameof(Index), new { message = "Successfully!!!Deleted User" });
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