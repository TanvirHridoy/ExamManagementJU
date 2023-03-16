using EMSJu.ContextModels;
using System.Collections.Generic;

namespace EMSJu.Models.VM
{
    public class SemesterTeacherCoursesListVM
    {
        public int Id { get; set; }
        public int SemesterId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public string SemesterName { get; set; }
        public string TeacherName { get; set; }
        public string CourseName { get; set; }
        public string message { get; set; }
        public List<TblCourse> coursesVMs { get; set; }
        public List<TblSemister> semesterVM { get; set; }
        public List<TblTeacher> teacherVM { get; set; }
        public List<SemesterWiseCourse> cc { get; set; }
        public List<SemesterTeacherCoursesListVM> datalist { get; set; }
    }
}
