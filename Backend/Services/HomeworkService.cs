using EducationSystemBackend.Models;
using EducationSystemBackend.Repositories;

namespace EducationSystemBackend.Services
{
    public class HomeworkService
    {
        private readonly IFirestoreRepository<Homework> _hwRepo;
        private readonly IFirestoreRepository<HomeworkSubmission> _submissionRepo;

        public HomeworkService(IFirestoreRepository<Homework> hwRepo, IFirestoreRepository<HomeworkSubmission> submissionRepo)
        {
            _hwRepo = hwRepo;
            _submissionRepo = submissionRepo;
        }

        public Task AddHomeworkAsync(Homework hw) => _hwRepo.AddAsync(hw);

        public Task<IEnumerable<Homework>> GetAllAsync() => _hwRepo.GetAllAsync();

        public Task<IEnumerable<Homework>> GetByTeacherAsync(Guid teacherId) => _hwRepo.QueryAsync("TeacherId", teacherId.ToString());

        public async Task<IEnumerable<Homework>> GetByStudentAsync(Guid studentId, IFirestoreRepository<StudentCourseInfo> enrollRepo)
        {
            var enrolls = await enrollRepo.QueryAsync("StudentId", studentId.ToString());
            var courseIds = enrolls.Select(e => e.CourseId).Distinct();
            var all = await _hwRepo.GetAllAsync();
            return all.Where(h => courseIds.Contains(h.CourseId));
        }

        public Task<IEnumerable<HomeworkSubmission>> GetSubmissionsByHomeworkIdAsync(Guid homeworkId)
        {
            return _submissionRepo.QueryAsync("HomeworkId", homeworkId.ToString());
        }
    }
}
