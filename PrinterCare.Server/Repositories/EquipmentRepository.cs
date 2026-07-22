using Microsoft.EntityFrameworkCore;
using PrinterCare.Server.Data;
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EquipmentRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Equipment>> GetAllAsync()
        {
            return await _context.Equipments.AsNoTracking().OrderBy(e => e.Alias).ToListAsync();
        }

        public async Task<Equipment?> GetByIdAsync(Guid id)
        {
            var result = await _context.Equipments.AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);
            if (result == null)
                return null;
            return result;
        }

        public async Task AddAsync(Equipment equipment)
        {
            await _context.Equipments.AddAsync(equipment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Equipment equipment)
        {
            _context.Entry(equipment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment == null)
                throw new KeyNotFoundException($"Equipment with id {id} not found");

            _context.Equipments.Remove(equipment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string name)
        {
            // Проверяем, существует ли производитель с таким названием
            return await _context.Equipments
                .AnyAsync(e => e.Alias == name);
        }
    }
}