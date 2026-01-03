using EducationSystemBackend.Models;
using EducationSystemBackend.Repositories;

namespace EducationSystemBackend.Services
{
    public class OrganizationService
    {
        private readonly IOrganizationRepository _repo;

        public OrganizationService(IOrganizationRepository repo)
        {
            _repo = repo;
        }

        public Task AddAsync(Organization org) => _repo.AddAsync(org);

        public async Task<IEnumerable<Organization>> GetVisibleAsync()
        {
            var all = await _repo.GetAllAsync();
            return all.Where(o => !o.IsHidden);
        }

        public Task<IEnumerable<Organization>> GetAllAsync() => 
            _repo.GetAllAsync().ContinueWith(t => (IEnumerable<Organization>)t.Result);
    }
}
