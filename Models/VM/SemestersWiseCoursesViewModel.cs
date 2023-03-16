using EMSJu.ContextModels;
using System;
using System.Collections.Generic;

namespace EMSJu.Models.VM
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
        public DateTime? ExamDate { get; set; }
        public List<SemesterWiseCourseView>  datalist { get; set; }
        public ExamDetail Exam { get; set; }
        public StudentInfo Student { get; set; }
        public TblCourse Course { get; set; }
        public TblTeacher Teacher { get; set; }
    }
    public class CourseView
    {
        public int SemesterWiseCourseId { get; set; }
        public bool? IsCheck { get; set; }=false;
    }
}
