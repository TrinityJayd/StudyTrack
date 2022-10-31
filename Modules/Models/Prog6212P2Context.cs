using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Modules.Models
{
    public partial class Prog6212P2Context : DbContext
    {
        public Prog6212P2Context()
        {
        }

        public Prog6212P2Context(DbContextOptions<Prog6212P2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Module> Modules { get; set; } = null!;
        public virtual DbSet<StudySession> StudySessions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserSemester> UserSemesters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("UserDatabase"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Module>(entity =>
            {
                entity.HasKey(e => e.EntryId)
                    .HasName("PK__Module__F57BD2D7E4FE783F");

                entity.ToTable("Module");

                entity.Property(e => e.EntryId)
                    .ValueGeneratedNever()
                    .HasColumnName("EntryID");

                entity.Property(e => e.ClassHours).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Credits).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ModuleCode)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ModuleName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Modules)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Module__UserID__0E6E26BF");
            });

            modelBuilder.Entity<StudySession>(entity =>
            {
                entity.HasKey(e => e.SessionId)
                    .HasName("PK__Study_Se__C9F4927034A14E32");

                entity.ToTable("Study_Sessions");

                entity.Property(e => e.SessionId)
                    .ValueGeneratedNever()
                    .HasColumnName("SessionID");

                entity.Property(e => e.DateStudied).HasColumnType("date");

                entity.Property(e => e.ModuleCode)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.StudySessions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Study_Ses__UserI__14270015");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.CellNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserSemester>(entity =>
            {
                entity.HasKey(e => e.SemesterId)
                    .HasName("PK__User_Sem__043301BDAB56DEFB");

                entity.ToTable("User_Semester");

                entity.Property(e => e.SemesterId)
                    .ValueGeneratedNever()
                    .HasColumnName("SemesterID");

                entity.Property(e => e.SemesterStartDate).HasColumnType("date");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.WeeksInSemester).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSemesters)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__User_Seme__UserI__114A936A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
