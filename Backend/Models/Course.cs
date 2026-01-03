using Google.Cloud.Firestore;

namespace EducationSystemBackend.Models
{
    [FirestoreData]
    public class Course
    {
        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [FirestoreProperty]
        public string OrganizationId { get; set; }

        [FirestoreProperty]
        public required string CourseName { get; set; }

        [FirestoreProperty]
        public int Grade { get; set; }
    }
}
