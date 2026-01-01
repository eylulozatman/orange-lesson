namespace EducationSystemBackend.Requests
{
    public class CreateCourseRequest
    {
        public Guid OrganizationId { get; set; }

        public string CourseName { get; set; } = null!;

        public int Grade { get; set; }
    }
}
