using EMSJu.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSJu.Utility
{
    public static class UserInfo
    {
        public static int Id { get; set; } = 121;
        public static string Name { get; set; } = "Hridoy";
        public static Department Department { get; set; } = new Department { Id = 2, DeptName = "Ecse", DeptSname = "Ecse" };
    }
}
