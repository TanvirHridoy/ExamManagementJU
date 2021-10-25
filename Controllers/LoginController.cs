//using CertificationMS.ContextModels;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CertificationMS.Controllers
//{
//    public class LoginController : Controller
//    {
//        //private readonly UserManager<AspNetUser> _user;
//        //private readonly SignInManager<AspNetUser> _signIn;
//        //public readonly CertificateMSContext _Db;
        
//        public LoginController(UserManager<AspNetUser> userManager,SignInManager<AspNetUser> signInManager, CertificateMSContext Db)
//        {
//            //_user = userManager;
//            //_signIn = signInManager;
//            //_Db = Db;
//        }
//        public async Task<IActionResult> Index()
//        {
//            //AspNetUser user = new AspNetUser { UserName = "9009", Email = "hridoy@gmail.com", EmailConfirmed=true, PhoneNumber="01755091665" };
//            //user.PasswordHash = _user.PasswordHasher.HashPassword(user, "RoktimLovesLima");
//            //await _Db.AspNetUsers.AddAsync(user);
//            //await _Db.SaveChangesAsync();
//            return View();
//        }
//    }
//}
