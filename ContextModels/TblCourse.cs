using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class TblCourse
    {
        public TblCourse()
        {
            SemesterWiseCourses = new HashSet<SemesterWiseCourse>();
        }

        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int? IsActive { get; set; }
        public decimal? Credit { get; set; }

        public virtual ICollection<SemesterWiseCourse> SemesterWiseCourses { get; set; }
    }
}
