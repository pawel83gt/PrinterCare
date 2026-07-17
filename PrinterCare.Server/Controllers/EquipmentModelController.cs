using Microsoft.AspNetCore.Mvc;
using PrinterCare.Server.DTOs.EquipmentModel;
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        
        public class EquipmentModelController : ControllerBase
        {
            private readonly IEquipmentModelService _service;

            public EquipmentModelController(IEquipmentModelService service) => _service = service;

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var equipmentModels = await _service.GetAllEquipmentModelAsync();
                return Ok(equipmentModels);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(Guid id)
            {
                var equipmentModel = await _service.GetByIdAsync(id);
                if (equipmentModel == null)
                    return NotFound();
                return Ok(equipmentModel);
            }

            [HttpPost]
            public async Task<IActionResult> Create(CreateEquipmentModelDto dto)
            {
                var result = await _service.CreateEquipmentModelAsync(dto);

                if (result == null)
                    return Conflict("Equipment Model with this name already exists.");
                //возврат 201 и возврат объекта

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEquipmentModelDto dto)
            {

                try
                {
                    var updated = await _service.UpdateEquipmentModelAsync(id, dto);
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
                    await _service.DeleteEquipmentModelAsync(id);
                    return NoContent(); // 204 - успешно удалено
                }
                catch (KeyNotFoundException)
                {
                    return NotFound(); // 404 - организация не найдена
                }
            }
        }
}
