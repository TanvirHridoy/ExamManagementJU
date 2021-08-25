using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class ApvStatus
    {
        public ApvStatus()
        {
            CertApplicationApprovedByExamNavigations = new HashSet<CertApplication>();
            CertApplicationApvStatusAcadNavigations = new HashSet<CertApplication>();
            CertApplicationApvStatusAccNavigations = new HashSet<CertApplication>();
            CertApplicationApvStatusDeptNavigations = new HashSet<CertApplication>();
            CertApplicationApvStatusLibNavigations = new HashSet<CertApplication>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CertApplication> CertApplicationApprovedByExamNavigations { get; set; }
        public virtual ICollection<CertApplication> CertApplicationApvStatusAcadNavigations { get; set; }
        public virtual ICollection<CertApplication> CertApplicationApvStatusAccNavigations { get; set; }
        public virtual ICollection<CertApplication> CertApplicationApvStatusDeptNavigations { get; set; }
        public virtual ICollection<CertApplication> CertApplicationApvStatusLibNavigations { get; set; }
    }
}
