using System;
using System.Collections.Generic;

#nullable disable

namespace EMSJu.ContextModels
{
    public partial class TblGroup
    {
        public TblGroup()
        {
            TblGroupInRoles = new HashSet<TblGroupInRole>();
            TblUsers = new HashSet<TblUser>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TblGroupInRole> TblGroupInRoles { get; set; }
        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}
