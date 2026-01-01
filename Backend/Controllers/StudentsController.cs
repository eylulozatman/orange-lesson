using Microsoft.AspNetCore.Mvc;
using EducationSystemBackend.Services;
using EducationSystemBackend.Requests;
using EducationSystemBackend.Responses;
using EducationSystemBackend.Models;

namespace EducationSystemBackend.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(StudentRegisterRequest req)
        {
            var student = new Student
            {
                OrganizationId = req.OrganizationId,
                FullName = req.FullName,
                Email = req.Email,
                Password = req.Password,
                City = req.City,
                Grade = req.Grade
            };

            await _service.RegisterAsync(student);

            return Ok(new AuthResponse
            {
                UserId = student.Id,
                Role = "Student"
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(StudentLoginRequest req)
        {
            var student = await _service.LoginAsync(req.Email, req.Password);
            if (student == null) return Unauthorized();

            return Ok(new AuthResponse
            {
                UserId = student.Id,
                Role = "Student"
            });
        }

        [HttpPost("submit-homework")]
        public async Task<IActionResult> SubmitHomework(SubmitHomeworkRequest req)
        {
            var submission = new HomeworkSubmission
            {
                HomeworkId = req.HomeworkId,
                CourseId = req.CourseId,
                StudentId = req.StudentId,
                Content = req.Content
            };

            await _service.SubmitHomeworkAsync(submission);
            return Ok(submission);
        }
    }
}
