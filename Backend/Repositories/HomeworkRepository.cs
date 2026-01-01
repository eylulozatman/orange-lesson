using EducationSystemBackend.Models;

namespace EducationSystemBackend.Repositories
{
    public class HomeworkRepository : IHomeworkRepository
    {
        private static readonly List<Homework> _homeworks = new();
        private static readonly List<HomeworkSubmission> _submissions = new();

        public Task AddAsync(Homework homework)
        {
            _homeworks.Add(homework);
            return Task.CompletedTask;
        }

        public Task<List<Homework>> GetByCourseIdAsync(Guid courseId)
        {
            return Task.FromResult(
                _homeworks.Where(x => x.CourseId == courseId).ToList()
            );
        }

        public Task SubmitAsync(HomeworkSubmission submission)
        {
            _submissions.Add(submission);
            return Task.CompletedTask;
        }

        public Task<List<HomeworkSubmission>> GetSubmissionsByStudentAsync(Guid studentId)
        {
            return Task.FromResult(
                _submissions.Where(x => x.StudentId == studentId).ToList()
            );
        }

        public Task<List<HomeworkSubmission>> GetSubmissionsByCourseAsync(Guid courseId)
        {
            return Task.FromResult(
                _submissions.Where(x => x.CourseId == courseId).ToList()
            );
        }

        public Task<List<Homework>> GetByCourseIdsAsync(List<Guid> courseIds)
        {
            throw new NotImplementedException();
        }

        public Task<List<HomeworkSubmission>> GetSubmissionsByStudentAndCourse(Guid studentId, Guid courseId)
        {
            throw new NotImplementedException();
        }
    }
}
