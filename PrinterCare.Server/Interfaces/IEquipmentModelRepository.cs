using PrinterCare.Server.Entities;

namespace PrinterCare.Server.Interfaces
{
    public interface IEquipmentModelRepository
    {
        Task<IEnumerable<EquipmentModel>> GetAllAsync();
        Task<EquipmentModel?> GetByIdAsync(Guid id);
        Task AddAsync(EquipmentModel equipmentModel);
        Task UpdateAsync(EquipmentModel equipmentModel);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(string name);
    }
}
