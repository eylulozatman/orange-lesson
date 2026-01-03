using EducationSystemBackend.Models;
using FirebaseAdmin;
using Google.Cloud.Firestore;

namespace EducationSystemBackend.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly FirestoreDb _firestoreDb;
        private const string CollectionName = "Organizations";

        public OrganizationRepository(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public async Task<List<Organization>> GetAllAsync()
        {
            var snapshot = await _firestoreDb
                .Collection(CollectionName)
                .GetSnapshotAsync();

            return snapshot.Documents
                .Select(d => d.ConvertTo<Organization>())
                .ToList();
        }

        public async Task<Organization?> GetByIdAsync(Guid id)
        {
            var docRef = _firestoreDb
                .Collection(CollectionName)
                .Document(id.ToString());

            var snapshot = await docRef.GetSnapshotAsync();

            return snapshot.Exists ? snapshot.ConvertTo<Organization>() : null;
        }

        public async Task AddAsync(Organization organization)
        {
            var docRef = _firestoreDb
                .Collection(CollectionName)
                .Document(organization.Id.ToString());

            await docRef.SetAsync(organization);
        }

        public async Task UpdateAsync(Organization organization)
        {
            var docRef = _firestoreDb
                .Collection(CollectionName)
                .Document(organization.Id.ToString());

            await docRef.SetAsync(organization, SetOptions.MergeAll);
        }

        public async Task DeleteAsync(Guid id)
        {
            var docRef = _firestoreDb
                .Collection(CollectionName)
                .Document(id.ToString());

            await docRef.DeleteAsync();
        }
    }
}
