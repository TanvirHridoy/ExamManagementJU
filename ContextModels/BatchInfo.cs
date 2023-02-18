using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class BatchInfo
    {
        public BatchInfo()
        {
            StudentInfos = new HashSet<StudentInfo>();
        }

        public int Id { get; set; }
        public string BatchNo { get; set; }

        public virtual ICollection<StudentInfo> StudentInfos { get; set; }
    }
}
