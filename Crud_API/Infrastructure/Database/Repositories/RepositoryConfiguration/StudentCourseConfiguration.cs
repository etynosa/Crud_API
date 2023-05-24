using Crud_API.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Crud_API.Infrastructure.Database.Repositories.NewFolder
{
    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.HasData(
                new StudentCourse
                {
                    Id = 3,
                    StudentId = 3,
                    CourseId = 3
                },
                new StudentCourse
                {
                    Id = 4,
                    StudentId = 4,
                    CourseId = 4
                }
            );
        }
    }

}
