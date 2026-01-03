namespace EducationSystemBackend.Requests
{
    public class TeacherRegisterRequest
    {
        public string OrganizationId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        // Dropdown’dan gelecek (Math, Physics, Biology vs.)
        public string CourseId { get; set; }
    }
}
