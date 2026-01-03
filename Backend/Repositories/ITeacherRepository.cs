using EducationSystemBackend.Models;

namespace EducationSystemBackend.Repositories
{
    public interface ITeacherRepository
    {
        Task AddAsync(Teacher teacher);

        Task<Teacher?> GetByIdAsync(string teacherId);
        Task<Teacher?> GetByEmailAsync(string email);

        Task<List<Teacher>> GetAllAsync();

        Task AssignCourseAsync(TeacherCourseInfo info);
        Task<List<TeacherCourseInfo>> GetTeacherCoursesAsync(string teacherId);
    }
}
