using Microsoft.AspNetCore.Mvc;
using PrinterCare.Server.DTOs.Organization;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _service;

        public OrganizationsController(IOrganizationService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var organizations = await _service.GetAllOrganizationsAsync();
            return Ok(organizations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var organization = await _service.GetByIdAsync(id);
            if (organization == null)
                return NotFound();
            return Ok(organization);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrganizationDto dto)
        {
            var result = await _service.CreateOrganizationAsync(dto);

            if (result == null)
                return Conflict("Organization with this name already exists.");
            //возврат 201 и возврат объекта
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrganizationDto dto)
        {
            try
            {
                var (data, message) = await _service.UpdateOrganizationAsync(id, dto);
                return Ok(new { data, message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
             return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteOrganizationAsync(id);
                return NoContent(); // 204 - успешно удалено
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // 404 - организация не найдена
            }
        }
    }
}