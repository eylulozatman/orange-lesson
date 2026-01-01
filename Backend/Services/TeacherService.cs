using EducationSystemBackend.Models;
using EducationSystemBackend.Repositories;

namespace EducationSystemBackend.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teachers;
        private readonly ICourseRepository _courses;

        public TeacherService(
            ITeacherRepository teachers,
            ICourseRepository courses)
        {
            _teachers = teachers;
            _courses = courses;
        }

        // ---------------- AUTH ----------------

        public async Task<Teacher> RegisterAsync(Teacher teacher, Guid courseId)
        {
            await _teachers.AddAsync(teacher);

            await AssignCourseAsync(teacher.Id, courseId);

            return teacher;
        }

        public async Task<Teacher?> LoginAsync(string email, string password)
        {
            var teacher = await _teachers.GetByEmailAsync(email);
            if (teacher == null) return null;
            if (teacher.Password != password) return null;

            return teacher;
        }

        // ---------------- GET ----------------

        public async Task<Teacher?> GetByIdAsync(Guid teacherId)
        {
            return await _teachers.GetByIdAsync(teacherId);
        }

        public async Task<Teacher?> GetByEmailAsync(string email)
        {
            return await _teachers.GetByEmailAsync(email);
        }

        public async Task<List<Course>> GetCoursesAsync(Guid teacherId)
        {
            return await _courses.GetByTeacherIdAsync(teacherId);
        }

        // ---------------- COURSE ----------------

        public async Task AssignCourseAsync(Guid teacherId, Guid courseId)
        {
            var info = new TeacherCourseInfo
            {
                TeacherId = teacherId,
                CourseId = courseId
            };

            await _teachers.AssignCourseAsync(info);
        }
    }
}
