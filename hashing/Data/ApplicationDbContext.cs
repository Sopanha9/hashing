using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Default connection string for local development
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=CleanStudentDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        public DbSet<SystemAdmin> SystemAdmins { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // SystemAdmin configuration
            modelBuilder.Entity<SystemAdmin>(entity =>
            {
                entity.HasKey(e => e.AdminID);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            });

            // Student configuration
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentID);
                entity.Property(e => e.StudentName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Gender).HasMaxLength(10);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Course).HasMaxLength(100);
            });

            // Teacher configuration
            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.TeacherID);
                entity.Property(e => e.TeacherName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            // Course configuration
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseID);
                entity.Property(e => e.CourseName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Credits).HasDefaultValue(3);
            });

            // Class configuration
            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.ClassID);
                entity.Property(e => e.Semester).HasMaxLength(20);
                entity.Property(e => e.RoomNumber).HasMaxLength(10);

                entity.HasOne(e => e.Course)
                    .WithMany()
                    .HasForeignKey(e => e.CourseID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Teacher)
                    .WithMany()
                    .HasForeignKey(e => e.TeacherID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Enrollment configuration
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.EnrollmentID);
                entity.Property(e => e.EnrollmentDate).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Grade).HasColumnType("decimal(5,2)");

                entity.HasOne(e => e.Student)
                    .WithMany()
                    .HasForeignKey(e => e.StudentID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Class)
                    .WithMany()
                    .HasForeignKey(e => e.ClassID)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
