namespace EducationSystemBackend.Models
{
    public class Course
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrganizationId { get; set; }
        public string CourseName { get; set; }  // Örn: Math, Physics
        public int Grade { get; set; }          // Örn: 9, 10, 11, 12
    }
}
