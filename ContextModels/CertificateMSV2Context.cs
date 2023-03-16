using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EMSJu.ContextModels
{
    public partial class CertificateMSV2Context : DbContext
    {
        public CertificateMSV2Context()
        {
        }

        public CertificateMSV2Context(DbContextOptions<CertificateMSV2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<ApvStatus> ApvStatuses { get; set; }
        public virtual DbSet<BatchInfo> BatchInfos { get; set; }
        public virtual DbSet<Campus> Campuses { get; set; }
        public virtual DbSet<CertApplication> CertApplications { get; set; }
        public virtual DbSet<Convocation> Convocations { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<ExamDetail> ExamDetails { get; set; }
        public virtual DbSet<ExamDuty> ExamDuties { get; set; }
        public virtual DbSet<ExamMaster> ExamMasters { get; set; }
        public virtual DbSet<PrmDesignation> PrmDesignations { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<SemesterWiseCourse> SemesterWiseCourses { get; set; }
        public virtual DbSet<StudentCourseMapping> StudentCourseMappings { get; set; }
        public virtual DbSet<StudentInfo> StudentInfos { get; set; }
        public virtual DbSet<StudentType> StudentTypes { get; set; }
        public virtual DbSet<TblCourse> TblCourses { get; set; }
        public virtual DbSet<TblGroup> TblGroups { get; set; }
        public virtual DbSet<TblGroupInRole> TblGroupInRoles { get; set; }
        public virtual DbSet<TblMenu> TblMenus { get; set; }
        public virtual DbSet<TblMenusInRole> TblMenusInRoles { get; set; }
        public virtual DbSet<TblModule> TblModules { get; set; }
        public virtual DbSet<TblRole> TblRoles { get; set; }
        public virtual DbSet<TblSemister> TblSemisters { get; set; }
        public virtual DbSet<TblTeacher> TblTeachers { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }
        public virtual DbSet<VwAttendance> VwAttendances { get; set; }
        public virtual DbSet<VwExamWiseStudent> VwExamWiseStudents { get; set; }
        public virtual DbSet<VwTeacherExamDuty> VwTeacherExamDuties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=103.125.254.20,9433;Database=juexamdb;User ID=gymuser;Password=sa@1234;MultipleActiveResultSets=true ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("gymuser")
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ApvStatus>(entity =>
            {
                entity.ToTable("ApvStatuses", "dbo");
            });

            modelBuilder.Entity<BatchInfo>(entity =>
            {
                entity.ToTable("BatchInfo", "dbo");

                entity.Property(e => e.BatchNo).HasMaxLength(50);
            });

            modelBuilder.Entity<Campus>(entity =>
            {
                entity.ToTable("Campuses", "dbo");
            });

            modelBuilder.Entity<CertApplication>(entity =>
            {
                entity.ToTable("CertApplications", "dbo");

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.FeeForCertificate)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Hsc).HasColumnName("HSC");

                entity.Property(e => e.IsDelivered).HasDefaultValueSql("((0))");

                entity.Property(e => e.Ssc).HasColumnName("SSC");

                entity.Property(e => e.TotalPaid).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalPayable)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Convocation>(entity =>
            {
                entity.ToTable("Convocations", "dbo");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Departments", "dbo");
            });

            modelBuilder.Entity<ExamDetail>(entity =>
            {
                entity.ToTable("ExamDetails", "dbo");

                entity.Property(e => e.Duration)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((3))");

                entity.Property(e => e.ExamDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.ExamMaster)
                    .WithMany(p => p.ExamDetails)
                    .HasForeignKey(d => d.ExamMasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamDetails_ExamMaster");

                entity.HasOne(d => d.SemesterWiseCourse)
                    .WithMany(p => p.ExamDetails)
                    .HasForeignKey(d => d.SemesterWiseCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamDetails_SemesterWiseCourse");
            });

            modelBuilder.Entity<ExamDuty>(entity =>
            {
                entity.ToTable("ExamDuty", "dbo");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.ExamDuties)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamDuty_TblTeacher");
            });

            modelBuilder.Entity<ExamMaster>(entity =>
            {
                entity.ToTable("ExamMaster", "dbo");

                entity.Property(e => e.ExamName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.ExamMasters)
                    .HasForeignKey(d => d.SemesterId)
                    .HasConstraintName("FK_ExamMaster_TblSemister");
            });

            modelBuilder.Entity<PrmDesignation>(entity =>
            {
                entity.ToTable("PRM_Designation", "dbo");

                entity.Property(e => e.JobDescription).IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameB).HasMaxLength(100);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ShortName).HasMaxLength(50);
            });

            modelBuilder.Entity<Program>(entity =>
            {
                entity.ToTable("Programs", "dbo");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("Sections", "dbo");

                entity.Property(e => e.SectionName)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("(' ')");
            });

            modelBuilder.Entity<SemesterWiseCourse>(entity =>
            {
                entity.ToTable("SemesterWiseCourse", "dbo");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.SemesterWiseCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SemesterWiseCourse_TblCourse");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.SemesterWiseCourses)
                    .HasForeignKey(d => d.SemesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SemesterWiseCourse_TblSemister");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.SemesterWiseCourses)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SemesterWiseCourse_TblTeacher");
            });

            modelBuilder.Entity<StudentCourseMapping>(entity =>
            {
                entity.ToTable("StudentCourseMapping", "dbo");

                entity.Property(e => e.IsComplete).HasDefaultValueSql("((0))");

                entity.Property(e => e.ResultPublishedOn).HasColumnType("datetime");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.VerificationMethod).HasMaxLength(50);

                entity.Property(e => e.VerifyDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("verifyDateTime");

                entity.HasOne(d => d.SemesterWiseCourse)
                    .WithMany(p => p.StudentCourseMappings)
                    .HasForeignKey(d => d.SemesterWiseCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCourseMapping_SemesterWiseCourse1");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourseMappings)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCourseMapping_StudentInfo1");
            });

            modelBuilder.Entity<StudentInfo>(entity =>
            {
                entity.ToTable("StudentInfo", "dbo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FileName).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("StudentID");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.StudentInfos)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("FK_StudentInfo_BatchInfo");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.StudentInfos)
                    .HasForeignKey(d => d.ProgramId)
                    .HasConstraintName("FK_StudentInfo_Programs");
            });

            modelBuilder.Entity<StudentType>(entity =>
            {
                entity.ToTable("StudentTypes", "dbo");
            });

            modelBuilder.Entity<TblCourse>(entity =>
            {
                entity.HasKey(e => e.CourseId);

                entity.ToTable("TblCourse", "dbo");

                entity.Property(e => e.CourseCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CourseName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Credit).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<TblGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.ToTable("TblGroups", "dbo");

                entity.HasIndex(e => e.GroupName, "IX_TblGroups")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblGroupInRole>(entity =>
            {
                entity.HasKey(e => e.GroupRoleId);

                entity.ToTable("TblGroupInRoles", "dbo");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.TblGroupInRoles)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblGroupInRoles_TblGroups");
            });

            modelBuilder.Entity<TblMenu>(entity =>
            {
                entity.HasKey(e => e.MenuId)
                    .HasName("PK_TblMenus_1");

                entity.ToTable("TblMenus", "dbo");

                entity.Property(e => e.MenuCaption)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MenuCaptionBng)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MenuIconClass)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("(N'far fa-fw fa-window-maximize')");

                entity.Property(e => e.MenuName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PageUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblMenusInRole>(entity =>
            {
                entity.HasKey(e => e.MenuRoleId);

                entity.ToTable("TblMenusInRoles", "dbo");

                entity.Property(e => e.Opadd).HasColumnName("OPAdd");

                entity.Property(e => e.Opcancel).HasColumnName("OPCancel");

                entity.Property(e => e.Opdelete).HasColumnName("OPDelete");

                entity.Property(e => e.Opedit).HasColumnName("OPEdit");

                entity.Property(e => e.Opprint).HasColumnName("OPPrint");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.TblMenusInRoles)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblMenusInRoles_TblMenus");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblMenusInRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblMenusInRoles_TblRoles");
            });

            modelBuilder.Entity<TblModule>(entity =>
            {
                entity.HasKey(e => e.ModuleId);

                entity.ToTable("TblModules", "dbo");

                entity.HasIndex(e => e.ModuleName, "IX_TblModules")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ModuleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModuleTitle)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("TblRoles", "dbo");

                entity.HasIndex(e => e.RoleName, "IX_TblRoles")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.TblRoles)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblRoles_TblModules");
            });

            modelBuilder.Entity<TblSemister>(entity =>
            {
                entity.HasKey(e => e.SemisterId);

                entity.ToTable("TblSemister", "dbo");

                entity.Property(e => e.IsDone).HasDefaultValueSql("((0))");

                entity.Property(e => e.SemisterName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTeacher>(entity =>
            {
                entity.HasKey(e => e.TeacherId);

                entity.ToTable("TblTeacher", "dbo");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Age)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Degree)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Designation)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ShortCode).HasMaxLength(50);
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_TblUserInfo");

                entity.ToTable("TblUsers", "dbo");

                entity.Property(e => e.Comment)
                    .HasMaxLength(3000)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Designation).HasDefaultValueSql("('')");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastLockoutDate).HasColumnType("datetime");

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.LastPasswordChangedDate).HasColumnType("datetime");

                entity.Property(e => e.LoginId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SectionId).HasColumnName("SectionID");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.TblUsers)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_TblUsers_TblGroups");
            });

            modelBuilder.Entity<VwAttendance>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwAttendance", "dbo");

                entity.Property(e => e.CourseCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CourseName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SemisterName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShortCode).HasMaxLength(50);

                entity.Property(e => e.TeacherName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwExamWiseStudent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwExamWiseStudents", "dbo");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.CourseCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CourseName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Credit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Duration).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ExamDate).HasColumnType("datetime");

                entity.Property(e => e.ExamName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FileName).HasMaxLength(250);

                entity.Property(e => e.Scmid).HasColumnName("SCMId");

                entity.Property(e => e.SemisterName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("StudentID");

                entity.Property(e => e.StudentName).HasMaxLength(250);

                entity.Property(e => e.VerificationMethod)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.VerifiedBy)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VerifyDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("verifyDateTime");
            });

            modelBuilder.Entity<VwTeacherExamDuty>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwTeacherExamDuty", "dbo");

                entity.Property(e => e.CourseCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CourseName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Credit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Duration).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ExamDate).HasColumnType("datetime");

                entity.Property(e => e.ExamName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.SemisterName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherShortCode)
                    .HasMaxLength(50)
                    .HasColumnName("teacherShortCode");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
