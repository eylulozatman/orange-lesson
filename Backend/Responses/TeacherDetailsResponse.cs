using EducationSystemBackend.Models;

namespace EducationSystemBackend.Responses;
public class TeacherDetailsResponse
{
    public Teacher Teacher { get; set; }
    public List<Course> Courses { get; set; } = new();
}
