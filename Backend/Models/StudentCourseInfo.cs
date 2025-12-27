namespace EducationSystemBackend.Models
{
    public class StudentCourseInfo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    }
}
