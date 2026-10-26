using PrinterCare.Server.Entities;

namespace PrinterCare.Server.Interfaces
{
    public interface ICartridgeRepository
    {
        Task<IEnumerable<Cartridge>> GetAllAsync();
        Task<Cartridge?> GetByIdAsync(Guid id);
        Task AddAsync(Cartridge cartridge);
        Task UpdateAsync(Cartridge cartridge);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(string name);
    }
}
