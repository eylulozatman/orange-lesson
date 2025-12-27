using EducationSystemBackend.Models;
using EducationSystemBackend.Requests;
using EducationSystemBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystemBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(StudentRegisterRequest request)
        {
            var response = await _studentService.RegisterAsync(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost("enroll")]
        public async Task<IActionResult> Enroll(Guid studentId, Guid courseId)
        {
            await _studentService.EnrollAsync(studentId, courseId);
            return Ok(new { Message = "Enrolled successfully." });
        }
    }
}
