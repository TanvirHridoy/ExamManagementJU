using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class CertificateMSContext : IdentityDbContext
    {
        public CertificateMSContext()
        {
        }

        public CertificateMSContext(DbContextOptions<CertificateMSContext> options)
            : base(options)
        {
        }
        

        public virtual DbSet<ApvStatus> ApvStatuses { get; set; }
        public virtual DbSet<Campus> Campuses { get; set; }
        public virtual DbSet<CertApplication> CertApplications { get; set; }
        public virtual DbSet<Convocation> Convocations { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<StudentType> StudentTypes { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
