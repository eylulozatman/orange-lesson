using Google.Cloud.Firestore;
using EducationSystemBackend.Models;
using FirebaseAdmin;
namespace EducationSystemBackend.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly FirestoreDb _firestoreDb;
        private const string CollectionName = "Teachers";
        private const string CoursesCollection = "Courses";

        public TeacherRepository(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public async Task AddAsync(Teacher teacher)
        {
            var docRef = _firestoreDb.Collection(CollectionName).Document(teacher.Id);
            await docRef.SetAsync(teacher);
        }

        public async Task<Teacher?> GetByIdAsync(string teacherId)
        {
            var doc = await _firestoreDb.Collection(CollectionName).Document(teacherId).GetSnapshotAsync();
            return doc.Exists ? doc.ConvertTo<Teacher>() : null;
        }

        public async Task<Teacher?> GetByEmailAsync(string email)
        {
            var query = _firestoreDb.Collection(CollectionName).WhereEqualTo("Email", email);
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.Documents.Select(d => d.ConvertTo<Teacher>()).FirstOrDefault();
        }

        public async Task<List<Teacher>> GetAllAsync()
        {
            var snapshot = await _firestoreDb.Collection(CollectionName).GetSnapshotAsync();
            return snapshot.Documents.Select(d => d.ConvertTo<Teacher>()).ToList();
        }

        public async Task AssignCourseAsync(TeacherCourseInfo info)
        {
            // Assuming 1-to-Maybe Many, but Course has single TeacherId
            // We need to update the Course document to set TeacherId
            var courseRef = _firestoreDb.Collection(CoursesCollection).Document(info.CourseId);
            // Fetch first to preserve other fields? update only TeacherId
            await courseRef.UpdateAsync("TeacherId", info.TeacherId);
        }

        public async Task<List<TeacherCourseInfo>> GetTeacherCoursesAsync(string teacherId)
        {
            // Query Courses where TeacherId matches
            var query = _firestoreDb.Collection(CoursesCollection).WhereEqualTo("TeacherId", teacherId);
            var snapshot = await query.GetSnapshotAsync();
            
            return snapshot.Documents.Select(d => {
                var c = d.ConvertTo<Course>();
                return new TeacherCourseInfo 
                { 
                    CourseId = c.Id, 
                    TeacherId = teacherId 
                };
            }).ToList();
        }
    }
}
