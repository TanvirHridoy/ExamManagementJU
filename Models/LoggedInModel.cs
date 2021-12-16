using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Models
{
    public class LoggedInModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string EmpId { get; set; }
        public EmpInfo empInfo { get; set; }
        public List<EmpMenus> EmpMenuList { get; set; }
        public string Message { get; set; }
    }
    public class EmpInfo
    {
        public string Name { get; set; }
        public string Section { get; set; }
        public string SDesig { get; set; }
        public byte[] Photo { get; set; }
    }
    public class EmpMenus
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; } = null!;
        public string MenuCaption { get; set; } = null!;
        public string MenuIconClass { get; set; }
        public string? MenuCaptionBng { get; set; }
        public int? ParentMenuId { get; set; }
        public int? SerialNo { get; set; }
        public string? PageUrl { get; set; }
        public int? OrderNo { get; set; }
        public bool OPAdd { get; set; }
        public bool OPEdit { get; set; }
        public bool OPDelete { get; set; }
        public bool OPCancel { get; set; }
        public bool OPPrint { get; set; }
    }
}
