using Microsoft.AspNetCore.Mvc;
using EducationSystemBackend.Services;
using EducationSystemBackend.Requests;
using EducationSystemBackend.Responses;
using EducationSystemBackend.Models;

namespace EducationSystemBackend.Controllers
{
    [ApiController]
    [Route("api/teachers")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _service;

        public TeachersController(ITeacherService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(TeacherRegisterRequest req)
        {
            var teacher = new Teacher
            {
                OrganizationId = req.OrganizationId,
                FullName = req.FullName,
                Email = req.Email,
                Password = req.Password,
                City = req.City
            };

            await _service.RegisterAsync(teacher, req.CourseId);

            return Ok(new AuthResponse
            {
                UserId = teacher.Id,
                Role = "Teacher"
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(TeacherLoginRequest req)
        {
            var teacher = await _service.LoginAsync(req.Email, req.Password);
            if (teacher == null) return Unauthorized();

            return Ok(new AuthResponse
            {
                UserId = teacher.Id,
                Role = "Teacher"
            });
        }

         [HttpPost("homeworks")]
        public async Task<IActionResult> CreateHomework([FromBody] Homework homework)
        {
            var result = await _service.CreateHomeworkAsync(homework);
            return Ok(result);
        }

        [HttpGet("{teacherId}/homeworks")]
        public async Task<IActionResult> GetHomeworks(string teacherId)
        {
            var list = await _service.GetHomeworksAsync(teacherId);
            return Ok(list);
        }
    }
}
