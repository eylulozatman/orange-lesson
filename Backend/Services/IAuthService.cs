using EducationSystemBackend.Models;

namespace EducationSystemBackend.Services
{
    public interface IAuthService
    {
        Task<Student?> AuthenticateStudentAsync(string email, string password);
        Task<Teacher?> AuthenticateTeacherAsync(string email, string password);
    }
}
