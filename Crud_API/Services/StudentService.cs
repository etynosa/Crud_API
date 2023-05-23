using Crud_API.DomainModels;
using Crud_API.DomainModels.Base;
using Crud_API.DomainModels.Enums;
using Crud_API.Infrastructure.Database;
using Crud_API.Infrastructure.Database.Models;
using Crud_API.Interfaces.Repositories;
using Crud_API.Interfaces.Services;

namespace Crud_API.Services
{
    public class StudentService : IStudentService
    {
        private readonly CrudDbcontext _dbcontext;
        private readonly IStudentRepository _studentRepository;

        public StudentService(CrudDbcontext dbcontext, IStudentRepository studentRepository)
        {
            _dbcontext = dbcontext;
            _studentRepository = studentRepository;
        }
    }
}
