using Microsoft.AspNetCore.Mvc;
using PrinterCare.Server.Interfaces;
using PrinterCare.Server.Entities;

namespace PrinterCare.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _service;

        public OrganizationsController(IOrganizationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var organizations = await _repository.GetAllAsync();
            return Ok(organizations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var organization = await _repository.GetByIdAsync(id);
            if (organization == null)
                return NotFound();
            return Ok(organization);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Organization organization)
        {
            var result = await _service.CreateOrganizationAsync(organization);

            if (!result)
                return Conflict("Organization with this name already exists.");

            return CreatedAtAction(nameof(GetById), new { id = organization.Id }, organization);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Organization organization)
        {
            if (id != organization.Id)
                return BadRequest("ID mismatch");

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _repository.UpdateAsync(organization);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var organization = await _repository.GetByIdAsync(id);
            if (organization == null)
                return NotFound();

            await _repository.DeleteAsync(organization);
            return NoContent();
        }
    }
}