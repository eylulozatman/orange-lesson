using Google.Cloud.Firestore;
using EducationSystemBackend.Models;
using FirebaseAdmin;
namespace EducationSystemBackend.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly FirestoreDb _firestoreDb;
        private const string CollectionName = "Courses";
        private const string StudentInfosCollection = "StudentCourseInfos";

        public CourseRepository(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public async Task AddAsync(Course course)
        {
            var docRef = _firestoreDb.Collection(CollectionName).Document(course.Id);
            await docRef.SetAsync(course);
        }

        public async Task<Course?> GetByIdAsync(string id)
        {
            var doc = await _firestoreDb.Collection(CollectionName).Document(id).GetSnapshotAsync();
            return doc.Exists ? doc.ConvertTo<Course>() : null;
        }

        public async Task<Course?> GetByNameAsync(string organizationId, string courseName)
        {
            var query = _firestoreDb.Collection(CollectionName)
                .WhereEqualTo("OrganizationId", organizationId)
                .WhereEqualTo("Name", courseName);
            
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.Documents.Select(d => d.ConvertTo<Course>()).FirstOrDefault();
        }

        public async Task<List<Course>> GetByOrganizationIdAsync(string organizationId)
        {
            var query = _firestoreDb.Collection(CollectionName).WhereEqualTo("OrganizationId", organizationId);
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.Documents.Select(d => d.ConvertTo<Course>()).ToList();
        }

        public async Task<List<Course>> GetByStudentIdAsync(string studentId)
        {
            // 1. Get Enrollments
            var enrollQuery = _firestoreDb.Collection(StudentInfosCollection).WhereEqualTo("StudentId", studentId);
            var enrollSnap = await enrollQuery.GetSnapshotAsync();
            
            if(enrollSnap.Count == 0) return new List<Course>();

            var courseIds = enrollSnap.Documents
                .Select(d => d.ConvertTo<StudentCourseInfo>().CourseId)
                .ToList();

            // 2. Get Courses
            var courses = new List<Course>();
            foreach(var cId in courseIds)
            {
                var cDoc = await _firestoreDb.Collection(CollectionName).Document(cId).GetSnapshotAsync();
                if(cDoc.Exists) courses.Add(cDoc.ConvertTo<Course>());
            }
            return courses;
        }

        public async Task<List<Course>> GetByTeacherIdAsync(string teacherId)
        {
            var query = _firestoreDb.Collection(CollectionName).WhereEqualTo("TeacherId", teacherId);
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.Documents.Select(d => d.ConvertTo<Course>()).ToList();
        }

        public async Task EnrollStudentAsync(string studentId, string courseId)
        {
            var query = _firestoreDb.Collection(StudentInfosCollection)
                .WhereEqualTo("StudentId", studentId)
                .WhereEqualTo("CourseId", courseId);
            
            var snap = await query.GetSnapshotAsync();
            if(snap.Count > 0) return;

            var info = new StudentCourseInfo
            {
                Id = Guid.NewGuid().ToString(),
                StudentId = studentId,
                CourseId = courseId,
                EnrolledAt = DateTime.UtcNow
            };
            
            await _firestoreDb.Collection(StudentInfosCollection).Document(info.Id).SetAsync(info);
        }
    }
}
