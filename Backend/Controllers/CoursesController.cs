using EducationSystemBackend.Models;
using EducationSystemBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystemBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            await _courseService.AddCourseAsync(course);
            return Ok(course);
        }
    }
}
