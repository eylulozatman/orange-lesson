using Google.Cloud.Firestore;

namespace EducationSystemBackend.Models
{
    [FirestoreData]
    public class Organization
    {
        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [FirestoreProperty]
        public string Name { get; set; } = null!;

        [FirestoreProperty]
        public string? Address { get; set; }

        [FirestoreProperty]
        public bool IsHidden { get; set; }
    }
}
