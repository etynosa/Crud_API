using Crud_API.Interfaces.Data;

namespace Crud_API.Infrastructure.Database.Models
{
    public class Student : IEntity 
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
