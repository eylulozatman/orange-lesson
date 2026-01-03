using EducationSystemBackend.Models;
using EducationSystemBackend.Repositories;

namespace EducationSystemBackend.Services
{
    public class HomeworkService : IHomeworkService
    {
        private readonly IHomeworkRepository _hwRepo;
        private readonly IStudentRepository _studentRepo;

        public HomeworkService(IHomeworkRepository hwRepo, IStudentRepository studentRepo)
        {
            _hwRepo = hwRepo;
            _studentRepo = studentRepo;
        }

        public Task CreateAsync(Homework hw) => _hwRepo.AddAsync(hw);

        public Task<IEnumerable<Homework>> GetAllAsync() => 
            _hwRepo.GetAllAsync().ContinueWith(t => (IEnumerable<Homework>)t.Result);

        public async Task<IEnumerable<Homework>> GetByTeacherAsync(string teacherId) 
            => await _hwRepo.GetByTeacherIdAsync(teacherId);

        public async Task<IEnumerable<Homework>> GetByStudentAsync(string studentId)
        {
            var courses = await _studentRepo.GetCoursesByStudentId(studentId);
            var courseIds = courses.Select(c => c.Id).ToList();
            if(!courseIds.Any()) return new List<Homework>();

            return await _hwRepo.GetByCourseIdsAsync(courseIds);
        }

        public Task SubmitAsync(HomeworkSubmission submission) => _hwRepo.AddSubmissionAsync(submission);

        public async Task<IEnumerable<HomeworkSubmission>> GetSubmissionsAsync(string homeworkId)
        {
            return await _hwRepo.GetSubmissionsByHomeworkIdAsync(homeworkId);
        }
    }
}
