using EMSJu.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSJu.Models
{
    public class LoginModel
    {
        public string EmployeeId { get; set; }
        public string Password { get; set; }

    }
    public class LoginViewModel
    {
        public LoginModel user { get; set; }
        public string Message { get; set; }
    }
    public class MenusViewModel
    {
        public List<TblMenu> List { get; set; }
        public string message { get; set; }
    }
    public class MenuCreateViewModel
    {
        public List<TblMenu> List { get; set; }
        public TblMenu Menu { get; set; }
    }
}
