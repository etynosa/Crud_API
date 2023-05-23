using Crud_API.DomainModels.Base;
using Crud_API.Infrastructure.Database.Models;
using Crud_API.Infrastructure.Database.Repositories;
using Crud_API.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Crud_API.Controllers
{
    [Route("studentcourse")]
    [ApiController]
    public class StudentCourseController : ControllerBase
    {
        private readonly IStudentCourseRepository _studentCourseRepository;
        private readonly ILogger<StudentCourseController> _logger;

        public StudentCourseController(IStudentCourseRepository studentCourseRepository, ILogger<StudentCourseController> logger)
        {
            _studentCourseRepository = studentCourseRepository;
            _logger = logger;
        }

        [HttpGet, Route("all")]
        public async Task<GenericResult<StudentCourse>> GetStudentsCoursesAsync()
        {
            var result = await _studentCourseRepository.GetAllAsync();

            return GenericResult<StudentCourse>.Succeed(result);

        }

        [HttpGet, Route("{:id}")]
        public async Task<GenericResult<StudentCourse>> GetCourseByIdAsync(long id)
        {
            var result = await _studentCourseRepository.GetAsync(id);
            if (result == null)
            {
                return GenericResult<StudentCourse>.NotFound;
            }
            return GenericResult<StudentCourse>.Succeed(result);
        }

        [HttpPost, Route("create")]
        public async Task<GenericResult<StudentCourse>> AddStudentCourseAsync([FromBody]StudentCourse studentCourse)
        {
            var result = await _studentCourseRepository.Add(studentCourse);
            if (result == null)
            {
                return GenericResult<StudentCourse>.BadRequest;
            }

            return GenericResult<StudentCourse>.CreatedWithMessage("Entity Created Successfully");
            _logger.LogInformation($"{DateTime.Now}: Course {studentCourse.Id} added to the database.");
        }

        [HttpPut, Route("Create")]
        public async Task<GenericResult<StudentCourse>> UpdateStudentCourseAsync(long studentCourseId)
        {
            var result = await _studentCourseRepository.GetAsync(studentCourseId);
            if (result == null)
            {
                return GenericResult<StudentCourse>.NotFound;
            }
            var studentCourse = await _studentCourseRepository.Update(result);
            if (studentCourse == null)
            {
                return GenericResult<StudentCourse>.BadRequest;
            }

            return GenericResult<StudentCourse>.SucceedWithMessage("Entity Updated Successfully");
            _logger.LogInformation($"{DateTime.Now}: Course {studentCourse.Id} updated to the database.");
        }

        [HttpDelete, Route("{:id}")]
        public async Task<IActionResult> Delete(long studentCourseId)
        {
            var result = await _studentCourseRepository.GetAsync(studentCourseId);
            return await _studentCourseRepository.Remove(result);

        }

        [HttpGet, Route("filter/{filter}")]
        public async Task<GenericResult<StudentCourse>> GetListAsync(Expression<Func<StudentCourse, bool>> filter = null)
        {
            var studentCourse = await _studentCourseRepository.GetListAsync(filter);

            return GenericResult<StudentCourse>.Succeed(studentCourse);
        }

        [HttpGet, Route("order/{orderby}")]
        public async Task<GenericResult<StudentCourse>> GetSortedAsync(Expression<Func<StudentCourse, object>> orderBy, bool ascending)
        {
            var studentsCourse = await _studentCourseRepository.GetSortedAsync(orderBy, ascending);
            return GenericResult<StudentCourse>.Succeed(studentsCourse);
        }
    }
}
