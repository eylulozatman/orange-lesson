using EducationSystemBackend.Models;
using EducationSystemBackend.Repositories;

namespace EducationSystemBackend.Services
{
    public class OrganizationService
    {
        private readonly IFirestoreRepository<Organization> _repo;

        public OrganizationService(IFirestoreRepository<Organization> repo)
        {
            _repo = repo;
        }

        public Task AddAsync(Organization org) => _repo.AddAsync(org);

        public async Task<IEnumerable<Organization>> GetVisibleAsync()
        {
            var all = await _repo.GetAllAsync();
            return all.Where(o => !o.IsHidden);
        }

        public Task<IEnumerable<Organization>> GetAllAsync() => _repo.GetAllAsync();
    }
}
