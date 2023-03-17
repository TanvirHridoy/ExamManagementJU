using System;
using System.Collections.Generic;

#nullable disable

namespace EMSJu.ContextModels
{
    public partial class VwTeacherExamDuty
    {
        public int DutyId { get; set; }
        public int ExamMasterId { get; set; }
        public string ExamName { get; set; }
        public DateTime? ExamDate { get; set; }
        public int ExamDetailId { get; set; }
        public decimal Duration { get; set; }
        public int ExamDutyId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public decimal? Credit { get; set; }
        public string SemisterName { get; set; }
        public int SemisterId { get; set; }
        public int SemesterWiseCourseId { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherShortCode { get; set; }
    }
}
