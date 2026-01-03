using EducationSystemBackend.Models;
using EducationSystemBackend.Repositories;

namespace EducationSystemBackend.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teachers;
        private readonly ICourseRepository _courses;
        private readonly IHomeworkRepository _homeworks;

        public TeacherService(
            ITeacherRepository teachers,
            ICourseRepository courses,
            IHomeworkRepository homeworks)
        {
            _teachers = teachers;
            _courses = courses;
            _homeworks = homeworks;
        }

        public async Task<Teacher> RegisterAsync(Teacher teacher, string courseId)
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

        public Task<Teacher?> GetByIdAsync(string teacherId)
            => _teachers.GetByIdAsync(teacherId);

        public Task<Teacher?> GetByEmailAsync(string email)
            => _teachers.GetByEmailAsync(email);

        public Task<List<Course>> GetCoursesAsync(string teacherId)
            => _courses.GetByTeacherIdAsync(teacherId);

        public async Task AssignCourseAsync(string teacherId, string courseId)
        {
            await _teachers.AssignCourseAsync(new TeacherCourseInfo
            {
                Id = Guid.NewGuid().ToString(),
                TeacherId = teacherId,
                CourseId = courseId
            });
        }

        // 🆕 HOMEWORK
        public async Task<Homework> CreateHomeworkAsync(Homework homework)
        {
            await _homeworks.AddAsync(homework);
            return homework;
        }

        public Task<List<Homework>> GetHomeworksAsync(string teacherId)
            => _homeworks.GetByTeacherIdAsync(teacherId);
    }
}
