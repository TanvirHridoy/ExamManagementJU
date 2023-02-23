using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class TblTeacher
    {
        public TblTeacher()
        {
            SemesterWiseCourses = new HashSet<SemesterWiseCourse>();
        }

        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string ShortCode { get; set; }
        public string Designation { get; set; }
        public string Degree { get; set; }
        public string Address { get; set; }
        public bool? IsActive { get; set; }
        public string Age { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public int? GenderId { get; set; }

        public virtual ICollection<SemesterWiseCourse> SemesterWiseCourses { get; set; }
    }
}
