using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class TblMenu
    {
        public TblMenu()
        {
            TblMenusInRoles = new HashSet<TblMenusInRole>();
        }

        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuCaption { get; set; }
        public string MenuCaptionBng { get; set; }
        public int? ParentMenuId { get; set; }
        public int? SerialNo { get; set; }
        public string PageUrl { get; set; }
        public int ModuleId { get; set; }
        public int? OrderNo { get; set; }
        public string MenuIconClass { get; set; }

        public virtual ICollection<TblMenusInRole> TblMenusInRoles { get; set; }
    }
}
