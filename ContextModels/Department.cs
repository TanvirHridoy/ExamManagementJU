using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class Department
    {
        public Department()
        {
            CertApplications = new HashSet<CertApplication>();
        }

        public int Id { get; set; }
        public string DeptName { get; set; }
        public string DeptSname { get; set; }

        public virtual ICollection<CertApplication> CertApplications { get; set; }
    }
}
