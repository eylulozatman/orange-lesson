namespace EducationSystemBackend.Requests
{
    public class StudentRegisterRequest
    {
        public string OrganizationId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int Grade { get; set; }
    }
}
