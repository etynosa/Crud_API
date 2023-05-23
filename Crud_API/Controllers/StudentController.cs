using Crud_API.DomainModels;
using Crud_API.DomainModels.Base;
using Crud_API.Infrastructure.Database.Models;
using Crud_API.Infrastructure.Database.Repositories;
using Crud_API.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud_API.Controllers
{
    [Route("students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<StudentController> _logger;
        public StudentController(ILogger<StudentController> logger, IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet, Route("all")]
        public async Task<GenericResult<Student>> GetStudentsAsync()
        {
            var result = await _studentRepository.GetAllAsync();

            return GenericResult<Student>.Succeed(result);
            
        }

        [HttpGet, Route("{:id}")]
        public async Task<GenericResult<Student>> GetCourseByIdAsync(long id)
        {
            var result = await _studentRepository.GetAsync(id);
            if (result == null)
            {
                return GenericResult<Student>.NotFound;
            }
            return GenericResult<Student>.Succeed(result);
        }

        [HttpPost, Route("create")]
        public async Task<GenericResult<Student>> AddStudentAsync([FromBody] Student student)
        {
            var result = await _studentRepository.Add(student);
            if (result == null)
            {
                return GenericResult<Student>.BadRequest;
            }

            return GenericResult<Student>.CreatedWithMessage("Entity Created Successfully");
            _logger.LogInformation($"{DateTime.Now}: Course {student.FirstName} added to the database.");
        }

        [HttpPut, Route("update/{:id}")]
        public async Task<GenericResult<Student>> UpdateStudentAsync(long studentId)
        {
            var result = await _studentRepository.GetAsync(studentId);
            if (result == null)
            {
                return GenericResult<Student>.NotFound;
            }

            var student = await _studentRepository.Update(result);
            if (student == null)
            {
                return GenericResult<Student>.BadRequest;
            }

            return GenericResult<Student>.SucceedWithMessage("Entity Updated Successfully");
            _logger.LogInformation($"{DateTime.Now}: Course {student.FirstName} added to the database.");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long studentId)
        {
            var result = await _studentRepository.GetAsync(studentId);
            return await _studentRepository.Remove(result);

        }
    }
}
