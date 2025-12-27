namespace EducationSystemBackend.Models
{
    public class Teacher
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrganizationId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public UserRole Role { get; set; } = UserRole.Teacher;
    }
}
