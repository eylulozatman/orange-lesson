using EducationSystemBackend.Models;

public interface ICourseService
{
    Task<List<Course>> GetByOrganizationAsync(Guid organizationId);
    Task<List<Course>> GetByStudentAsync(Guid studentId);
    Task<List<Course>> GetByTeacherAsync(Guid teacherId);

    Task<Course?> GetByIdAsync(Guid courseId);
    Task<Guid?> GetCourseIdByNameAsync(Guid organizationId, string courseName);

    Task<Course> CreateAsync(Course course);

    Task EnrollStudentAsync(Guid studentId, Guid courseId);
}
