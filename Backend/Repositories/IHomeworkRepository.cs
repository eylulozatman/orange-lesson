using EducationSystemBackend.Models;

namespace EducationSystemBackend.Repositories
{
    public interface IHomeworkRepository
    {
        Task AddAsync(Homework homework);

        Task<Homework?> GetByIdAsync(Guid homeworkId);

        Task<List<Homework>> GetByCourseIdAsync(Guid courseId);

        Task<List<Homework>> GetByTeacherIdAsync(Guid teacherId);

        Task AddSubmissionAsync(HomeworkSubmission submission);

        Task<List<HomeworkSubmission>> GetSubmissionsByHomeworkIdAsync(Guid homeworkId);

        Task<List<HomeworkSubmission>> GetSubmissionsByStudentAsync(Guid studentId);
    }
}
