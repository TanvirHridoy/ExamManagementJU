using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSJu.Models
{
    public class Enum
    {
    }

    public enum Gender
    {
        Male=1,
        Female=2,
        Others=3
    }
    public enum SCM_Status
    {
        Registered=1,
        Present=2,
        Dropped=6,
        ResultPublished=3,
        Absent=4
    }

    public enum VerificationMethod
    {
        Face=1,
        Manual=2
    }

}
