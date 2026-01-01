namespace EducationSystemBackend.Models
{
    public class Course
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid OrganizationId { get; set; }

        public required string CourseName { get; set; }

        public int Grade { get; set; }
    }
}
