using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Models
{
    public class RoleFormViewModel
    {
        public UserRole Role { set; get; }
        public string Message01 { set; get; }
        public string Message02 { set; get; }
        public int RoleId { set; get; }
        public List<UserRole> RoleList { set; get; }
        public List<UserGroup> GroupList { set; get; }
        public List<HRMMenus> MenuList { set; get; }

        public RoleFormViewModel()
        {
            //Role = new Role();
        }

        public RoleFormViewModel(bool initialize)
        {
            //if (initialize)
            //{

            //    Role = new UserRole();
            //    MenuList = new List<AuditMenus>();
            //}
        }
    }
    public class UserGroup
    {
        public int GroupId { get; set; }
        public int RoleId { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public string RoleName { get; set; }
    }
    public class UserRole
    {
        public int RoleId { get; set; }
        public int ModuleId { get; set; }
        public string RoleName { get; set; }
        public string ModuleTitle { set; get; }
        public string Description { get; set; }
        public bool IsAssignedRole { set; get; }
        public Int32 UserId { set; get; }
        public Int32 UserGroupId { set; get; }
    }
}
