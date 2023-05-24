using Crud_API.DomainModels.Base;
using Crud_API.Infrastructure.Database.Models;
using Crud_API.Infrastructure.Database.Repositories;
using Crud_API.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet, Route("id")]
        public async Task<GenericResult<StudentCourse>> GetCourseByIdAsync(long id)
        {
            var result = await _studentCourseRepository.GetAsync(id);
            if (result == null)
            {
                return GenericResult<StudentCourse>.NotFound;
            }
            else
            {
                return GenericResult<StudentCourse>.Succeed(result);
            }

        }

        [HttpPost, Route("create")]
        public async Task<GenericResult<bool>> AddStudentCourseAsync([FromBody]StudentCourse studentCourse)
        {
            var result = await _studentCourseRepository.Add(studentCourse);
            if (result == null)
            {
                return GenericResult<bool>.BadRequest;
            }
            else
            {
                return GenericResult<bool>.CreatedWithMessage("Entity Created Successfully");
                _logger.LogInformation($"{DateTime.Now}: Course {studentCourse.Id} added to the database.");
            }

        }

        [HttpPut, Route("update")]
        public async Task<GenericResult<StudentCourse>> UpdateStudentCourseAsync(StudentCourse studentCourse)
        {
            var result = await _studentCourseRepository.GetAsync(studentCourse.Id);
            if (result == null)
            {
                return GenericResult<StudentCourse>.NotFound;
            }
            else
            {
                var updatedStudentCourse = await _studentCourseRepository.Update(result);
                return GenericResult<StudentCourse>.SucceedWithMessage("Entity Updated Successfully");
                _logger.LogInformation($"{DateTime.Now}: Course {studentCourse.Id} updated to the database.");
            }

        }

        [HttpDelete, Route("delete/id")]
        public async Task<GenericResult<bool>> Delete(long studentCourseId)
        {
            var result = await _studentCourseRepository.GetAsync(studentCourseId);
            if (result == null)
            {
                return GenericResult<bool>.NotFound;
            }
            else
            {
                await _studentCourseRepository.Remove(result);
                return GenericResult<bool>.SucceedWithMessage("Entity deleted successfully");
            }
        }

        [HttpGet, Route("filter")]
        public async Task<GenericResult<IEnumerable<StudentCourse>>> GetListAsync([FromQuery] string filter = null)
        {
            try
            {
                var studentcourses = await _studentCourseRepository.GetListAsync(s => s.Course.CourseName == filter);
                return GenericResult<IEnumerable<StudentCourse>>.Succeed(studentcourses);
            }
            catch (Exception ex)
            {
                return GenericResult<IEnumerable<StudentCourse>>.Fail(ex.Message);
            }
        }

        [HttpGet, Route("order")]
        public async Task<GenericResult<IEnumerable<StudentCourse>>> GetSortedAsync([FromQuery] string orderBy, bool ascending)
        {
            try
            {
                var studentcourses = await _studentCourseRepository.GetSortedAsync(s => EF.Property<object>(s, orderBy), ascending);
                return GenericResult<IEnumerable<StudentCourse>>.Succeed(studentcourses);
            }
            catch (Exception ex)
            {
                return GenericResult<IEnumerable<StudentCourse>>.Fail(ex.Message);
            }

        }

        [HttpGet("page")]
        public async Task<GenericResult<IEnumerable<StudentCourse>>> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var studentcourses = await _studentCourseRepository.GetPagedAsync(pageNumber, pageSize);
                return GenericResult<IEnumerable<StudentCourse>>.Succeed(studentcourses);
            }
            catch (Exception ex)
            {
                return GenericResult<IEnumerable<StudentCourse>>.Fail(ex.Message);
            }
        }
    }
}
