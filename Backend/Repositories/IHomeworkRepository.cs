using EducationSystemBackend.Models;

namespace EducationSystemBackend.Repositories
{
    public interface IHomeworkRepository
{
    Task<List<Homework>> GetByCourseIdsAsync(List<Guid> courseIds);
    Task<List<HomeworkSubmission>> GetSubmissionsByStudentAndCourse(Guid studentId, Guid courseId);
}

}
