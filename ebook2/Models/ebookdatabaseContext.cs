using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ebook2.Models
{
    public partial class ebookdatabaseContext : DbContext
    {
        public ebookdatabaseContext()
        {
        }

        public ebookdatabaseContext(DbContextOptions<ebookdatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Issurance> Issurance { get; set; }
        public virtual DbSet<Redemption> Redemption { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=ebookdatabase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issurance>(entity =>
            {
                entity.Property(e => e.IssuranceId).HasColumnName("IssuranceID");

                entity.Property(e => e.RedemptionId).HasColumnName("RedemptionID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.HasOne(d => d.Redemption)
                    .WithMany(p => p.Issurance)
                    .HasForeignKey(d => d.RedemptionId)
                    .HasConstraintName("FK_Issurance_Redemption");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Issurance)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Issurance_Student");
            });

            modelBuilder.Entity<Redemption>(entity =>
            {
                entity.Property(e => e.RedemptionId).HasColumnName("RedemptionID");

                entity.Property(e => e.BookTitle)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
