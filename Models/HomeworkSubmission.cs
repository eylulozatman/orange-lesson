namespace EducationSystemBackend.Models
{
    public class HomeworkSubmission
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid HomeworkId { get; set; }
        public Guid StudentId { get; set; }
        public string? Content { get; set; } // Text response
        public string? FilePath { get; set; } // Uploaded file path (doc, txt, etc.)
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
