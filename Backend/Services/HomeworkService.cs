using EducationSystemBackend.Models;
using EducationSystemBackend.Requests;

namespace EducationSystemBackend.Services
{
    public interface IHomeworkService
    {
        Task<Homework> CreateHomeworkAsync(CreateHomeworkRequest request);
        Task<IEnumerable<Homework>> GetHomeworksByCourseAsync(Guid courseId);
        Task<IEnumerable<Homework>> GetHomeworksForStudentAsync(Guid studentId);
        Task<IEnumerable<Homework>> GetHomeworksByTeacherAsync(Guid teacherId);
        Task<HomeworkSubmission> SubmitHomeworkAsync(SubmitHomeworkRequest request, string? filePath);
        Task<bool> DeleteSubmissionAsync(Guid submissionId, Guid studentId);
        Task<IEnumerable<HomeworkSubmission>> GetSubmissionsByHomeworkIdAsync(Guid homeworkId);
    }

    public class HomeworkService : IHomeworkService
    {
        private readonly Repositories.IRepository<Homework> _homeworkRepository;
        private readonly Repositories.IRepository<HomeworkSubmission> _submissionRepository;
        private readonly Repositories.IRepository<StudentCourseInfo> _studentCourseRepository;
        private readonly Repositories.IRepository<TeacherCourseInfo> _teacherCourseRepository;

        public HomeworkService(
            Repositories.IRepository<Homework> homeworkRepository,
            Repositories.IRepository<HomeworkSubmission> submissionRepository,
            Repositories.IRepository<StudentCourseInfo> studentCourseRepository,
            Repositories.IRepository<TeacherCourseInfo> teacherCourseRepository)
        {
            _homeworkRepository = homeworkRepository;
            _submissionRepository = submissionRepository;
            _studentCourseRepository = studentCourseRepository;
            _teacherCourseRepository = teacherCourseRepository;
        }

        public async Task<Homework> CreateHomeworkAsync(CreateHomeworkRequest request)
        {
            var homework = new Homework
            {
                CourseId = request.CourseId,
                TeacherId = request.TeacherId,
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate
            };

            await _homeworkRepository.AddAsync(homework);
            await _homeworkRepository.SaveChangesAsync();
            return homework;
        }

        public async Task<IEnumerable<Homework>> GetHomeworksByCourseAsync(Guid courseId)
        {
            return await _homeworkRepository.FindAsync(h => h.CourseId == courseId);
        }

        public async Task<IEnumerable<Homework>> GetHomeworksForStudentAsync(Guid studentId)
        {
            var enrollments = await _studentCourseRepository.FindAsync(e => e.StudentId == studentId);
            var courseIds = enrollments.Select(e => e.CourseId).ToList();
            
            return await _homeworkRepository.FindAsync(h => courseIds.Contains(h.CourseId));
        }

        public async Task<IEnumerable<Homework>> GetHomeworksByTeacherAsync(Guid teacherId)
        {
            return await _homeworkRepository.FindAsync(h => h.TeacherId == teacherId);
        }

        public async Task<HomeworkSubmission> SubmitHomeworkAsync(SubmitHomeworkRequest request, string? filePath)
        {
            // Check if student already submitted to this homework
            var existing = await _submissionRepository.FindAsync(s => s.HomeworkId == request.HomeworkId && s.StudentId == request.StudentId);
            if (existing.Any())
            {
                // Logic could be to update or reject. Here we delete old one if student pushes new one
                foreach (var sub in existing)
                {
                    _submissionRepository.Remove(sub);
                }
            }

            var submission = new HomeworkSubmission
            {
                HomeworkId = request.HomeworkId,
                StudentId = request.StudentId,
                Content = request.Content,
                FilePath = filePath
            };

            await _submissionRepository.AddAsync(submission);
            await _submissionRepository.SaveChangesAsync();
            return submission;
        }

        public async Task<bool> DeleteSubmissionAsync(Guid submissionId, Guid studentId)
        {
            var submission = await _submissionRepository.GetByIdAsync(submissionId);
            if (submission == null || submission.StudentId != studentId) return false;

            _submissionRepository.Remove(submission);
            return await _submissionRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<HomeworkSubmission>> GetSubmissionsByHomeworkIdAsync(Guid homeworkId)
        {
            return await _submissionRepository.FindAsync(s => s.HomeworkId == homeworkId);
        }
    }
}
