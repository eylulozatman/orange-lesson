namespace EducationSystemBackend.Models
{
    public class Teacher
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid OrganizationId { get; set; }

        public required string FullName { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public required string City { get; set; }

        public UserRole Role { get; set; } = UserRole.Teacher;
    }
}
