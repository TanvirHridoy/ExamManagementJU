using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class TblGroupInRole
    {
        public int GroupRoleId { get; set; }
        public int GroupId { get; set; }
        public int RoleId { get; set; }

        public virtual TblGroup Group { get; set; }
    }
}
