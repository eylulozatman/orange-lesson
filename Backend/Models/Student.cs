namespace EducationSystemBackend.Models
{
    public class Student
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid OrganizationId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public int Grade { get; set; }

        public string Section { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.Student;
    }
}
