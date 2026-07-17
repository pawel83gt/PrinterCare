using PrinterCare.Server.DTOs.Manufacturer;
using PrinterCare.Server.Entities;

namespace PrinterCare.Server.Interfaces
{
    public interface IManufacturerRepository
    {
        Task<IEnumerable<Manufacturer>> GetAllAsync();
        Task<Manufacturer?> GetByIdAsync(Guid id);
        Task AddAsync(Manufacturer manufacturer);
        Task UpdateAsync(Manufacturer manufacturer);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(string name);
    }
}
