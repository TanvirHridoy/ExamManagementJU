using CertificationMS.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Models
{
    public class DesignationViewModel
    {
        public List<PrmDesignation> List { get; set; }
        public string message { get; set; }
    }
}
