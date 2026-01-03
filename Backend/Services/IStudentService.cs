using EducationSystemBackend.Models;
using EducationSystemBackend.Responses; 

namespace EducationSystemBackend.Services
{
    public interface IStudentService
    {
        Task<Student> RegisterAsync(Student student);
        Task<Student?> LoginAsync(string email, string password);
        Task<StudentDetailsResponse> GetStudentDetails(string studentId);
        Task EnrollToCourse(string studentId, string courseId);
        Task SubmitHomeworkAsync(HomeworkSubmission submission);
        Task<List<HomeworkSubmission>> GetMySubmissionsAsync(string studentId);
        Task<List<HomeworkSubmission>> GetSubmissionsByCourse(string studentId, string courseId);
    }
}
