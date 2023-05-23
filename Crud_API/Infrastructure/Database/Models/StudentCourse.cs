using Crud_API.Interfaces.Data;

namespace Crud_API.Infrastructure.Database.Models
{
    public class StudentCourse : IEntity
    {
        public long Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        #region Navigation properties

        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }

        #endregion
    }
}
