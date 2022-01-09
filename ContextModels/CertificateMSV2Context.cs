using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CertificationMS.ContextModels
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
        public virtual DbSet<Campus> Campuses { get; set; }
        public virtual DbSet<CertApplication> CertApplications { get; set; }
        public virtual DbSet<Convocation> Convocations { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<PrmDesignation> PrmDesignations { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<StudentType> StudentTypes { get; set; }
        public virtual DbSet<TblGroup> TblGroups { get; set; }
        public virtual DbSet<TblGroupInRole> TblGroupInRoles { get; set; }
        public virtual DbSet<TblMenu> TblMenus { get; set; }
        public virtual DbSet<TblMenusInRole> TblMenusInRoles { get; set; }
        public virtual DbSet<TblModule> TblModules { get; set; }
        public virtual DbSet<TblRole> TblRoles { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }

     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CertApplication>(entity =>
            {
                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.FeeForCertificate)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDelivered).HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalPaid).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalPayable)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<PrmDesignation>(entity =>
            {
                entity.ToTable("PRM_Designation");

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

            modelBuilder.Entity<Section>(entity =>
            {
                entity.Property(e => e.SectionName)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("(' ')");
            });

            modelBuilder.Entity<TblGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId);

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

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_TblUserInfo");

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
