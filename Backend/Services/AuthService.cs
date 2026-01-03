using EducationSystemBackend.Models;
using EducationSystemBackend.Repositories;

namespace EducationSystemBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IStudentRepository _students;
        private readonly ITeacherRepository _teachers;

        public AuthService(
            IStudentRepository students,
            ITeacherRepository teachers)
        {
            _students = students;
            _teachers = teachers;
        }

        public async Task<Student?> AuthenticateStudentAsync(string email, string password)
        {
            var student = await _students.GetByEmailAsync(email);
            if (student == null) return null;

            return student.Password == password ? student : null;
        }

        public async Task<Teacher?> AuthenticateTeacherAsync(string email, string password)
        {
            var teacher = await _teachers.GetByEmailAsync(email);
            if (teacher == null) return null;

            return teacher.Password == password ? teacher : null;
        }
    }
}
