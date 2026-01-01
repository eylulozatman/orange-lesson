using EducationSystemBackend.Models;

namespace EducationSystemBackend.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        // In-memory storage
        public static readonly List<Teacher> Teachers = new();
        public static readonly List<TeacherCourseInfo> TeacherCourses = new();

        // ---------------- TEACHER ----------------

        public Task AddAsync(Teacher teacher)
        {
            Teachers.Add(teacher);
            return Task.CompletedTask;
        }

        public Task<Teacher?> GetByIdAsync(Guid teacherId)
        {
            var teacher = Teachers.FirstOrDefault(t => t.Id == teacherId);
            return Task.FromResult(teacher);
        }

        public Task<Teacher?> GetByEmailAsync(string email)
        {
            var teacher = Teachers.FirstOrDefault(t => t.Email == email);
            return Task.FromResult(teacher);
        }

        public Task<List<Teacher>> GetAllAsync()
        {
            return Task.FromResult(Teachers);
        }

        // ---------------- COURSE RELATION ----------------

        public Task AssignCourseAsync(TeacherCourseInfo info)
        {
            var exists = TeacherCourses.Any(tc =>
                tc.TeacherId == info.TeacherId &&
                tc.CourseId == info.CourseId);

            if (!exists)
            {
                TeacherCourses.Add(info);
            }

            return Task.CompletedTask;
        }

        public Task<List<TeacherCourseInfo>> GetTeacherCoursesAsync(Guid teacherId)
        {
            var list = TeacherCourses
                .Where(tc => tc.TeacherId == teacherId)
                .ToList();

            return Task.FromResult(list);
        }
    }
}
