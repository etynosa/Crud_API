using Crud_API.Infrastructure.Database.Models;
using Crud_API.Interfaces.Repositories;

namespace Crud_API.Infrastructure.Database.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(CrudDbcontext databaseContext) 
            : base(databaseContext)
        {
        }
    }
}
