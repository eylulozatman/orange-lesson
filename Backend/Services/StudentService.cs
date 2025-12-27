using EducationSystemBackend.Models;
using EducationSystemBackend.Requests;
using EducationSystemBackend.Responses;

namespace EducationSystemBackend.Services
{
    public interface IStudentService
    {
        Task<StudentRegisterResponse> RegisterAsync(StudentRegisterRequest request);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(Guid id);
        Task EnrollAsync(Guid studentId, Guid courseId);
    }

    public class StudentService : IStudentService
    {
        private readonly Repositories.IRepository<Student> _studentRepository;
        private readonly Repositories.IRepository<StudentCourseInfo> _studentCourseRepository;

        public StudentService(
            Repositories.IRepository<Student> studentRepository,
            Repositories.IRepository<StudentCourseInfo> studentCourseRepository)
        {
            _studentRepository = studentRepository;
            _studentCourseRepository = studentCourseRepository;
        }

        public async Task<StudentRegisterResponse> RegisterAsync(StudentRegisterRequest request)
        {
            var student = new Student
            {
                OrganizationId = request.OrganizationId,
                FullName = request.FullName,
                Email = request.Email,
                Password = request.Password, // In a real app, hash this!
                City = request.City
            };

            await _studentRepository.AddAsync(student);
            await _studentRepository.SaveChangesAsync();

            return new StudentRegisterResponse
            {
                StudentId = student.Id,
                Message = "Student registered successfully."
            };
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(Guid id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task EnrollAsync(Guid studentId, Guid courseId)
        {
            var enrollment = new StudentCourseInfo
            {
                StudentId = studentId,
                CourseId = courseId
            };
            await _studentCourseRepository.AddAsync(enrollment);
            await _studentCourseRepository.SaveChangesAsync();
        }
    }
}
