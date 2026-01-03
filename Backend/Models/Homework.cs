using Google.Cloud.Firestore;

namespace EducationSystemBackend.Models
{
    [FirestoreData]
    public class Homework
    {
        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [FirestoreProperty]
        public string OrganizationId { get; set; }

        [FirestoreProperty]
        public string CourseId { get; set; }

        [FirestoreProperty]
        public string TeacherId { get; set; }

        [FirestoreProperty]
        public required string Title { get; set; }

        [FirestoreProperty]
        public string? Description { get; set; }

        [FirestoreProperty]
        public DateTime AssignedDate { get; set; } = DateTime.UtcNow;

        [FirestoreProperty]
        public DateTime DueDate { get; set; }
    }
}
