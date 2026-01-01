using Microsoft.AspNetCore.Mvc;
using EducationSystemBackend.Services;
using EducationSystemBackend.Models;

namespace EducationSystemBackend.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;

        public CoursesController(ICourseService service)
        {
            _service = service;
        }

        // 🔹 Organization’a ait tüm dersler
        // GET api/courses/by-organization/{organizationId}
        [HttpGet("by-organization/{organizationId}")]
        public async Task<IActionResult> GetByOrganization(Guid organizationId)
        {
            var courses = await _service.GetByOrganizationAsync(organizationId);
            return Ok(courses);
        }

        // 🔹 Öğrencinin kayıtlı olduğu dersler
        // GET api/courses/by-student/{studentId}
        [HttpGet("by-student/{studentId}")]
        public async Task<IActionResult> GetByStudent(Guid studentId)
        {
            var courses = await _service.GetByStudentAsync(studentId);
            return Ok(courses);
        }

        // 🔹 Öğretmenin verdiği dersler
        // GET api/courses/by-teacher/{teacherId}
        [HttpGet("by-teacher/{teacherId}")]
        public async Task<IActionResult> GetByTeacher(Guid teacherId)
        {
            var courses = await _service.GetByTeacherAsync(teacherId);
            return Ok(courses);
        }

        // 🔹 CourseId çekmek (frontend için)
        // GET api/courses/id-by-name?organizationId=...&courseName=...
        [HttpGet("id-by-name")]
        public async Task<IActionResult> GetCourseIdByName(
            [FromQuery] Guid organizationId,
            [FromQuery] string courseName)
        {
            var courseId = await _service.GetCourseIdByNameAsync(organizationId, courseName);
            if (courseId == null) return NotFound();

            return Ok(new { CourseId = courseId });
        }

        // 🔹 Yeni ders oluşturma
        // POST api/courses
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Course course)
        {
            var created = await _service.CreateAsync(course);
            return CreatedAtAction(nameof(GetByOrganization),
                new { organizationId = created.OrganizationId },
                created);
        }

        // 🔹 Öğrenciyi derse kaydet
        // POST api/courses/enroll
        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollStudent(
            [FromQuery] Guid studentId,
            [FromQuery] Guid courseId)
        {
            await _service.EnrollStudentAsync(studentId, courseId);
            return Ok();
        }
    }
}
