using Google.Cloud.Firestore;

namespace EducationSystemBackend.Data
{
    public static class FirestoreSeeder
    {
        public static async Task SeedAsync(FirestoreDb firestore)
        {
            var orgsCollection = firestore.Collection("Organizations");
            var teachersCollection = firestore.Collection("Teachers");
            var studentsCollection = firestore.Collection("Students");

            // Check if seeding is needed (check if any organization exists)
            var snapshot = await orgsCollection.Limit(1).GetSnapshotAsync();
            if (snapshot.Count > 0)
            {
                // Data already exists
                return;
            }

            Console.WriteLine("Seeding Firestore data...");

            // Create 2 Organizations
            for (int i = 1; i <= 2; i++)
            {
                string orgId = Guid.NewGuid().ToString();
                var orgData = new Dictionary<string, object>
                {
                    { "Id", orgId },
                    { "Name", $"Gelecek Bilim Akademisi - Kampüs {i}" },
                    { "Address", i == 1 ? "İstanbul, Kadıköy" : "Ankara, Çankaya" },
                    { "IsHidden", false },
                    { "CreatedAt", DateTime.UtcNow.ToString("o") }
                };

                await orgsCollection.Document(orgId).SetAsync(orgData);
                Console.WriteLine($"Created Organization: {orgData["Name"]}");

                // Create 2 Teachers for this Org
                for (int t = 1; t <= 2; t++)
                {
                    string teacherId = Guid.NewGuid().ToString();
                    var teacherData = new Dictionary<string, object>
                    {
                        { "Id", teacherId },
                        { "FullName", $"Öğretmen {i}-{t}" },
                        { "Email", $"teacher{i}_{t}@gelecek.com" },
                        { "Password", "password" }, // In production this should be hashed
                        { "Branch", t == 1 ? "Matematik" : "Fizik" },
                        { "OrganizationId", orgId },
                        { "Role", "Teacher" },
                        { "CreatedAt", DateTime.UtcNow.ToString("o") }
                    };
                    await teachersCollection.Document(teacherId).SetAsync(teacherData);
                }

                // Create 3 Students for this Org
                for (int s = 1; s <= 3; s++)
                {
                    string studentId = Guid.NewGuid().ToString();
                    var studentData = new Dictionary<string, object>
                    {
                        { "Id", studentId },
                        { "FullName", $"Öğrenci {i}-{s}" },
                        { "Email", $"student{i}_{s}@gelecek.com" },
                        { "Password", "password" },
                        { "Grade", 10 + s }, // 11, 12, 13 (mezar?) -> 10, 11, 12 olsun
                        { "OrganizationId", orgId },
                        { "Role", "Student" },
                        { "CreatedAt", DateTime.UtcNow.ToString("o") }
                    };
                    await studentsCollection.Document(studentId).SetAsync(studentData);
                }
            }

            Console.WriteLine("Seeding completed.");
        }
    }
}
