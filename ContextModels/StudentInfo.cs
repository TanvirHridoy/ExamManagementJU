using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class StudentInfo
    {
        public StudentInfo()
        {
            StudentCourseMappings = new HashSet<StudentCourseMapping>();
        }

        public int Id { get; set; }
        public string StudentId { get; set; }
        public byte[] Photo { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public int? ProgramId { get; set; }
        public int? BatchId { get; set; }

        public virtual BatchInfo Batch { get; set; }
        public virtual Program Program { get; set; }
        public virtual ICollection<StudentCourseMapping> StudentCourseMappings { get; set; }
    }
}
