namespace EducationSystemBackend.Requests
{
    public class CreateHomeworkRequest
    {
        public Guid CourseId { get; set; }
        public Guid TeacherId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }

    public class SubmitHomeworkRequest
    {
        public Guid HomeworkId { get; set; }
        public Guid StudentId { get; set; }
        public string? Content { get; set; }
        // File handling will be handled separately in the controller via IFormFile
    }
}
