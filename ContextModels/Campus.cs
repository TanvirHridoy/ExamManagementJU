using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class Campus
    {
        public Campus()
        {
            CertApplicationFromNubCampusNavigations = new HashSet<CertApplication>();
            CertApplicationToNubCampusNavigations = new HashSet<CertApplication>();
        }

        public int Id { get; set; }
        public string CampusName { get; set; }
        public string Address { get; set; }
        public int? SortOrder { get; set; }

        public virtual ICollection<CertApplication> CertApplicationFromNubCampusNavigations { get; set; }
        public virtual ICollection<CertApplication> CertApplicationToNubCampusNavigations { get; set; }
    }
}
