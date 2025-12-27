namespace EducationSystemBackend.Models
{
    public class Homework
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrganizationId { get; set; }

        public Guid CourseId { get; set; }

        public Guid TeacherId { get; set; }   // Ödevi veren öğretmen

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime AssignedDate { get; set; } = DateTime.UtcNow;

        public DateTime DueDate { get; set; }


    }
}
