using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class CertificateMSContext : DbContext
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
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ApvStatus>(entity =>
            {
                entity.ToTable("ApvStatus");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Campus>(entity =>
            {
                entity.ToTable("Campus");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.CampusName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasDefaultValueSql("(' ')");
            });

            modelBuilder.Entity<CertApplication>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.ApplyDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ApprovedByAcad)
                    .HasColumnName("ApprovedByACAD")
                    .HasComment("User Id who approved In ACAD section");

                entity.Property(e => e.ApprovedByAcc).HasComment("User Id who approved In Accounts section");

                entity.Property(e => e.ApprovedByDept).HasComment("User Id who approved In Department section");

                entity.Property(e => e.ApprovedByLib).HasComment("User Id who approved In Library section");

                entity.Property(e => e.ApvAcaddate)
                    .HasColumnType("datetime")
                    .HasColumnName("ApvACADDate");

                entity.Property(e => e.ApvAccDate).HasColumnType("datetime");

                entity.Property(e => e.ApvDeptDate).HasColumnType("datetime");

                entity.Property(e => e.ApvExamDate).HasColumnType("datetime");

                entity.Property(e => e.ApvLibDate).HasColumnType("datetime");

                entity.Property(e => e.ApvStatusAcad)
                    .HasColumnName("ApvStatusACAD")
                    .HasComment("Approval StatusID For ACADsection");

                entity.Property(e => e.ApvStatusAcc).HasComment("Approval StatusID For Accounts section");

                entity.Property(e => e.ApvStatusDept).HasComment("Approval Status ID For Dept");

                entity.Property(e => e.ApvStatusLib).HasComment("Approval StatusID For library section");

                entity.Property(e => e.ConvocationId).HasColumnName("ConvocationID");

                entity.Property(e => e.ExtraThree).HasDefaultValueSql("(' ')");

                entity.Property(e => e.ExtraTwo).HasDefaultValueSql("(' ')");

                entity.Property(e => e.IsDelivered).HasDefaultValueSql("((0))");

                entity.Property(e => e.PhoneNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProgramId).HasColumnName("ProgramID");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("StudentID");

                entity.Property(e => e.StudentName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TrackId)
                    .IsRequired()
                    .HasColumnName("TrackID");
            });

            modelBuilder.Entity<Convocation>(entity =>
            {
                entity.ToTable("Convocation");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProgramDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Year)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DeptName).HasMaxLength(50);

                entity.Property(e => e.DeptSname)
                    .HasMaxLength(20)
                    .HasColumnName("DeptSName");
            });

            modelBuilder.Entity<Program>(entity =>
            {
                entity.ToTable("Program");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProgramName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StudentType>(entity =>
            {
                entity.ToTable("StudentType");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
