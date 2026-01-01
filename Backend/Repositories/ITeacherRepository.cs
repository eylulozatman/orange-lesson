using EducationSystemBackend.Models;

namespace EducationSystemBackend.Repositories
{
   public interface ITeacherRepository
{
    Task AddAsync(Teacher teacher);
    Task<Teacher?> GetByEmailAsync(string email);
    Task<Teacher?> GetByIdAsync(Guid id);

    Task AssignCourseAsync(TeacherCourseInfo info);
    Task<List<Course>> GetCoursesByTeacherId(Guid teacherId);
}

}
