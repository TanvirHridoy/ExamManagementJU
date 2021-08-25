using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class StudentType
    {
        public StudentType()
        {
            CertApplications = new HashSet<CertApplication>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<CertApplication> CertApplications { get; set; }
    }
}
