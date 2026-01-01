using EducationSystemBackend.Models;

namespace EducationSystemBackend.Services
{
    public interface ITeacherService
    {
        Task<Teacher> RegisterAsync(Teacher teacher, Guid courseId);
        Task<Teacher?> LoginAsync(string email, string password);
    }
}
