using Microsoft.EntityFrameworkCore;
using PrinterCare.Server.Data;
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Repositories
{
    public class EquipmentModelRepository : IEquipmentModelRepository
    {
        private readonly ApplicationDbContext _context;

        public EquipmentModelRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<EquipmentModel>> GetAllAsync()
        {
            return await _context.EquipmentModels.AsNoTracking().OrderBy(o => o.Name).ToListAsync();
        }

        public async Task<EquipmentModel?> GetByIdAsync(Guid id)
        {
            var result = await _context.EquipmentModels.AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);
            if (result == null)
                return null;
            return result;
        }

        public async Task AddAsync(EquipmentModel equipmentModel)
        {
            await _context.EquipmentModels.AddAsync(equipmentModel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EquipmentModel equipmentModel)
        {
            _context.Entry(equipmentModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var equipmentModel = await _context.EquipmentModels.FindAsync(id);
            if (equipmentModel == null)
                throw new KeyNotFoundException($"Equipment Model with id {id} not found");

            _context.EquipmentModels.Remove(equipmentModel);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string name)
        {
            // Проверяем, существует ли производитель с таким названием
            return await _context.EquipmentModels
                .AnyAsync(o => o.Name == name);
        }
    }
}
