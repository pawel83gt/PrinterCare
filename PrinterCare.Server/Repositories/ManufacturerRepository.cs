using Microsoft.EntityFrameworkCore;
using PrinterCare.Server.Data;
using PrinterCare.Server.DTOs.Manufacturer;
using PrinterCare.Server.DTOs.Organization;
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Repositories
{
    public class ManufacturerRepository :IManufacturerRepository
    {
        private readonly ApplicationDbContext _context;

        public ManufacturerRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Manufacturer>> GetAllAsync()
        {
            return await _context.Manufacturers.AsNoTracking().OrderBy(o => o.Name).ToListAsync();
        }

        public async Task<Manufacturer?> GetByIdAsync(Guid id)
        {
            var result = await _context.Manufacturers.AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);
            if (result == null)
                return null;
            return result;
        }

        public async Task AddAsync(Manufacturer manufacturer)
        {
            await _context.Manufacturers.AddAsync(manufacturer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Manufacturer manufacturer)
        {

            _context.Entry(manufacturer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null)
                throw new KeyNotFoundException($"Manufacturer with id {id} not found");

            _context.Manufacturers.Remove(manufacturer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string name)
        {
            // Проверяем, существует ли производитель с таким названием
            return await _context.Manufacturers
                .AnyAsync(o => o.Name == name);
        }
    }
}
