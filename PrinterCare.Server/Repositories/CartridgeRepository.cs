using Microsoft.EntityFrameworkCore;
using PrinterCare.Server.Data;
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Repositories
{
    public class CartridgeRepository : ICartridgeRepository
    {
        private readonly ApplicationDbContext _context;

        public CartridgeRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Cartridge>> GetAllAsync()
        {
            return await _context.Cartridges.AsNoTracking().OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<Cartridge?> GetByIdAsync(Guid id)
        {
            var result = await _context.Cartridges.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
            if (result == null)
                return null;
            return result;
        }

        public async Task AddAsync(Cartridge cartridge)
        {
            await _context.Cartridges.AddAsync(cartridge);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cartridge cartridge)
        {
            _context.Entry(cartridge).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var cartridge = await _context.Cartridges.FindAsync(id);
            if (cartridge == null)
                throw new KeyNotFoundException($"Cartridge with id {id} not found");

            _context.Cartridges.Remove(cartridge);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string name)
        {
            // Проверяем, существует ли картридж с таким названием
            return await _context.Cartridges
                .AnyAsync(c => c.Name == name);
        }
    }
}