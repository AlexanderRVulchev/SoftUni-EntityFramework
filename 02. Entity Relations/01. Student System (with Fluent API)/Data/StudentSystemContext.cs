using Microsoft.EntityFrameworkCore;

namespace P01_StudentSystem.Data
{
    using P01_StudentSystem.Data.Models;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.StudentId);
                entity.Property(s => s.Name).HasMaxLength(100);
                entity.Property(s => s.PhoneNumber).IsRequired(false).HasMaxLength(10).IsUnicode(false);                               
                entity.Property(s => s.Birthday).IsRequired(false);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.CourseId);
                entity.Property(c => c.Name).HasMaxLength(80);
                entity.Property(c => c.Description).IsRequired(false);
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasKey(r => r.ResourceId);
                entity.Property(r => r.Name).HasMaxLength(50).IsUnicode();
                entity.Property(r => r.Url).IsUnicode(false);

                entity.HasOne(r => r.Course).WithMany(c => c.Resources).HasForeignKey(r => r.ResourceId);
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.HasKey(h => h.HomeworkId);
                entity.Property(h => h.Content).IsUnicode(false);

                entity.HasOne(h => h.Course).WithMany(c => c.HomeworkSubmissions).HasForeignKey(h => h.CourseId);
                entity.HasOne(h => h.Student).WithMany(s => s.HomeworkSubmissions).HasForeignKey(h => h.StudentId);
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(sc => new { sc.StudentId, sc.CourseId });

                entity.HasOne(sc => sc.Student).WithMany(s => s.CourseEnrollments).HasForeignKey(sc => sc.StudentId);
                entity.HasOne(sc => sc.Course).WithMany(c => c.StudentsEnrolled).HasForeignKey(sc => sc.CourseId);
            });
        }
    }
}
