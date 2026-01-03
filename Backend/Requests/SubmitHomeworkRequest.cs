namespace EducationSystemBackend.Requests
{
    public class SubmitHomeworkRequest
    {
        public string HomeworkId { get; set; }

        public string CourseId { get; set; }

        public string StudentId { get; set; }

        public string? Content { get; set; }
    }
}
