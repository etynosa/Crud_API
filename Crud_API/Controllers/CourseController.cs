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
using System.Runtime.InteropServices;

namespace Crud_API.Controllers
{
    [Route("course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<StudentController> _logger;

        public CourseController(ICourseRepository courseRepository, ILogger<StudentController> logger)
        {
            _courseRepository = courseRepository;
            _logger = logger;
        }

        [HttpGet, Route("all")]
        public async Task<GenericResult<Course>> GetCoursesAsync()
        {
            var result = await _courseRepository.GetAllAsync();

            return GenericResult<Course>.Succeed(result);

        }

        [HttpGet, Route("")]
        public async Task<GenericResult<Course>> GetCourseByIdAsync(long id)
        {
            var result = await _courseRepository.GetAsync(id);
            if (result == null)
            {
                return GenericResult<Course>.NotFound;
            }
            else
            {
                return GenericResult<Course>.Succeed(result);
            }
        }

        [HttpPost, Route("create")]
        public async Task<GenericResult<Course>> AddCourseAsync([FromBody] Course course)
        {
            var result = await _courseRepository.Add(course);
            if (result == null)
            {
                return GenericResult<Course>.BadRequest;
            }
            else
            {
                return GenericResult<Course>.CreatedWithMessage("Entity Created Successfully");
                _logger.LogInformation($"{DateTime.Now}: Course {course.CourseName} added to the database.");
            }

        }

        [HttpPut, Route("update")]
        public async Task<GenericResult<Course>> UpdateCourseAsync(Course course)
        {
            var result = await _courseRepository.GetAsync(course.Id);
            if (result == null)
            {
                return GenericResult<Course>.NotFound;
            }
            else
            {
                var updatedCourse = await _courseRepository.Update(course);
                return GenericResult<Course>.SucceedWithMessage("Entity Successfully Updated");
                _logger.LogInformation($"{DateTime.Now}: Course {course.CourseName} updated to the database.");
            }
    
        }

        [HttpDelete, Route("delete")]
        public async Task<GenericResult<bool>> Delete(long courseId)
        {
            var result = await _courseRepository.GetAsync(courseId);
            if (result == null)
            {
                return GenericResult<bool>.NotFound;
            }
            else
            {
                await _courseRepository.Remove(result);
                return GenericResult<bool>.SucceedWithMessage("Entity deleted successfully");
            }
        }

        [HttpGet, Route("filter")]
        public async Task<GenericResult<IEnumerable<Course>>> GetListAsync([FromQuery] string filter = null)
        {
            try
            {
                var courses = await _courseRepository.GetListAsync(s => s.CourseName == filter);
                return GenericResult<IEnumerable<Course>>.Succeed(courses);
            }
            catch (Exception ex)
            {
                return GenericResult<IEnumerable<Course>>.Fail(ex.Message);
            }
        }

        [HttpGet, Route("order")]
        public async Task<GenericResult<IEnumerable<Course>>> GetSortedAsync([FromQuery] string orderBy, bool ascending)
        {
            try
            {
                var courses = await _courseRepository.GetSortedAsync(s => EF.Property<object>(s, orderBy), ascending);
                return GenericResult<IEnumerable<Course>>.Succeed(courses);
            }
            catch (Exception ex)
            {
                return GenericResult<IEnumerable<Course>>.Fail(ex.Message);
            }

        }

        [HttpGet("page")]
        public async Task<GenericResult<IEnumerable<Course>>> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var courses = await _courseRepository.GetPagedAsync(pageNumber, pageSize);
                return GenericResult<IEnumerable<Course>>.Succeed(courses);
            }
            catch (Exception ex)
            {
                return GenericResult<IEnumerable<Course>>.Fail(ex.Message);
            }
        }
    }
}
