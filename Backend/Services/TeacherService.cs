using EducationSystemBackend.Models;
using EducationSystemBackend.Repositories;
using EducationSystemBackend.Responses;

namespace EducationSystemBackend.Services
{
   public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _teachers;

    public TeacherService(ITeacherRepository teachers)
    {
        _teachers = teachers;
    }

    public async Task<Teacher> RegisterAsync(Teacher teacher, Guid courseId)
    {
        await _teachers.AddAsync(teacher);

        await _teachers.AssignCourseAsync(new TeacherCourseInfo
        {
            TeacherId = teacher.Id,
            CourseId = courseId
        });

        return teacher;
    }

    public async Task<Teacher?> LoginAsync(string email, string password)
    {
        var teacher = await _teachers.GetByEmailAsync(email);
        return teacher?.Password == password ? teacher : null;
    }

    public async Task<TeacherDetailsResponse> GetTeacherDetails(Guid teacherId)
    {
        var teacher = await _teachers.GetByIdAsync(teacherId)
            ?? throw new Exception("Teacher not found");

        var courses = await _teachers.GetCoursesByTeacherId(teacherId);

        return new TeacherDetailsResponse
        {
            Teacher = teacher,
            Courses = courses
        };
    }
}

}
