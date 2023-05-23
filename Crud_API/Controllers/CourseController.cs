using Crud_API.DomainModels;
using Crud_API.DomainModels.Base;
using Crud_API.Infrastructure.Database.Models;
using Crud_API.Infrastructure.Database.Repositories;
using Crud_API.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet, Route("{:id}")]
        public async Task<GenericResult<Course>> GetCourseByIdAsync(long id)
        {
            var result = await _courseRepository.GetAsync(id);
            if (result == null)
            {
                return GenericResult<Course>.NotFound;
            }
            return GenericResult<Course>.Succeed(result);
        }

        [HttpPost, Route("create")]
        public async Task<GenericResult<Course>> AddCourseAsync([FromBody] Course course)
        {
            var result = await _courseRepository.Add(course);
            if (result == null)
            {
                return GenericResult<Course>.BadRequest;
            }

            return GenericResult<Course>.CreatedWithMessage("Entity Created Successfully");
            _logger.LogInformation($"{DateTime.Now}: Course {course.CourseName} added to the database.");
        }

        [HttpPut, Route("update/{:id}")]
        public async Task<GenericResult<Course>> UpdateCourseAsync(long courseId)
        {
            var result = await _courseRepository.GetAsync(courseId);
            if (result == null)
            {
                return GenericResult<Course>.NotFound;
            }
            var course = await _courseRepository.Update(result);
            if (course == null)
            {
                return GenericResult<Course>.BadRequest;
            }

            return GenericResult<Course>.SucceedWithMessage("Entity Successfully Updated");
            _logger.LogInformation($"{DateTime.Now}: Course {course.CourseName} updated to the database.");
        }

        [HttpDelete, Route("{:id}")]
        public async Task<IActionResult> Delete(long courseId) 
        {
            var result = await _courseRepository.GetAsync(courseId);
            return await _courseRepository.Remove(result);
        }

        [HttpGet, Route("filter/{filter}")]
        public async Task<GenericResult<Course>> GetListAsync(Expression<Func<Course, bool>> filter = null)
        {
            var courses = await _courseRepository.GetListAsync(filter);

            return GenericResult<Course>.Succeed(courses);
        }

        [HttpGet, Route("order/{orderby}")]
        public async Task<GenericResult<Course>> GetSortedAsync(Expression<Func<Course, object>> orderBy, bool ascending)
        {
            var students = await _courseRepository.GetSortedAsync(orderBy, ascending);
            return GenericResult<Course>.Succeed(students);
        }
    }
}
