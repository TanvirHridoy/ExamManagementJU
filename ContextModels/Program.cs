using System;
using System.Collections.Generic;

#nullable disable

namespace EMSJu.ContextModels
{
    public partial class Program
    {
        public Program()
        {
            StudentInfos = new HashSet<StudentInfo>();
        }

        public int Id { get; set; }
        public string ProgramName { get; set; }

        public virtual ICollection<StudentInfo> StudentInfos { get; set; }
    }
}
