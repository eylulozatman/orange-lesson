namespace EducationSystemBackend.Responses
{
    public class AuthResponse
    {
        public Guid UserId { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
