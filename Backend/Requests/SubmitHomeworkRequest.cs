namespace EducationSystemBackend.Requests
{
    public class SubmitHomeworkRequest
    {
        public Guid HomeworkId { get; set; }

        public Guid CourseId { get; set; }

        public Guid StudentId { get; set; }

        public string? Content { get; set; }
    }
}
