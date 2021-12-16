using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class TblRole
    {
        public TblRole()
        {
            TblMenusInRoles = new HashSet<TblMenusInRole>();
        }

        public string RoleName { get; set; }
        public string Description { get; set; }
        public int RoleId { get; set; }
        public int ModuleId { get; set; }

        public virtual TblModule Module { get; set; }
        public virtual ICollection<TblMenusInRole> TblMenusInRoles { get; set; }
    }
}
