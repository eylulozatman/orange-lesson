using EducationSystemBackend.Models;
namespace EducationSystemBackend.Responses;


public class StudentDetailsResponse
{
    public Student Student { get; set; }
    public List<Course> Courses { get; set; } = new();
    public List<Homework> Homeworks { get; set; } = new();
}

