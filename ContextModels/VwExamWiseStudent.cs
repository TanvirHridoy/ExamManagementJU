using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class VwExamWiseStudent
    {
        public int ExamMasterId { get; set; }
        public string ExamName { get; set; }
        public decimal Duration { get; set; }
        public DateTime? ExamDate { get; set; }
        public int ExamDetailId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public decimal? Credit { get; set; }
        public string SemisterName { get; set; }
        public int SemisterId { get; set; }
        public int SemesterWiseCourseId { get; set; }
        public string StudentName { get; set; }
        public string StudentId { get; set; }
        public string FileName { get; set; }
        public byte[] Photo { get; set; }
        public int StdId { get; set; }
        public int BatchId { get; set; }
        public string BatchNo { get; set; }
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
    }
}
