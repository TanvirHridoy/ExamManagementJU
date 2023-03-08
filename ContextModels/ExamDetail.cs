using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class ExamDetail
    {
        public int Id { get; set; }
        public int ExamMasterId { get; set; }
        public int SemesterWiseCourseId { get; set; }
        public DateTime? ExamDate { get; set; }
        public bool? Status { get; set; }
        public decimal Duration { get; set; }

        public virtual ExamMaster ExamMaster { get; set; }
        public virtual SemesterWiseCourse SemesterWiseCourse { get; set; }
    }
}
