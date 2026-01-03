using EducationSystemBackend.Models;
using EducationSystemBackend.Requests;

namespace EducationSystemBackend.Services
{
    public interface ICourseService
    {
        Task<List<Course>> GetByOrganizationAsync(string organizationId);
        Task<List<Course>> GetByStudentAsync(string studentId);
        Task<List<Course>> GetByTeacherAsync(string teacherId);

        Task<Course?> GetByIdAsync(string courseId);
        Task<string?> GetCourseIdByNameAsync(string organizationId, string courseName);

        Task<Course> CreateAsync(CreateCourseRequest request);

        Task EnrollStudentAsync(string studentId, string courseId);
    }
}
