using Google.Cloud.Firestore;
using EducationSystemBackend.Models;

namespace EducationSystemBackend.Data
{
    public static class DbInitializer
    {
        public static async Task ClearDatabaseAsync(FirestoreDb db)
        {
            Console.WriteLine("🧹 CLEAR DATABASE STARTED");

            string[] collections =
            {
                "Organizations",
                "Teachers",
                "Students",
                "Courses",
                "TeacherCourseInfos",
                "StudentCourseInfos",
                "Homeworks",
                "HomeworkSubmissions"
            };

            foreach (var col in collections)
            {
                var snapshot = await db.Collection(col).GetSnapshotAsync();
                foreach (var doc in snapshot.Documents)
                    await doc.Reference.DeleteAsync();

                Console.WriteLine($"🗑 {col} cleared");
            }

            Console.WriteLine("✅ CLEAR DATABASE COMPLETED");
        }

        public static async Task SeedAsync(FirestoreDb db)
        {
            Console.WriteLine("🌱 SEEDING STARTED");

            // Admin varsa tekrar seed etme
            var teacherCheck = await db.Collection("Teachers")
                .WhereEqualTo("Email", "eylul@ozatman.com")
                .GetSnapshotAsync();

            if (teacherCheck.Count > 0)
            {
                Console.WriteLine("⚠️ Seed skipped (admin already exists)");
                return;
            }

            /* =======================
             * ORGANIZATIONS
             * ======================= */
            var org1 = new Organization
            {
                Id = "11111111-1111-1111-1111-111111111111",
                Name = "OrangeLesson",
                IsHidden = true
            };

            var org2 = new Organization
            {
                Id = "22222222-2222-2222-2222-222222222222",
                Name = "BlueSchool",
                Address = "Istanbul"
            };

            var org3 = new Organization
            {
                Id = "33333333-3333-3333-3333-333333333333",
                Name = "GreenAcademy",
                Address = "Ankara"
            };

            await db.Collection("Organizations").Document(org1.Id).SetAsync(org1);
            await db.Collection("Organizations").Document(org2.Id).SetAsync(org2);
            await db.Collection("Organizations").Document(org3.Id).SetAsync(org3);

            Console.WriteLine("🏢 Organizations seeded");

            /* =======================
             * TEACHERS
             * ======================= */
            var teachers = new List<Teacher>
            {
                new Teacher
                {
                    Id = "99999999-9999-9999-9999-999999999999",
                    FullName = "Eylül Özatman",
                    Email = "eylul@ozatman.com",
                    Password = "admin",
                    City = "Istanbul",
                    OrganizationId = org1.Id,
                    Role = UserRole.Admin
                },
                new Teacher
                {
                    Id = "88888888-8888-8888-8888-888888888888",
                    FullName = "Ali Yılmaz",
                    Email = "ali@blueschool.com",
                    Password = "123",
                    City = "Istanbul",
                    OrganizationId = org2.Id
                },
                new Teacher
                {
                    Id = "77777777-7777-7777-7777-777777777777",
                    FullName = "Ayşe Demir",
                    Email = "ayse@blueschool.com",
                    Password = "123",
                    City = "Istanbul",
                    OrganizationId = org2.Id
                },
                new Teacher
                {
                    Id = "77777777-7777-7777-7777-777777777778",
                    FullName = "Mehmet Kaya",
                    Email = "mehmet@green.com",
                    Password = "123",
                    City = "Ankara",
                    OrganizationId = org3.Id
                }
            };

            foreach (var t in teachers)
                await db.Collection("Teachers").Document(t.Id).SetAsync(t);

            Console.WriteLine("👩‍🏫 Teachers seeded");

            /* =======================
             * STUDENTS
             * ======================= */
            var students = new List<Student>
            {
                new Student
                {
                    Id = "66666666-6666-6666-6666-666666666666",
                    FullName = "Can Yıldız",
                    Email = "can@blueschool.com",
                    Password = "123",
                    City = "Istanbul",
                    Grade = 10,
                    OrganizationId = org2.Id
                },
                new Student
                {
                    Id = "55555555-5555-5555-5555-555555555555",
                    FullName = "Elif Su",
                    Email = "elif@blueschool.com",
                    Password = "123",
                    City = "Istanbul",
                    Grade = 11,
                    OrganizationId = org2.Id
                },
                new Student
                {
                    Id = "44444444-4444-4444-4444-444444444445",
                    FullName = "Selin Ak",
                    Email = "selin@green.com",
                    Password = "123",
                    City = "Ankara",
                    Grade = 9,
                    OrganizationId = org3.Id
                }
            };

            foreach (var s in students)
                await db.Collection("Students").Document(s.Id).SetAsync(s);

            Console.WriteLine("🎓 Students seeded");

            /* =======================
             * COURSES
             * ======================= */
            var course1 = new Course
            {
                Id = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                CourseName = "Math 101",
                Grade = 10,
                OrganizationId = org2.Id
            };

            var course2 = new Course
            {
                Id = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                CourseName = "Physics 101",
                Grade = 11,
                OrganizationId = org2.Id
            };

            var course3 = new Course
            {
                Id = "cccccccc-cccc-cccc-cccc-cccccccccccc",
                CourseName = "Biology 101",
                Grade = 9,
                OrganizationId = org3.Id
            };

            await db.Collection("Courses").Document(course1.Id).SetAsync(course1);
            await db.Collection("Courses").Document(course2.Id).SetAsync(course2);
            await db.Collection("Courses").Document(course3.Id).SetAsync(course3);

            Console.WriteLine("📚 Courses seeded");

            /* =======================
             * TEACHER ↔ COURSE
             * ======================= */
            var teacherCourses = new List<TeacherCourseInfo>
            {
                new TeacherCourseInfo
                {
                    Id = Guid.NewGuid().ToString(),
                    TeacherId = teachers[1].Id,
                    CourseId = course1.Id
                },
                new TeacherCourseInfo
                {
                    Id = Guid.NewGuid().ToString(),
                    TeacherId = teachers[2].Id,
                    CourseId = course2.Id
                },
                new TeacherCourseInfo
                {
                    Id = Guid.NewGuid().ToString(),
                    TeacherId = teachers[3].Id,
                    CourseId = course3.Id
                }
            };

            foreach (var tc in teacherCourses)
                await db.Collection("TeacherCourseInfos").Document(tc.Id).SetAsync(tc);

            Console.WriteLine("🔗 TeacherCourseInfos seeded");

            /* =======================
             * STUDENT ↔ COURSE
             * ======================= */
            var studentCourses = new List<StudentCourseInfo>
            {
                new StudentCourseInfo
                {
                    Id = Guid.NewGuid().ToString(),
                    StudentId = students[0].Id,
                    CourseId = course1.Id
                },
                new StudentCourseInfo
                {
                    Id = Guid.NewGuid().ToString(),
                    StudentId = students[1].Id,
                    CourseId = course2.Id
                },
                new StudentCourseInfo
                {
                    Id = Guid.NewGuid().ToString(),
                    StudentId = students[2].Id,
                    CourseId = course3.Id
                }
            };

            foreach (var sc in studentCourses)
                await db.Collection("StudentCourseInfos").Document(sc.Id).SetAsync(sc);

            Console.WriteLine("🎉 SEEDING COMPLETED");
        }
    }
}
