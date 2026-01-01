using Google.Cloud.Firestore;
using EducationSystemBackend.Models;


namespace EducationSystemBackend.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private static readonly List<Teacher> _teachers = new();
        private static readonly List<TeacherCourseInfo> _teacherCourses = new();

        public Task AddAsync(Teacher teacher)
        {
            _teachers.Add(teacher);
            return Task.CompletedTask;
        }

        public Task<Teacher?> GetByEmailAsync(string email)
        {
            return Task.FromResult(
                _teachers.FirstOrDefault(x => x.Email == email)
            );
        }

        public Task AssignCourseAsync(TeacherCourseInfo info)
        {
            _teacherCourses.Add(info);
            return Task.CompletedTask;
        }

        public Task<List<Course>> GetCoursesByTeacherIdAsync(Guid teacherId)
        {
            var courseIds = _teacherCourses
                .Where(x => x.TeacherId == teacherId)
                .Select(x => x.CourseId)
                .ToList();

            var courses = CourseRepository.Courses
                .Where(c => courseIds.Contains(c.Id))
                .ToList();

            return Task.FromResult(courses);
        }

        public Task<Teacher?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Course>> GetCoursesByTeacherId(Guid teacherId)
        {
            throw new NotImplementedException();
        }
    }
}
