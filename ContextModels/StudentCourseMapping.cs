using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class StudentCourseMapping
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SemesterWiseCourseId { get; set; }
        public int Status { get; set; }
        public double? Result { get; set; }
        public bool? IsComplete { get; set; }

        public virtual SemesterWiseCourse SemesterWiseCourse { get; set; }
        public virtual StudentInfo Student { get; set; }
    }
}
