using Crud_API.Infrastructure.Database.Models;
using Crud_API.Interfaces.Repositories;

namespace Crud_API.Infrastructure.Database.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(CrudDbcontext databaseContext) 
            : base(databaseContext)
        {
        }
    }
}
