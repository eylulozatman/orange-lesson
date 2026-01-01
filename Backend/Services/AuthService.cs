using EducationSystemBackend.Models;
using EducationSystemBackend.Repositories;

namespace EducationSystemBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IFirestoreRepository<Student> _students;
        private readonly IFirestoreRepository<Teacher> _teachers;

        public AuthService(IFirestoreRepository<Student> students, IFirestoreRepository<Teacher> teachers)
        {
            _students = students;
            _teachers = teachers;
        }

        public async Task<Student?> AuthenticateStudentAsync(string email, string password)
        {
            var matches = await _students.QueryAsync("Email", email);
            var student = matches.FirstOrDefault();
            if (student != null && student.Password == password) return student;
            return null;
        }

        public async Task<Teacher?> AuthenticateTeacherAsync(string email, string password)
        {
            var matches = await _teachers.QueryAsync("Email", email);
            var teacher = matches.FirstOrDefault();
            if (teacher != null && teacher.Password == password) return teacher;
            return null;
        }
    }
}
