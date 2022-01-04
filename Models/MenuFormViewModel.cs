using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Models
{
    public class MenuFormViewModel
    {
        //public CustomMessage CustomMessage { set; get; }
        public HRMMenus HRMMenu { set; get; }
        public List<ParentMenu> ParentMenuList { set; get; }
        public List<HRMMenus> HRMMenuList { set; get; }

        public MenuFormViewModel()
        {
            this.GetParentMenus();
        }


        public void GetParentMenus()
        {
            this.ParentMenuList = null;// AuditDataAccess<ParentMenu>.AuditGetDataList01(ProcessID: "AUDITMENUS_INFO01", _param01: "PARENTMENU", _param02: "", _param03: "", _param04: "", _param05: "");
        }
        public class ParentMenu
        {
            public int MenuId { get; set; }
            public string MenuName { get; set; }
            public string MenuCaption { get; set; }
            public string MenuCaptionBng { get; set; }
            public int SerialNo { get; set; }
            public string MenuIcon { get; set; }
            public string PageUrl { get; set; }
        }

        public void PopulateMenuList(List<HRMMenus> list)
        {
            this.HRMMenuList = list;//AuditDataAccess<AuditMenus>.AuditGetDataList01(ProcessID: "AUDITMENUS_INFO01", _param01: "ALL", _param02: "", _param03: "", _param04: "", _param05: "");

        }
    }

    public class HRMMenus
    {
        public int MenuId { get; set; }
        public int ApplicationId { get; set; }
        public string MenuName { get; set; }
        public string MenuCaption { get; set; }
        public string MenuCaptionBng { get; set; }
        public int ParentMenuId { get; set; }
        public string ParentMenuName { set; get; }
        public string MenuIcon { get; set; }
        public int SerialNo { get; set; }
        public string PageUrl { get; set; }
        public int ModuleId { get; set; }
        public int RoleId { get; set; }
        public string ModuleName { set; get; }
        public int OrderNo { get; set; }
        public bool MenuStatus { get; set; }

        public bool IsAssignedMenu { set; get; }
        public bool IsAddAssigned { set; get; }
        public bool IsEditAssigned { set; get; }
        public bool IsCancelAssigned { set; get; }
        public bool IsPrintAssigned { set; get; }
        public bool IsDeleteAssigned { set; get; }

        private bool viewAssigned;

        public bool IsViewAssigned
        {
            get
            {
                if (!IsAddAssigned)
                {
                    if (!IsEditAssigned)
                    {
                        if (!IsDeleteAssigned)
                        {
                            if (!IsCancelAssigned)
                            {
                                if (!IsPrintAssigned)
                                {
                                    viewAssigned = true;
                                }
                            }
                        }
                    }
                }
                return viewAssigned;
            }
            set
            {
                viewAssigned = value;
            }
        }
    }
}
