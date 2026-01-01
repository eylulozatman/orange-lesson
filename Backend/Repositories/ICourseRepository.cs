using EducationSystemBackend.Models;

namespace EducationSystemBackend.Repositories
{
public interface ICourseRepository
{
    Task AddAsync(Course course);
    Task<Course?> GetByIdAsync(Guid courseId);

    Task<Course?> GetByNameAsync(Guid organizationId, string courseName);

    Task<List<Course>> GetByOrganizationIdAsync(Guid organizationId);
    Task<List<Course>> GetByStudentIdAsync(Guid studentId);
    Task<List<Course>> GetByTeacherIdAsync(Guid teacherId);

    Task EnrollStudentAsync(Guid studentId, Guid courseId);
}

}