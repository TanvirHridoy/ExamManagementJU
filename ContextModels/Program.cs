using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class Program
    {
        public Program()
        {
            CertApplications = new HashSet<CertApplication>();
        }

        public int Id { get; set; }
        public string ProgramName { get; set; }

        public virtual ICollection<CertApplication> CertApplications { get; set; }
    }
}
