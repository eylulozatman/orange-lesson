using EducationSystemBackend.Models;

namespace EducationSystemBackend.Repositories
{
    public interface ICourseRepository
    {
        Task AddAsync(Course course);

        Task<Course?> GetByIdAsync(string id);
        Task<Course?> GetByNameAsync(string organizationId, string courseName);

        Task<List<Course>> GetByOrganizationIdAsync(string organizationId);
        Task<List<Course>> GetByStudentIdAsync(string studentId);
        Task<List<Course>> GetByTeacherIdAsync(string teacherId);

        Task EnrollStudentAsync(string studentId, string courseId);
    }
}
