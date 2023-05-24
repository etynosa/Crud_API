using Crud_API.Interfaces.Data;

namespace Crud_API.Infrastructure.Database.Models
{
    public class Course : IEntity
    {
        public long Id { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
    }

}
