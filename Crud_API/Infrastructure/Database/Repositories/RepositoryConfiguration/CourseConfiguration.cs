using Crud_API.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Crud_API.Infrastructure.Database.Repositories.NewFolder
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasData(
                new Course
                {
                    Id = 3,
                    CourseCode = "CSC101",
                    CourseName = "Introduction to Computer Science",
                    CourseTitle = "CS101"
                },
                new Course
                {
                    Id = 4,
                    CourseCode = "MAT201",
                    CourseName = "Linear Algebra",
                    CourseTitle = "LA201"
                }
            );
        }
    }

}
