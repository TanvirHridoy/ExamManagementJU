using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class Campus
    {
        public int Id { get; set; }
        public string CampusName { get; set; }
        public string Address { get; set; }
        public int? SortOrder { get; set; }
    }
}
