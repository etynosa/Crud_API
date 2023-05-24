using Crud_API.Infrastructure.Database.Models;
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


    }
}
