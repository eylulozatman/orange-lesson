using EducationSystemBackend.Models;

namespace EducationSystemBackend.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private static readonly List<Student> _students = new();
        private static readonly List<StudentCourseInfo> _studentCourses = new();

        public Task AddAsync(Student student)
        {
            _students.Add(student);
            return Task.CompletedTask;
        }

        public Task<Student?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_students.FirstOrDefault(x => x.Id == id));
        }

        public Task<Student?> GetByEmailAsync(string email)
        {
            return Task.FromResult(
                _students.FirstOrDefault(x => x.Email == email)
            );
        }

        public Task<List<Student>> GetAllAsync()
        {
            return Task.FromResult(_students);
        }

        public Task EnrollToCourseAsync(Guid studentId, Guid courseId)
        {
            _studentCourses.Add(new StudentCourseInfo
            {
                StudentId = studentId,
                CourseId = courseId
            });

            return Task.CompletedTask;
        }

        public Task<List<Course>> GetCoursesByStudentIdAsync(Guid studentId)
        {
            var courseIds = _studentCourses
                .Where(x => x.StudentId == studentId)
                .Select(x => x.CourseId)
                .ToList();

            var courses = CourseRepository.Courses
                .Where(c => courseIds.Contains(c.Id))
                .ToList();

            return Task.FromResult(courses);
        }

        public Task EnrollAsync(StudentCourseInfo info)
        {
            throw new NotImplementedException();
        }

        public Task<List<Course>> GetCoursesByStudentId(Guid studentId)
        {
            throw new NotImplementedException();
        }
    }
}
