using System;
using System.Collections.Generic;

#nullable disable

namespace EMSJu.ContextModels
{
    public partial class SemesterWiseCourse
    {
        public SemesterWiseCourse()
        {
            ExamDetails = new HashSet<ExamDetail>();
            StudentCourseMappings = new HashSet<StudentCourseMapping>();
        }

        public int Id { get; set; }
        public int SemesterId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public bool? Status { get; set; }
        public int Capacity { get; set; }

        public virtual TblCourse Course { get; set; }
        public virtual TblSemister Semester { get; set; }
        public virtual TblTeacher Teacher { get; set; }
        public virtual ICollection<ExamDetail> ExamDetails { get; set; }
        public virtual ICollection<StudentCourseMapping> StudentCourseMappings { get; set; }
    }
}
