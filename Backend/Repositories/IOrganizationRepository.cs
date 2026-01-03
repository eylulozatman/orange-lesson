using EducationSystemBackend.Models;

namespace EducationSystemBackend.Repositories
{
   public interface IOrganizationRepository
{
    Task AddAsync(Organization organization);
    Task<List<Organization>> GetAllAsync();
    Task<Organization?> GetByIdAsync(Guid id);
    Task UpdateAsync(Organization organization);
    Task DeleteAsync(Guid id);
}

}
