using EducationSystemBackend.Models;
using EducationSystemBackend.Repositories;
using EducationSystemBackend.Requests;

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
        public async Task<Course> CreateAsync(CreateCourseRequest request)
        {
            var course = new Course
            {
                Id = Guid.NewGuid().ToString(),
                OrganizationId = request.OrganizationId,
                CourseName = request.CourseName,
                Grade = request.Grade
            };

            await _courses.AddAsync(course);
            return course;
        }

        // 🔹 Organization’a ait kurslar
        public async Task<List<Course>> GetByOrganizationAsync(string organizationId)
        {
            return await _courses.GetByOrganizationIdAsync(organizationId);
        }

        // 🔹 Öğrencinin kayıtlı olduğu kurslar
        public async Task<List<Course>> GetByStudentAsync(string studentId)
        {
            return await _courses.GetByStudentIdAsync(studentId);
        }

        // 🔹 Öğretmenin verdiği kurslar
        public async Task<List<Course>> GetByTeacherAsync(string teacherId)
        {
            return await _courses.GetByTeacherIdAsync(teacherId);
        }

        // 🔹 Course detay
        public async Task<Course?> GetByIdAsync(string courseId)
        {
            return await _courses.GetByIdAsync(courseId);
        }
    
        // 🔹 Frontend için: courseName → courseId
        public async Task<string?> GetCourseIdByNameAsync(string organizationId, string courseName)
        {
            var course = await _courses.GetByNameAsync(organizationId, courseName);
            return course?.Id;
        }

        // 🔹 Öğrenciyi derse kaydet
        public async Task EnrollStudentAsync(string studentId, string courseId)
        {
            await _courses.EnrollStudentAsync(studentId, courseId);
        }
    }
}
