namespace EducationSystemBackend.Models
{
    public class TeacherCourseInfo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
    }
}
