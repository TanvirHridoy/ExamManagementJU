using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Models.VM
{
    public class ExamAttendanceVM
    {
        public int SCMId { get; set; }
        public int Status { get; set; }
        public int VerifyBy { get; set; }
        public DateTime VerifyDate { get; set; }
        public int VerifyMethod { get; set; }
    }
}
