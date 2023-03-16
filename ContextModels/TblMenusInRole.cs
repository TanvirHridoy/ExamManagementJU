using System;
using System.Collections.Generic;

#nullable disable

namespace EMSJu.ContextModels
{
    public partial class TblMenusInRole
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool Opadd { get; set; }
        public bool Opedit { get; set; }
        public bool Opdelete { get; set; }
        public bool Opcancel { get; set; }
        public bool Opprint { get; set; }
        public int MenuRoleId { get; set; }

        public virtual TblMenu Menu { get; set; }
        public virtual TblRole Role { get; set; }
    }
}
