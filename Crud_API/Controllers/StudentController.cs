using Crud_API.DomainModels;
using Crud_API.DomainModels.Base;
using Crud_API.Infrastructure.Database.Models;
using Crud_API.Infrastructure.Database.Repositories;
using Crud_API.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        [HttpGet, Route("/id")]
        public async Task<GenericResult<Student>> GetCourseByIdAsync(long id)
        {
            var result = await _studentRepository.GetAsync(id);
            if (result == null)
            {
                return GenericResult<Student>.NotFound;
            }
            else
            {
                return GenericResult<Student>.Succeed(result);
            }
        }

        [HttpPost, Route("create")]
        public async Task<GenericResult<Student>> AddStudentAsync([FromBody] Student student)
        {
            var result = await _studentRepository.Add(student);
            if (result == null)
            {
                return GenericResult<Student>.BadRequest;
            }
            else
            {
                return GenericResult<Student>.CreatedWithMessage("Entity Created Successfully");
                _logger.LogInformation($"{DateTime.Now}: Course {student.FirstName} added to the database.");
            }
        }

        [HttpPut, Route("update")]
        public async Task<GenericResult<Student>> UpdateStudentAsync(Student student)
        {
            var getStudent = await _studentRepository.GetAsync(student.Id);
            if (getStudent == null)
            {
                return GenericResult<Student>.NotFound;
            }
            else
            {
                var result = await _studentRepository.Update(student);
                return GenericResult<Student>.SucceedWithMessage("Entity Updated Successfully");
                _logger.LogInformation($"{DateTime.Now}: Course {student.FirstName} added to the database.");
            }
        }

        [HttpDelete, Route("delete")]
        public async Task<GenericResult<bool>> Delete(long studentId)
        {
            var result = await _studentRepository.GetAsync(studentId);
            if (result == null)
            {
                return GenericResult<bool>.NotFound;
            }
            else
            {
                await _studentRepository.Remove(result);
                return GenericResult<bool>.SucceedWithMessage("Entity deleted successfully");
            }
        }

        [HttpGet, Route("filter")]
        public async Task<GenericResult<IEnumerable<Student>>> GetListAsync([FromQuery] string filter = null)
        {
            try
            {
                var students = await _studentRepository.GetListAsync(s => s.Class == filter);
                return GenericResult<IEnumerable<Student>>.Succeed(students);
            }
            catch (Exception ex)
            {
                return GenericResult<IEnumerable<Student>>.Fail(ex.Message);
            }
        }

        [HttpGet, Route("order")]
        public async Task<GenericResult<IEnumerable<Student>>> GetSortedAsync([FromQuery] string orderBy, bool ascending)
        {
            try
            {
                var students = await _studentRepository.GetSortedAsync(s => EF.Property<object>(s, orderBy), ascending);
                return GenericResult<IEnumerable<Student>>.Succeed(students);
            }
            catch (Exception ex)
            {
                return GenericResult<IEnumerable<Student>>.Fail(ex.Message);
            }
          
        }

        [HttpGet("page")]
        public async Task<GenericResult<IEnumerable<Student>>> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var students = await _studentRepository.GetPagedAsync(pageNumber, pageSize);
                return GenericResult<IEnumerable<Student>>.Succeed(students);
            }
            catch (Exception ex)
            {
                return GenericResult<IEnumerable<Student>>.Fail(ex.Message);
            }
        }
    }
}
