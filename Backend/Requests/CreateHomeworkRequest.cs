namespace EducationSystemBackend.Requests
{
    public class CreateHomeworkRequest
    {
        public Guid OrganizationId { get; set; }

        public Guid CourseId { get; set; }

        public Guid TeacherId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }
    }
}
