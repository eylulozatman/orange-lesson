using EducationSystemBackend.Models;
using EducationSystemBackend.Repositories;

namespace EducationSystemBackend.Services
{
    public class TeacherAppService : ITeacherService
    {
        private readonly IFirestoreRepository<Teacher> _teachers;
        private readonly IFirestoreRepository<TeacherCourseInfo> _assignments;
        private readonly IFirestoreRepository<Course> _courses;

        public TeacherAppService(IFirestoreRepository<Teacher> teachers,
                                 IFirestoreRepository<TeacherCourseInfo> assignments,
                                 IFirestoreRepository<Course> courses)
        {
            _teachers = teachers;
            _assignments = assignments;
            _courses = courses;
        }

        public Task<IEnumerable<Teacher>> GetByEmailAsync(string email) => _teachers.QueryAsync("Email", email);

        public Task AssignCourseAsync(TeacherCourseInfo info) => _assignments.AddAsync(info);

        public async Task<IEnumerable<Course>> GetAssignedCoursesAsync(Guid teacherId)
        {
            var assigns = await _assignments.QueryAsync("TeacherId", teacherId.ToString());
            var courseIds = assigns.Select(a => a.CourseId).Distinct();
            var all = await _courses.GetAllAsync();
            return all.Where(c => courseIds.Contains(c.Id));
        }
    }
}
