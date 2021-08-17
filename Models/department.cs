using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Models
{
    public class department
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage ="required")]
        public string deptname { get; set; }
        [Required(ErrorMessage = "required")]
        public string deptsname{ get; set; }
    }
    public class DepartmentViewModel
    {
        public IEnumerable<department> deptlist { get; set; }
        public string message { get; set; }
    }
   
}
