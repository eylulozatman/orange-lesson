using System.IO;
using Microsoft.AspNetCore.Mvc;
using EducationSystemBackend.Services;
using EducationSystemBackend.Requests;
using EducationSystemBackend.Models;
using EducationSystemBackend.Repositories;

namespace EducationSystemBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeworksController : ControllerBase
    {
        private readonly IHomeworkService _service;
        private readonly IFirestoreRepository<StudentCourseInfo> _enrollRepo;

        public HomeworksController(IHomeworkService service, IFirestoreRepository<StudentCourseInfo> enrollRepo)
        {
            _service = service;
            _enrollRepo = enrollRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateHomeworkRequest req)
        {
            var hw = new Homework
            {
                CourseId = req.CourseId,
                TeacherId = req.TeacherId,
                Title = req.Title,
                Description = req.Description,
                DueDate = req.DueDate
            };

            await _service.CreateAsync(hw);
            return CreatedAtAction(nameof(GetByTeacher), new { teacherId = hw.TeacherId }, hw);
        }

        [HttpGet("teacher/{teacherId}")]
        public async Task<IActionResult> GetByTeacher(Guid teacherId)
        {
            var list = await _service.GetByTeacherAsync(teacherId);
            return Ok(list);
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetByStudent(Guid studentId)
        {
            var list = await _service.GetByStudentAsync(studentId);
            return Ok(list);
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromForm] SubmitHomeworkRequest req, IFormFile? file)
        {
            var submission = new HomeworkSubmission
            {
                HomeworkId = req.HomeworkId,
                StudentId = req.StudentId,
                Content = req.Content
            };

            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine("wwwroot", "uploads");
                Directory.CreateDirectory(uploads);
                var fileName = $"{submission.Id}_{Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(uploads, fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
                submission.FilePath = $"/uploads/{fileName}";
            }

            await _service.SubmitAsync(submission);
            return Ok(submission);
        }

        [HttpGet("{homeworkId}/submissions")]
        public async Task<IActionResult> GetSubmissions(Guid homeworkId)
        {
            var subs = await _service.GetSubmissionsAsync(homeworkId);
            return Ok(subs);
        }
    }
}

