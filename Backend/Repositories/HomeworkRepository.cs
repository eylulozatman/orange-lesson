using EducationSystemBackend.Models;

namespace EducationSystemBackend.Repositories
{
    public class HomeworkRepository : IHomeworkRepository
    {
        public static readonly List<Homework> Homeworks = new();
        public static readonly List<HomeworkSubmission> Submissions = new();

        public Task AddAsync(Homework homework)
        {
            Homeworks.Add(homework);
            return Task.CompletedTask;
        }

        public Task<Homework?> GetByIdAsync(Guid homeworkId)
        {
            return Task.FromResult(
                Homeworks.FirstOrDefault(h => h.Id == homeworkId)
            );
        }

        public Task<List<Homework>> GetByCourseIdAsync(Guid courseId)
        {
            return Task.FromResult(
                Homeworks.Where(h => h.CourseId == courseId).ToList()
            );
        }

        public Task<List<Homework>> GetByTeacherIdAsync(Guid teacherId)
        {
            return Task.FromResult(
                Homeworks.Where(h => h.TeacherId == teacherId).ToList()
            );
        }

        public Task AddSubmissionAsync(HomeworkSubmission submission)
        {
            Submissions.Add(submission);
            return Task.CompletedTask;
        }

        public Task<List<HomeworkSubmission>> GetSubmissionsByHomeworkIdAsync(Guid homeworkId)
        {
            return Task.FromResult(
                Submissions.Where(s => s.HomeworkId == homeworkId).ToList()
            );
        }

        public Task<List<HomeworkSubmission>> GetSubmissionsByStudentAsync(Guid studentId)
        {
            return Task.FromResult(
                Submissions.Where(s => s.StudentId == studentId).ToList()
            );
        }
    }
}
