using Google.Cloud.Firestore;

namespace EducationSystemBackend.Models
{
    [FirestoreData]
    public class Teacher
    {
        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [FirestoreProperty]
        public string OrganizationId { get; set; }

        [FirestoreProperty]
        public required string FullName { get; set; }

        [FirestoreProperty]
        public required string Email { get; set; }

        [FirestoreProperty]
        public required string Password { get; set; }

        [FirestoreProperty]
        public required string City { get; set; }

        [FirestoreProperty]
        public UserRole Role { get; set; } = UserRole.Teacher;
    }
}
