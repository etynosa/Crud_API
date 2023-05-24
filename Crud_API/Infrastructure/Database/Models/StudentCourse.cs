using Crud_API.Interfaces.Data;

namespace Crud_API.Infrastructure.Database.Models
{
    public class StudentCourse : IEntity
    {
        public long Id { get; set; }
        public long StudentId { get; set; }
        public long CourseId { get; set; }

    }
}
