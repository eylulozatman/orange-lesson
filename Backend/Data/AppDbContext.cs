using EducationSystemBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationSystemBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<HomeworkSubmission> HomeworkSubmissions { get; set; }
        public DbSet<StudentCourseInfo> StudentCourseInfos { get; set; }
        public DbSet<TeacherCourseInfo> TeacherCourseInfos { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Organization Relationships
            modelBuilder.Entity<Organization>()
                .HasMany(o => o.Students)
                .WithOne()
                .HasForeignKey(s => s.OrganizationId);

            modelBuilder.Entity<Organization>()
                .HasMany(o => o.Teachers)
                .WithOne()
                .HasForeignKey(t => t.OrganizationId);

            modelBuilder.Entity<Organization>()
                .HasMany(o => o.Courses)
                .WithOne()
                .HasForeignKey(c => c.OrganizationId);
        }
    }
}
