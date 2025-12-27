using EducationSystemBackend.Models;

namespace EducationSystemBackend.Services
{
    public interface ITeacherService
    {
        Task<TeacherCourseInfo?> AssignCourseAsync(Guid teacherId, Guid courseId);
        Task<IEnumerable<Teacher>> GetAllTeachersAsync();
        Task<Teacher?> GetByEmailAsync(string email);
        Task<TeacherCourseInfo?> GetTeacherCourseAsync(Guid teacherId);
    }

    public class TeacherService : ITeacherService
    {
        private readonly Repositories.IRepository<Teacher> _teacherRepository;
        private readonly Repositories.IRepository<TeacherCourseInfo> _teacherCourseRepository;

        public TeacherService(
            Repositories.IRepository<Teacher> teacherRepository,
            Repositories.IRepository<TeacherCourseInfo> teacherCourseRepository)
        {
            _teacherRepository = teacherRepository;
            _teacherCourseRepository = teacherCourseRepository;
        }

        public async Task<TeacherCourseInfo?> AssignCourseAsync(Guid teacherId, Guid courseId)
        {
            // Enforce "one course per teacher" rule
            var existingAssignment = await _teacherCourseRepository.FindAsync(t => t.TeacherId == teacherId);
            if (existingAssignment.Any())
            {
                throw new InvalidOperationException("Teacher is already assigned to a course.");
            }

            var info = new TeacherCourseInfo
            {
                TeacherId = teacherId,
                CourseId = courseId
            };

            await _teacherCourseRepository.AddAsync(info);
            await _teacherCourseRepository.SaveChangesAsync();
            return info;
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
        {
            return await _teacherRepository.GetAllAsync();
        }

        public async Task<Teacher?> GetByEmailAsync(string email)
        {
            var teachers = await _teacherRepository.FindAsync(t => t.Email == email);
            return teachers.FirstOrDefault();
        }

        public async Task<TeacherCourseInfo?> GetTeacherCourseAsync(Guid teacherId)
        {
            var info = await _teacherCourseRepository.FindAsync(t => t.TeacherId == teacherId);
            return info.FirstOrDefault();
        }
    }
}
