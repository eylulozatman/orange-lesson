namespace EducationSystemBackend.Models
{
    public class HomeworkSubmission
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid HomeworkId { get; set; }
        public Guid CourseId { get; set; }   // ✅ GEREKLİ

        public Guid StudentId { get; set; }

        public string? Content { get; set; }   // Text answer
        public string? FilePath { get; set; }  // File URL / path

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
