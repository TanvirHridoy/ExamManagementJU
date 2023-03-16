using System;
using System.Collections.Generic;

#nullable disable

namespace EMSJu.ContextModels
{
    public partial class TblSemister
    {
        public TblSemister()
        {
            ExamMasters = new HashSet<ExamMaster>();
            SemesterWiseCourses = new HashSet<SemesterWiseCourse>();
        }

        public int SemisterId { get; set; }
        public string SemisterName { get; set; }
        public bool? IsDone { get; set; }

        public virtual ICollection<ExamMaster> ExamMasters { get; set; }
        public virtual ICollection<SemesterWiseCourse> SemesterWiseCourses { get; set; }
    }
}
