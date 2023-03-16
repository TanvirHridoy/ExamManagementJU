using System;
using System.Collections.Generic;

#nullable disable

namespace EMSJu.ContextModels
{
    public partial class VwAttendance
    {
        public int Id { get; set; }
        public int SemisterId { get; set; }
        public string SemisterName { get; set; }
        public string CourseName { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string TeacherName { get; set; }
        public string ShortCode { get; set; }
        public int SemesterWiseCourseId { get; set; }
    }
}
