using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.ContextModels
{
    public class formViewModel
    {
        public string getdata { get; set; }

        public string Name { get; set; }
        public int DeptId { get; set; }
        public int ConvID { get; set; }
        public int CampID { get; set; }
        public int ProgramID { get; set; }
        public List<Department> deptLst = new List<Department>();

        public List<Campus> CampusLst = new List<Campus>();
        public List<Convocation> convLst = new List<Convocation>();
        public List<Program> programLst = new List<Program>();
        

    }

}
