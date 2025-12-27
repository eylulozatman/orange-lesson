namespace EducationSystemBackend.Requests
{
    public class StudentRegisterRequest
    {
        public Guid OrganizationId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
    }
}
