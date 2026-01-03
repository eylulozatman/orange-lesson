using Google.Cloud.Firestore;

namespace EducationSystemBackend.Models
{
    [FirestoreData]
    public class TeacherCourseInfo
    {
        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [FirestoreProperty]
        public string TeacherId { get; set; }

        [FirestoreProperty]
        public string CourseId { get; set; }
    }
}
