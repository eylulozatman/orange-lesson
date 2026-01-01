
using Google.Cloud.Firestore;

namespace EducationSystemBackend.Repositories
{
    public interface IFirestoreRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
        Task<IEnumerable<T>> QueryAsync(string field, object value);
    }

    public class FirestoreRepository<T> : IFirestoreRepository<T> where T : class
    {
        private readonly FirestoreDb _firestoreDb;
        private readonly string _collectionName;

        public FirestoreRepository(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
            // Class adını koleksiyon adı olarak kullan (örn: Organization -> Organizations)
            _collectionName = typeof(T).Name + "s"; 
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var collection = _firestoreDb.Collection(_collectionName);
            var snapshot = await collection.GetSnapshotAsync();
            return snapshot.Documents.Select(doc => doc.ConvertTo<T>()).ToList();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var doc = await _firestoreDb.Collection(_collectionName).Document(id).GetSnapshotAsync();
            if (!doc.Exists) return null;
            return doc.ConvertTo<T>();
        }

        public async Task<IEnumerable<T>> QueryAsync(string field, object value)
        {
            var collection = _firestoreDb.Collection(_collectionName);
            var query = collection.WhereEqualTo(field, value);
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.Documents.Select(d => d.ConvertTo<T>()).ToList();
        }

        public async Task AddAsync(T entity)
        {
            // Reflection kullanarak 'Id' propertysini bul ve doküman ID'si olarak kullan
            var idProp = typeof(T).GetProperty("Id");
            var id = idProp?.GetValue(entity)?.ToString();

            if (string.IsNullOrEmpty(id))
            {
                await _firestoreDb.Collection(_collectionName).AddAsync(entity);
            }
            else
            {
                await _firestoreDb.Collection(_collectionName).Document(id).SetAsync(entity);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            var idProp = typeof(T).GetProperty("Id");
            var id = idProp?.GetValue(entity)?.ToString();

            if (!string.IsNullOrEmpty(id))
            {
                await _firestoreDb.Collection(_collectionName).Document(id).SetAsync(entity, SetOptions.MergeAll);
            }
        }

        public async Task DeleteAsync(string id)
        {
            await _firestoreDb.Collection(_collectionName).Document(id).DeleteAsync();
        }
    }
}
