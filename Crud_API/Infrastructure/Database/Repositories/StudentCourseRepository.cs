using Crud_API.Infrastructure.Database.Models;
using Crud_API.Interfaces.Repositories;

namespace Crud_API.Infrastructure.Database.Repositories
{
    public class StudentCourseRepository : Repository<StudentCourse>, IStudentCourseRepository
    {
        public StudentCourseRepository(CrudDbcontext databaseContext) 
            : base(databaseContext)
        {
        }
    }
}
