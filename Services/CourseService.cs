using EducationSystemBackend.Models;

namespace EducationSystemBackend.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task AddCourseAsync(Course course);
    }

    public class CourseService : ICourseService
    {
        private readonly Repositories.IRepository<Course> _courseRepository;

        public CourseService(Repositories.IRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _courseRepository.GetAllAsync();
        }

        public async Task AddCourseAsync(Course course)
        {
            await _courseRepository.AddAsync(course);
            await _courseRepository.SaveChangesAsync();
        }
    }
}
