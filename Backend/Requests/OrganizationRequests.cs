namespace EducationSystemBackend.Requests
{
    public class CreateOrganizationRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
    }
}
