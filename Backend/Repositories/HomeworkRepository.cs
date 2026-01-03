using Google.Cloud.Firestore;
using EducationSystemBackend.Models;
using FirebaseAdmin;
namespace EducationSystemBackend.Repositories
{
    public class HomeworkRepository : IHomeworkRepository
    {
        private readonly FirestoreDb _firestoreDb;
        private const string CollectionName = "Homeworks";
        private const string SubmissionsCollection = "HomeworkSubmissions";

        public HomeworkRepository(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public async Task AddAsync(Homework homework)
        {
            var docRef = _firestoreDb.Collection(CollectionName).Document(homework.Id);
            await docRef.SetAsync(homework);
        }

        public async Task<List<Homework>> GetAllAsync()
        {
            var snapshot = await _firestoreDb.Collection(CollectionName).GetSnapshotAsync();
            return snapshot.Documents.Select(d => d.ConvertTo<Homework>()).ToList();
        }

        public async Task<Homework?> GetByIdAsync(string homeworkId)
        {
            var doc = await _firestoreDb.Collection(CollectionName).Document(homeworkId).GetSnapshotAsync();
            return doc.Exists ? doc.ConvertTo<Homework>() : null;
        }

        public async Task<List<Homework>> GetByCourseIdAsync(string courseId)
        {
            var query = _firestoreDb.Collection(CollectionName).WhereEqualTo("CourseId", courseId);
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.Documents.Select(d => d.ConvertTo<Homework>()).ToList();
        }

        public async Task<List<Homework>> GetByTeacherIdAsync(string teacherId)
        {
            var query = _firestoreDb.Collection(CollectionName).WhereEqualTo("TeacherId", teacherId);
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.Documents.Select(d => d.ConvertTo<Homework>()).ToList();
        }

        public async Task<List<Homework>> GetByCourseIdsAsync(List<string> courseIds)
        {
            if (courseIds == null || !courseIds.Any()) return new List<Homework>();
            
            var allHomeworks = new List<Homework>();
            var chunks = courseIds.Chunk(10); 

            foreach (var chunk in chunks)
            {
                // chunk is string[] or IEnumerable<string>
                var query = _firestoreDb.Collection(CollectionName).WhereIn("CourseId", chunk.ToList());
                var snapshot = await query.GetSnapshotAsync();
                allHomeworks.AddRange(snapshot.Documents.Select(d => d.ConvertTo<Homework>()));
            }
            return allHomeworks;
        }

        public async Task AddSubmissionAsync(HomeworkSubmission submission)
        {
            var docRef = _firestoreDb.Collection(SubmissionsCollection).Document(submission.Id);
            await docRef.SetAsync(submission);
        }

        public async Task<List<HomeworkSubmission>> GetSubmissionsByHomeworkIdAsync(string homeworkId)
        {
            var query = _firestoreDb.Collection(SubmissionsCollection).WhereEqualTo("HomeworkId", homeworkId);
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.Documents.Select(d => d.ConvertTo<HomeworkSubmission>()).ToList();
        }

        public async Task<List<HomeworkSubmission>> GetSubmissionsByStudentAsync(string studentId)
        {
            var query = _firestoreDb.Collection(SubmissionsCollection).WhereEqualTo("StudentId", studentId);
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.Documents.Select(d => d.ConvertTo<HomeworkSubmission>()).ToList();
        }

        public async Task<List<HomeworkSubmission>> GetSubmissionsByStudentAndCourse(string studentId, string courseId)
        {
            var query = _firestoreDb.Collection(SubmissionsCollection)
                .WhereEqualTo("StudentId", studentId)
                .WhereEqualTo("CourseId", courseId); 
            
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.Documents.Select(d => d.ConvertTo<HomeworkSubmission>()).ToList();
        }
    }
}
