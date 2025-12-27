namespace EducationSystemBackend.Models
{
    public enum UserRole
    {
        Admin = 0,
        Teacher = 1,
        Student = 2
    }
    public class Student
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrganizationId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
        public int Grade { get; set; }
        public string Section { get; set; }
        public UserRole Role { get; set; } = UserRole.Student;
    }
}
