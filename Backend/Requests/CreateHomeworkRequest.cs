namespace EducationSystemBackend.Requests
{
    public class CreateHomeworkRequest
    {
        public string OrganizationId { get; set; }

        public string CourseId { get; set; }

        public string TeacherId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }
    }
}
