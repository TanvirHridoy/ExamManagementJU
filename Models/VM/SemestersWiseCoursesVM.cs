using CertificationMS.ContextModels;
using System.Collections.Generic;

namespace CertificationMS.Models.VM
{
    public class SemestersWiseCoursesVM
    {
        public int Id { get; set; }
        public int SemesterId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public bool? Status { get; set; }
        public string message { get; set; }
        public List<TblCourse> coursesVMs { get; set; } 
        public List<TblSemister> semesterVM { get; set; } 
        public List<TblTeacher> teacherVM { get; set; } 
        public List<MappVm> MappVm { get; set; } 
    }
    public class MappVm
    {
        public int SemesterId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
    }
    public class CoursesVM
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int? IsActive { get; set; }
        public decimal? Credit { get; set; }
    }
    public class SemesterVM
    {
        public int SemisterId { get; set; }
        public string SemisterName { get; set; }
    }
    
    public class TeacherVM
    {
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
    }



}
