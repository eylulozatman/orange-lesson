namespace EducationSystemBackend.Requests
{
    public class CreateCourseRequest
    {
        public string OrganizationId { get; set; }

        public string CourseName { get; set; } = null!;

        public int Grade { get; set; }
    }
}
