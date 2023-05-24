using Crud_API.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Crud_API.Infrastructure.Database.Repositories.NewFolder
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData(
                new Student
                {
                    Id = 3,
                    FirstName = "John",
                    LastName = "Doe",
                    Class = "10A",
                    DateOfBirth = new DateTime(2005, 10, 15)
                },
                new Student
                {
                    Id = 4,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Class = "11B",
                    DateOfBirth = new DateTime(2004, 9, 20)
                }
            );
        }
    }

}
