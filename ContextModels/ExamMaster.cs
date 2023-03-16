using System;
using System.Collections.Generic;

#nullable disable

namespace EMSJu.ContextModels
{
    public partial class ExamMaster
    {
        public ExamMaster()
        {
            ExamDetails = new HashSet<ExamDetail>();
        }

        public int Id { get; set; }
        public string ExamName { get; set; }
        public int? SemesterId { get; set; }
        public bool IsComplete { get; set; }

        public virtual TblSemister Semester { get; set; }
        public virtual ICollection<ExamDetail> ExamDetails { get; set; }
    }
}
