using System;
using System.Collections.Generic;

#nullable disable

namespace EMSJu.ContextModels
{
    public partial class TblModule
    {
        public TblModule()
        {
            TblRoles = new HashSet<TblRole>();
        }

        public int ModuleId { get; set; }
        public string ModuleTitle { get; set; }
        public string Description { get; set; }
        public string ModuleName { get; set; }
        public int? SortOrder { get; set; }

        public virtual ICollection<TblRole> TblRoles { get; set; }
    }
}
