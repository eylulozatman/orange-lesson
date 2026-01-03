using EducationSystemBackend.Models;

namespace EducationSystemBackend.Services
{
    public interface IHomeworkService
    {
        Task CreateAsync(Homework hw);
        Task<IEnumerable<Homework>> GetByTeacherAsync(string teacherId);
        Task<IEnumerable<Homework>> GetByStudentAsync(string studentId);
        Task SubmitAsync(HomeworkSubmission submission);
        Task<IEnumerable<HomeworkSubmission>> GetSubmissionsAsync(string homeworkId);
    }
}
