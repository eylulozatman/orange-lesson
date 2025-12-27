using EducationSystemBackend.Models;
using EducationSystemBackend.Requests;
using EducationSystemBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystemBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationsController : ControllerBase
    {
        private readonly IRepository<Organization> _orgRepository;

        public OrganizationsController(IRepository<Organization> orgRepository)
        {
            _orgRepository = orgRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrganizationRequest request)
        {
            var org = new Organization
            {
                Name = request.Name,
                Address = request.Address
            };

            await _orgRepository.AddAsync(org);
            await _orgRepository.SaveChangesAsync();
            return Ok(org);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orgs = await _orgRepository.FindAsync(o => !o.IsHidden);
            return Ok(orgs);
        }
    }
}
