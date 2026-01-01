using EducationSystemBackend.Models;
using EducationSystemBackend.Repositories;

namespace EducationSystemBackend.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courses;

        public CourseService(ICourseRepository courses)
        {
            _courses = courses;
        }

        // 🔹 Course oluştur
        public async Task<Course> CreateAsync(Course course)
        {
            await _courses.AddAsync(course);
            return course;
        }

        // 🔹 Organization’a ait kurslar
        public async Task<List<Course>> GetByOrganizationAsync(Guid organizationId)
        {
            return await _courses.GetByOrganizationIdAsync(organizationId);
        }

        // 🔹 Öğrencinin kayıtlı olduğu kurslar
        public async Task<List<Course>> GetByStudentAsync(Guid studentId)
        {
            return await _courses.GetByStudentIdAsync(studentId);
        }

        // 🔹 Öğretmenin verdiği kurslar
        public async Task<List<Course>> GetByTeacherAsync(Guid teacherId)
        {
            return await _courses.GetByTeacherIdAsync(teacherId);
        }

        // 🔹 Course detay
        public async Task<Course?> GetByIdAsync(Guid courseId)
        {
            return await _courses.GetByIdAsync(courseId);
        }

        // 🔹 Frontend için: courseName → courseId
        public async Task<Guid?> GetCourseIdByNameAsync(Guid organizationId, string courseName)
        {
            var course = await _courses.GetByNameAsync(organizationId, courseName);
            return course?.Id;
        }

        // 🔹 Öğrenciyi derse kaydet
        public async Task EnrollStudentAsync(Guid studentId, Guid courseId)
        {
            await _courses.EnrollStudentAsync(studentId, courseId);
        }
    }
}
