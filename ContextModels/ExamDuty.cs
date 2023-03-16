using System;
using System.Collections.Generic;

#nullable disable

namespace EMSJu.ContextModels
{
    public partial class ExamDuty
    {
        public int Id { get; set; }
        public int ExamDetailsId { get; set; }
        public int TeacherId { get; set; }

        public virtual TblTeacher Teacher { get; set; }
    }
}
