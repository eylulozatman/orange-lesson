using EducationSystemBackend.Requests;
using EducationSystemBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystemBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetTeacherInfo([FromQuery] string email)
        {
            var teacher = await _teacherService.GetByEmailAsync(email);
            if (teacher == null) return NotFound();

            var courseInfo = await _teacherService.GetTeacherCourseAsync(teacher.Id);
            return Ok(new
            {
                teacher.Id,
                teacher.FullName,
                teacher.Email,
                teacher.City,
                Course = courseInfo
            });
        }

        [HttpPost("assign-course")]
        public async Task<IActionResult> AssignCourse(Guid teacherId, Guid courseId)
        {
            try
            {
                var result = await _teacherService.AssignCourseAsync(teacherId, courseId);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
