using EMSJu.ContextModels;
using System.Collections.Generic;

namespace EMSJu.Models
{
    public class ApplicationDetailsViewModel
    {
        public CertApplication Application { get; set; }
        public string message { get; set; }

        public List<ApvStatus> ApvStatusLst = new List<ApvStatus>();
        public List<Department> departments = new List<Department>();
        public List<StudentType> studentTypes = new List<StudentType>();
        public List<Campus> Campuses = new List<Campus>();
        public List<Convocation> Convocations = new List<Convocation>();
        public List<ContextModels.Program> programs = new List<ContextModels.Program>();
    }
}
