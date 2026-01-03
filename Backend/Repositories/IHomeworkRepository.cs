using EducationSystemBackend.Models;

namespace EducationSystemBackend.Repositories
{
    public interface IHomeworkRepository
    {
        Task AddAsync(Homework homework);
        Task<List<Homework>> GetAllAsync();

        Task<Homework?> GetByIdAsync(string homeworkId);

        Task<List<Homework>> GetByCourseIdAsync(string courseId);

        Task<List<Homework>> GetByTeacherIdAsync(string teacherId);

        Task<List<Homework>> GetByCourseIdsAsync(List<string> courseIds);

        Task AddSubmissionAsync(HomeworkSubmission submission);

        Task<List<HomeworkSubmission>> GetSubmissionsByHomeworkIdAsync(string homeworkId);

        Task<List<HomeworkSubmission>> GetSubmissionsByStudentAsync(string studentId);
        
        Task<List<HomeworkSubmission>> GetSubmissionsByStudentAndCourse(string studentId, string courseId);
    }
}
