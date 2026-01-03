using Google.Cloud.Firestore;
using EducationSystemBackend.Models;
using FirebaseAdmin;
namespace EducationSystemBackend.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly FirestoreDb _firestoreDb;
        private const string CollectionName = "Students";
        private const string CourseInfosCollection = "StudentCourseInfos";
        private const string CoursesCollection = "Courses";

        public StudentRepository(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public async Task AddAsync(Student student)
        {
            var docRef = _firestoreDb.Collection(CollectionName).Document(student.Id);
            await docRef.SetAsync(student);
        }

        public async Task<Student?> GetByIdAsync(string id)
        {
            var doc = await _firestoreDb.Collection(CollectionName).Document(id).GetSnapshotAsync();
            return doc.Exists ? doc.ConvertTo<Student>() : null;
        }

        public async Task<Student?> GetByEmailAsync(string email)
        {
            var query = _firestoreDb.Collection(CollectionName).WhereEqualTo("Email", email);
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.Documents.Select(d => d.ConvertTo<Student>()).FirstOrDefault();
        }

        public async Task<List<Student>> GetAllAsync()
        {
            var snapshot = await _firestoreDb.Collection(CollectionName).GetSnapshotAsync();
            return snapshot.Documents.Select(d => d.ConvertTo<Student>()).ToList();
        }

        public async Task EnrollAsync(StudentCourseInfo info)
        {
             var docRef = _firestoreDb.Collection(CourseInfosCollection).Document(info.Id);
             await docRef.SetAsync(info);
        }

        public async Task<List<Course>> GetCoursesByStudentId(string studentId)
        {
            // 1. Get Course IDs from StudentCourseInfos
            var infoQuery = _firestoreDb.Collection(CourseInfosCollection).WhereEqualTo("StudentId", studentId);
            var infoSnapshot = await infoQuery.GetSnapshotAsync();
            
            if(infoSnapshot.Count == 0) return new List<Course>();

            var courseIds = infoSnapshot.Documents.Select(d => d.ConvertTo<StudentCourseInfo>().CourseId).ToList();
            
            // 2. Fetch Courses
            var courses = new List<Course>();
            foreach(var cId in courseIds)
            {
                var cDoc = await _firestoreDb.Collection(CoursesCollection).Document(cId).GetSnapshotAsync();
                if(cDoc.Exists) courses.Add(cDoc.ConvertTo<Course>());
            }
            return courses;
        }
    }
}
