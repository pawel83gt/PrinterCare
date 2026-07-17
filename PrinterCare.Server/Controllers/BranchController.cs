using Microsoft.AspNetCore.Mvc;
using PrinterCare.Server.DTOs.Branch;
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _service;

        public BranchController(IBranchService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var branches = await _service.GetAllBranchAsync();
            return Ok(branches);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var branch = await _service.GetByIdAsync(id);
            if (branch == null)
                return NotFound();
            return Ok(branch);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBranchDto dto)
        {
            Console.WriteLine($"OrganizationId: {dto.OrganizationId}");
            var result = await _service.CreateBranchAsync(dto);

            if (result == null)
                return Conflict("Branch with this name already exists.");
            //возврат 201 и возврат объекта
            
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBranchDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            try
            {
                var updated = await _service.UpdateBranchAsync(dto);
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
                await _service.DeleteBranchAsync(id);
                return NoContent(); // 204 - успешно удалено
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // 404 - организация не найдена
            }
        }
    }
}
