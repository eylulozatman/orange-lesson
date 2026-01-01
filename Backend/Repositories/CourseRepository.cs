using EducationSystemBackend.Models;

using EducationSystemBackend.Models;

namespace EducationSystemBackend.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        // ⚠️ In-memory storage
        public static readonly List<Course> Courses = new();
        public static readonly List<StudentCourseInfo> StudentCourses = new();
        public static readonly List<TeacherCourseInfo> TeacherCourses = new();

        // ---------------- COURSES ----------------

        public Task AddAsync(Course course)
        {
            Courses.Add(course);
            return Task.CompletedTask;
        }

        public Task<Course?> GetByIdAsync(Guid courseId)
        {
            var course = Courses.FirstOrDefault(c => c.Id == courseId);
            return Task.FromResult(course);
        }

        public Task<Course?> GetByNameAsync(Guid organizationId, string courseName)
        {
            var course = Courses.FirstOrDefault(c =>
                c.OrganizationId == organizationId &&
                c.CourseName == courseName);

            return Task.FromResult(course);
        }

        public Task<List<Course>> GetByOrganizationIdAsync(Guid organizationId)
        {
            var list = Courses
                .Where(c => c.OrganizationId == organizationId)
                .ToList();

            return Task.FromResult(list);
        }

        // ---------------- STUDENT ----------------

        public Task<List<Course>> GetByStudentIdAsync(Guid studentId)
        {
            var courseIds = StudentCourses
                .Where(sc => sc.StudentId == studentId)
                .Select(sc => sc.CourseId)
                .ToList();

            var courses = Courses
                .Where(c => courseIds.Contains(c.Id))
                .ToList();

            return Task.FromResult(courses);
        }

        public Task EnrollStudentAsync(Guid studentId, Guid courseId)
        {
            var alreadyEnrolled = StudentCourses.Any(sc =>
                sc.StudentId == studentId && sc.CourseId == courseId);

            if (!alreadyEnrolled)
            {
                StudentCourses.Add(new StudentCourseInfo
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    EnrolledAt = DateTime.UtcNow
                });
            }

            return Task.CompletedTask;
        }

        // ---------------- TEACHER ----------------

        public Task<List<Course>> GetByTeacherIdAsync(Guid teacherId)
        {
            var courseIds = TeacherCourses
                .Where(tc => tc.TeacherId == teacherId)
                .Select(tc => tc.CourseId)
                .ToList();

            var courses = Courses
                .Where(c => courseIds.Contains(c.Id))
                .ToList();

            return Task.FromResult(courses);
        }
    }
}
