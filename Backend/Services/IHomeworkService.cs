using EducationSystemBackend.Models;

namespace EducationSystemBackend.Services
{
    public interface IHomeworkService
    {
        Task CreateAsync(Homework hw);
        Task<IEnumerable<Homework>> GetByTeacherAsync(Guid teacherId);
        Task<IEnumerable<Homework>> GetByStudentAsync(Guid studentId);
        Task SubmitAsync(HomeworkSubmission submission);
        Task<IEnumerable<HomeworkSubmission>> GetSubmissionsAsync(Guid homeworkId);
    }
}
