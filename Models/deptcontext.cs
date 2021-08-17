using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Models
{
    public class deptcontext:DbContext
    {
        public deptcontext(DbContextOptions<deptcontext> options) :base(options)
        {

        }
        public DbSet<department> Department { get; set; }


    }
}
