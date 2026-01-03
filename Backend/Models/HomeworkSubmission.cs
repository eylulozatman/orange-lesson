using Google.Cloud.Firestore;

namespace EducationSystemBackend.Models
{
    [FirestoreData]
    public class HomeworkSubmission
    {
        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [FirestoreProperty]
        public string HomeworkId { get; set; }

        [FirestoreProperty]
        public string CourseId { get; set; }

        [FirestoreProperty]
        public string StudentId { get; set; }

        [FirestoreProperty]
        public string? Content { get; set; }

        [FirestoreProperty]
        public string? FilePath { get; set; }

        [FirestoreProperty]
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
