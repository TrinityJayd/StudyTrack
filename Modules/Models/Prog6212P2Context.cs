using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Protocols;

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
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:st10083735.database.windows.net,1433;Initial Catalog=Prog6212P2;Persist Security Info=False;User ID=admn;Password=Trinday2*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Module>(entity =>
            {
                entity.HasKey(e => e.EntryId)
                    .HasName("PK__Module__F57BD2D766C641E3");

                entity.ToTable("Module");

                entity.Property(e => e.EntryId).HasColumnName("EntryID");

                entity.Property(e => e.DateLastStudied).HasColumnType("date");

                entity.Property(e => e.ModuleCode)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ModuleName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SemesterStartDate).HasColumnType("date");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Modules)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Module__UserID__6383C8BA");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

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

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
