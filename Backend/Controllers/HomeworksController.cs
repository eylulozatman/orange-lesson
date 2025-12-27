using EducationSystemBackend.Models;
using EducationSystemBackend.Requests;
using EducationSystemBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystemBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeworksController : ControllerBase
    {
        private readonly IHomeworkService _homeworkService;
        private readonly IWebHostEnvironment _env;

        public HomeworksController(IHomeworkService homeworkService, IWebHostEnvironment env)
        {
            _homeworkService = homeworkService;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateHomeworkRequest request)
        {
            var homework = await _homeworkService.CreateHomeworkAsync(request);
            return Ok(homework);
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetForStudent(Guid studentId)
        {
            var homeworks = await _homeworkService.GetHomeworksForStudentAsync(studentId);
            return Ok(homeworks);
        }

        [HttpGet("teacher/{teacherId}")]
        public async Task<IActionResult> GetByTeacher(Guid teacherId)
        {
            var homeworks = await _homeworkService.GetHomeworksByTeacherAsync(teacherId);
            return Ok(homeworks);
        }

        [HttpPost("submit")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Submit([FromForm] SubmitHomeworkRequest request, IFormFile? file)
        {
            string? filePath = null;
            if (file != null)
            {
                var uploads = Path.Combine(_env.ContentRootPath, "wwwroot", "uploads");
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
                
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                filePath = Path.Combine("uploads", fileName);
                var fullPath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            var submission = await _homeworkService.SubmitHomeworkAsync(request, filePath);
            return Ok(submission);
        }

        [HttpDelete("submission/{submissionId}")]
        public async Task<IActionResult> DeleteSubmission(Guid submissionId, [FromQuery] Guid studentId)
        {
            var result = await _homeworkService.DeleteSubmissionAsync(submissionId, studentId);
            if (!result) return BadRequest("Could not delete submission.");
            return Ok(new { Message = "Submission deleted." });
        }

        [HttpGet("{homeworkId}/submissions")]
        public async Task<IActionResult> GetSubmissions(Guid homeworkId)
        {
            var submissions = await _homeworkService.GetSubmissionsByHomeworkIdAsync(homeworkId);
            return Ok(submissions);
        }
    }
}
