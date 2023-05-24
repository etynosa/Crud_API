using Crud_API.Infrastructure.Database.Models;
using Crud_API.Infrastructure.Database.Repositories.NewFolder;
using Microsoft.EntityFrameworkCore;

namespace Crud_API.Infrastructure.Database
{
    public class CrudDbcontext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        public CrudDbcontext(DbContextOptions<CrudDbcontext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new StudentCourseConfiguration());
        }


    }
}
