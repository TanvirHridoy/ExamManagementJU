using System;
using System.Collections.Generic;

#nullable disable

namespace EMSJu.ContextModels
{
    public partial class VwExamSchedule
    {
        public int Scmid { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public int ExamMasterId { get; set; }
        public int ExamDetailsId { get; set; }
        public DateTime? ExamDate { get; set; }
        public decimal Duration { get; set; }
        public bool? Status { get; set; }
        public string TeacherName { get; set; }
        public string TeacherShortCode { get; set; }
    }
}
