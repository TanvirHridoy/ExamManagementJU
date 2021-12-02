using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Models
{
    public class TrackingViewModel
    {
        public string ApplyDate { get; set; }
        public string TrackId { get; set; }
        public string ApvStatusDept { get; set; }
        public string ApvStatusAcc { get; set; }
        public string ApvStatusLib { get; set; }
        public string ApvStatusAcad { get; set; }
        public string ApvStatusExam { get; set; }
        public string ApvDeptDate { get; set; }
        public string ApvAccDate { get; set; }
        public string ApvLibDate { get; set; }
        public string ApvAcadDate { get; set; }
        public string ApvExamDate { get; set; }
        public string DeliveryDate { get; set; }
    }
}
