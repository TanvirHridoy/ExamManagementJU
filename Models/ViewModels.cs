﻿using CertificationMS.ContextModels;
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
}