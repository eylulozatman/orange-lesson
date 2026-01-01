using EducationSystemBackend.Models;
using EducationSystemBackend.Repositories;
using EducationSystemBackend.Responses;

namespace EducationSystemBackend.Services
{
public class StudentService : IStudentService
{
    private readonly IStudentRepository _students;
    private readonly ICourseRepository _courses;
    private readonly IHomeworkRepository _homeworks;

    private static readonly string[] Sections = { "A", "B", "C" };

    public StudentService(
        IStudentRepository students,
        ICourseRepository courses,
        IHomeworkRepository homeworks)
    {
        _students = students;
        _courses = courses;
        _homeworks = homeworks;
    }

    public async Task<Student> RegisterAsync(Student student)
    {
        student.Section = Sections[new Random().Next(Sections.Length)];
        await _students.AddAsync(student);
        return student;
    }

    public async Task<Student?> LoginAsync(string email, string password)
    {
        var student = await _students.GetByEmailAsync(email);
        return student?.Password == password ? student : null;
    }

    public async Task<Student?> GetByEmailAsync(string email)
        => await _students.GetByEmailAsync(email);

    public async Task<StudentDetailsResponse> GetStudentDetails(Guid studentId)
    {
        var student = await _students.GetByIdAsync(studentId)
            ?? throw new Exception("Student not found");

        var courses = await _students.GetCoursesByStudentId(studentId);
        var homeworks = await _homeworks.GetByCourseIdsAsync(courses.Select(c => c.Id).ToList());

        return new StudentDetailsResponse
        {
            Student = student,
            Courses = courses,
            Homeworks = homeworks
        };
    }

    public async Task EnrollToCourse(Guid studentId, Guid courseId)
    {
        await _students.EnrollAsync(new StudentCourseInfo
        {
            StudentId = studentId,
            CourseId = courseId
        });
    }

      public async Task SubmitHomeworkAsync(HomeworkSubmission submission)
        {
            await _homeworks.AddSubmissionAsync(submission);
        }

        public Task<List<HomeworkSubmission>> GetMySubmissionsAsync(Guid studentId)
        {
            return _homeworks.GetSubmissionsByStudentAsync(studentId);
        }

    public async Task<List<HomeworkSubmission>> GetSubmissionsByCourse(Guid studentId, Guid courseId)
        => await _homeworks.GetSubmissionsByStudentAndCourse(studentId, courseId);

        public Task<List<Student>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }

}
