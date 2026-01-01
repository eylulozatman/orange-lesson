using EducationSystemBackend.Models;

namespace EducationSystemBackend.Services
{
    public interface IStudentService
    {
        Task<Student> RegisterAsync(Student student);
        Task<Student?> LoginAsync(string email, string password);
        Task<List<Student>> GetAllAsync();
    }
}
