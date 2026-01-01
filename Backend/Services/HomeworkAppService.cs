using EducationSystemBackend.Models;
using EducationSystemBackend.Repositories;

namespace EducationSystemBackend.Services
{
    public class HomeworkAppService : IHomeworkService
    {
        private readonly IFirestoreRepository<Homework> _hw;
        private readonly IFirestoreRepository<HomeworkSubmission> _subs;
        private readonly IFirestoreRepository<StudentCourseInfo> _enrolls;

        public HomeworkAppService(IFirestoreRepository<Homework> hw,
                                  IFirestoreRepository<HomeworkSubmission> subs,
                                  IFirestoreRepository<StudentCourseInfo> enrolls)
        {
            _hw = hw;
            _subs = subs;
            _enrolls = enrolls;
        }

        public Task CreateAsync(Homework hw) => _hw.AddAsync(hw);

        public Task<IEnumerable<Homework>> GetByTeacherAsync(Guid teacherId) => _hw.QueryAsync("TeacherId", teacherId.ToString());

        public async Task<IEnumerable<Homework>> GetByStudentAsync(Guid studentId)
        {
            var enrolls = await _enrolls.QueryAsync("StudentId", studentId.ToString());
            var courseIds = enrolls.Select(e => e.CourseId).Distinct();
            var all = await _hw.GetAllAsync();
            return all.Where(h => courseIds.Contains(h.CourseId));
        }

        public Task SubmitAsync(HomeworkSubmission submission) => _subs.AddAsync(submission);

        public Task<IEnumerable<HomeworkSubmission>> GetSubmissionsAsync(Guid homeworkId) => _subs.QueryAsync("HomeworkId", homeworkId.ToString());
    }
}
