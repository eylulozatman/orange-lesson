using EducationSystemBackend.Models;

namespace EducationSystemBackend.Repositories
{
    public interface ITeacherRepository
    {
        Task AddAsync(Teacher teacher);

        Task<Teacher?> GetByIdAsync(Guid teacherId);
        Task<Teacher?> GetByEmailAsync(string email);

        Task<List<Teacher>> GetAllAsync();

        Task AssignCourseAsync(TeacherCourseInfo info);
        Task<List<TeacherCourseInfo>> GetTeacherCoursesAsync(Guid teacherId);
    }
}
