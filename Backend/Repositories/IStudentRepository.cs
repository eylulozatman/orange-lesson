using EducationSystemBackend.Models;

namespace EducationSystemBackend.Repositories
{
   public interface IStudentRepository
{
    Task AddAsync(Student student);
    Task<Student?> GetByEmailAsync(string email);
    Task<Student?> GetByIdAsync(Guid id);
    Task<List<Student>> GetAllAsync();

    Task EnrollAsync(StudentCourseInfo info);
    Task<List<Course>> GetCoursesByStudentId(Guid studentId);
}

}
