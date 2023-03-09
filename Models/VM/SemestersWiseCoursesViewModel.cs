using CertificationMS.ContextModels;
using System.Collections.Generic;

namespace CertificationMS.Models.VM
{
    public class SemestersWiseCoursesViewModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SemesterWiseCourseId { get; set; }       
        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        public string TeacherName { get; set; }
        public string CoursesName { get; set; }
        public List<CourseView> MappVm { set; get; }
        public List<StudentInfo> StudentInfoVm { get; set; }
        public List<TblSemister> semesterVM { get; set; }
        public List<SemesterWiseCourse> SemesterWiseCourseVM { get; set; }
    }
    public class SemesterWiseCourseView
    {
        public int SemesterWiseCourseId { get; set; }
        public string SemesterName { get; set; }
        public string TeacherName { get; set; }
        public string CoursesName { get; set; }
        public bool? IsCheck { get; set; } = false;
    }
    public class CourseView
    {
        public int SemesterWiseCourseId { get; set; }
        public bool? IsCheck { get; set; }=false;
    }
}
