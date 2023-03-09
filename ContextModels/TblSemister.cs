using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class TblSemister
    {
        public TblSemister()
        {
            SemesterWiseCourses = new HashSet<SemesterWiseCourse>();
        }

        public int SemisterId { get; set; }
        public string SemisterName { get; set; }
        public bool? IsDone { get; set; }

        public virtual ExamMaster ExamMaster { get; set; }
        public virtual ICollection<SemesterWiseCourse> SemesterWiseCourses { get; set; }
    }
}
