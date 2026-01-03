using Google.Cloud.Firestore;

namespace EducationSystemBackend.Models
{
    [FirestoreData]
    public class Student
    {
        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [FirestoreProperty]
        public string OrganizationId { get; set; }

        [FirestoreProperty]
        public string FullName { get; set; } = string.Empty;

        [FirestoreProperty]
        public string Email { get; set; } = string.Empty;

        [FirestoreProperty]
        public string City { get; set; } = string.Empty;

        [FirestoreProperty]
        public string Password { get; set; } = string.Empty;

        [FirestoreProperty]
        public int Grade { get; set; }

        [FirestoreProperty]
        public string Section { get; set; } = string.Empty;

        [FirestoreProperty]
        public UserRole Role { get; set; } = UserRole.Student;
    }
}
