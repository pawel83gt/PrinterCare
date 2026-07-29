using Microsoft.AspNetCore.Mvc;
using PrinterCare.Server.DTOs.Equipment;
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        
        public class EquipmentController : ControllerBase
        {
            private readonly IEquipmentService _service;

            public EquipmentController(IEquipmentService service) => _service = service;

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var equipments = await _service.GetAllEquipmentAsync();
                return Ok(equipments);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(Guid id)
            {
                var equipment = await _service.GetByIdAsync(id);
                if (equipment == null)
                    return NotFound();
                return Ok(equipment);
            }

            [HttpPost]
            public async Task<IActionResult> Create(CreateEquipmentDto dto)
            {
                var result = await _service.CreateEquipmentAsync(dto);

                if (result == null)
                    return Conflict("Equipment with this name already exists.");
                //возврат 201 и возврат объекта

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEquipmentDto dto)
            {

                try
                {
                    var updated = await _service.UpdateEquipmentAsync(id, dto);
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
                    await _service.DeleteEquipmentAsync(id);
                    return NoContent(); // 204 - успешно удалено
                }
                catch (KeyNotFoundException)
                {
                    return NotFound(); // 404 - организация не найдена
                }
            }
        }
}
