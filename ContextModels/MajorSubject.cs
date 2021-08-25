using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class MajorSubject
    {
        public MajorSubject()
        {
            CertApplications = new HashSet<CertApplication>();
        }

        public int Id { get; set; }
        public string SubName { get; set; }
        public string ProgramId { get; set; }

        public virtual ICollection<CertApplication> CertApplications { get; set; }
    }
}
