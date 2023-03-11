using CertificationMS.ContextModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Models
{
   
    public class DepartmentViewModel
    {
        public IEnumerable<Department> deptlist { get; set; }
        public string message { get; set; }
    }
    public class ProgramViewModel
    {
        public IEnumerable<CertificationMS.ContextModels.Program> list { get; set; }
        public string message { get; set; }
    }
    public class CampusViewModel
    {
        public IEnumerable<Campus> list { get; set; }
        public string message { get; set; }
    }
    public class StudentViewModel
    {
        public IEnumerable<StudentInfo> list { get; set; }
        public string message { get; set; }
    }
    public class ApvStatusViewModel
    {
        public IEnumerable<ApvStatus> list { get; set; }
        public string message { get; set; }
    }
    public class ConvocationViewModel
    {
        public IEnumerable<Convocation> list { get; set; }
        public string message { get; set; }
    }
    public class StudentTypeViewModel
    {
        public IEnumerable<StudentType> list { get; set; }
        public string message { get; set; }
    }
    
    public class DeptSectionViewModel
    {
        public string message { get; set; }
        public string Department { get; set; } = "";
        public List<DeptSectionListModels> Applications { get; set; }

        public List<Department> departments = new List<Department>();
       public List<StudentType> studentTypes = new List<StudentType>();
       public List<ContextModels.Program> programs = new List<ContextModels.Program>();
    }
    public class AccSectionViewModel
    {
        public string message { get; set; }
        public List<DeptSectionListModels> Applications { get; set; }

        public List<Department> departments = new List<Department>();
        public List<StudentType> studentTypes = new List<StudentType>();
        public List<ContextModels.Program> programs = new List<ContextModels.Program>();
    }
    public class TeacherViewModel
    {
        public IEnumerable<TblTeacher> list { get; set; }
        public string message { get; set; }
    }
    public class SemisterViewModel
    {
        public IEnumerable<TblSemister> list { get; set; }
        public string message { get; set; }
    }
    public class CourseViewModel
    {
        public IEnumerable<TblCourse> list { get; set; }
        public string message { get; set; }
    }
    public class ExamMasterVM
    {
        public IEnumerable<ExamMaster> list { get; set; }
        public string message { get; set; }
    }

    public class ExamMasterCreateVM
    {
        public ExamMaster Exam { get; set; }
        public List<TblSemister> SemesterList { get; set; }
        public string message { get; set; }
    }
}
