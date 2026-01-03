using Google.Cloud.Firestore;

namespace EducationSystemBackend.Models
{
    [FirestoreData]
    public class StudentCourseInfo
    {
        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [FirestoreProperty]
        public string StudentId { get; set; }

        [FirestoreProperty]
        public string CourseId { get; set; }

        [FirestoreProperty]
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    }
}
