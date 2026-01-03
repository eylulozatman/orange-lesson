using EducationSystemBackend.Models;
using EducationSystemBackend.Responses;

namespace EducationSystemBackend.Services
{
    public interface ITeacherService
    {
        Task<Teacher> RegisterAsync(Teacher teacher, string courseId);
        Task<Teacher?> LoginAsync(string email, string password);

        Task<Teacher?> GetByIdAsync(string teacherId);
        Task<Teacher?> GetByEmailAsync(string email);

        Task<List<Course>> GetCoursesAsync(string teacherId);

        Task AssignCourseAsync(string teacherId, string courseId);
        Task<Homework> CreateHomeworkAsync(Homework homework);
        Task<List<Homework>> GetHomeworksAsync(string teacherId);
    }
}
