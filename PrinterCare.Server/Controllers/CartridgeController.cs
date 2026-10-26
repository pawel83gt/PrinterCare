using Microsoft.AspNetCore.Mvc;
using PrinterCare.Server.DTOs.Cartridge;
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        
        public class CartridgeController : ControllerBase
        {
            private readonly ICartridgeService _service;

            public CartridgeController(ICartridgeService service) => _service = service;

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var cartridges = await _service.GetAllCartridgeAsync();
                return Ok(cartridges);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(Guid id)
            {
                var cartridge = await _service.GetByIdAsync(id);
                if (cartridge == null)
                    return NotFound();
                return Ok(cartridge);
            }

            [HttpPost]
            public async Task<IActionResult> Create(CreateCartridgeDto dto)
            {
                var result = await _service.CreateCartridgeAsync(dto);

                if (result == null)
                    return Conflict("Cartridge with this name already exists.");
                //возврат 201 и возврат объекта

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCartridgeDto dto)
            {

                try
                {
                    var updated = await _service.UpdateCartridgeAsync(id, dto);
                    return Ok(updated);
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
                    await _service.DeleteCartridgeAsync(id);
                    return NoContent(); // 204 - успешно удалено
                }
                catch (KeyNotFoundException)
                {
                    return NotFound(); // 404 - организация не найдена
                }
            }
        }
}
