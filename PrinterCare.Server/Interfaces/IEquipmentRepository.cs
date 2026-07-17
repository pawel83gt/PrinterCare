using PrinterCare.Server.Entities;

namespace PrinterCare.Server.Interfaces
{
    public interface IEquipmentRepository
    {
        Task<IEnumerable<Equipment>> GetAllAsync();
        Task<Equipment?> GetByIdAsync(Guid id);
        Task AddAsync(Equipment equipment);
        Task UpdateAsync(Equipment equipment);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(string name);
    }
}
