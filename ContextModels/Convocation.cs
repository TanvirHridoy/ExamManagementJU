﻿using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class Convocation
    {
        public Convocation()
        {
            CertApplications = new HashSet<CertApplication>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public DateTime? ProgramDate { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<CertApplication> CertApplications { get; set; }
    }
}
