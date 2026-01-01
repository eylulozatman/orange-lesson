using Microsoft.AspNetCore.Mvc;
using EducationSystemBackend.Services;
using EducationSystemBackend.Requests;
using Microsoft.AspNetCore.Mvc;
using EducationSystemBackend.Services;
using EducationSystemBackend.Requests;
using EducationSystemBackend.Models;

namespace EducationSystemBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationsController : ControllerBase
    {
        private readonly OrganizationService _service;

        public OrganizationsController(OrganizationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrganizationRequest req)
        {
            var org = new Organization
            {
                Name = req.Name,
                Address = req.Address
            };

            await _service.AddAsync(org);
            return CreatedAtAction(nameof(GetAll), new { id = org.Id }, org);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orgs = await _service.GetVisibleAsync();
            return Ok(orgs);
        }
    }
}
