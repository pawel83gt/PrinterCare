using Microsoft.AspNetCore.Mvc;
using PrinterCare.Server.DTOs.Manufacturer;
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        
        public class ManufacturerController : ControllerBase
        {
            private readonly IManufacturerService _service;

            public ManufacturerController(IManufacturerService service) => _service = service;

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var manufacturers = await _service.GetAllManufacturerAsync();
                return Ok(manufacturers);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(Guid id)
            {
                var manufacturer = await _service.GetByIdAsync(id);
                if (manufacturer == null)
                    return NotFound();
                return Ok(manufacturer);
            }

            [HttpPost]
            public async Task<IActionResult> Create(CreateManufacturerDto dto)
            {
                var result = await _service.CreateManufacturerAsync(dto);

                if (result == null)
                    return Conflict("Manufacturer with this name already exists.");
                //возврат 201 и возврат объекта

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(Guid id, [FromBody] UpdateManufacturerDto dto)
            {
            try
                {
                    var (data, message) = await _service.UpdateManufacturerAsync(id, dto);
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
                    await _service.DeleteManufacturerAsync(id);
                    return NoContent(); // 204 - успешно удалено
                }
                catch (KeyNotFoundException)
                {
                    return NotFound(); // 404 - организация не найдена
                }
            }
        }
}
