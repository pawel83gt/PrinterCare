using PrinterCare.Server.DTOs.Equipment;

namespace PrinterCare.Server.Interfaces
{
    public interface IEquipmentService
    {
        Task<EquipmentDto?> CreateEquipmentAsync(CreateEquipmentDto dto);
        Task<IEnumerable<EquipmentDto>> GetAllEquipmentAsync();
        Task<EquipmentDto?> GetByIdAsync(Guid id);
        Task<(EquipmentDto Data, string Message)> UpdateEquipmentAsync(Guid id, UpdateEquipmentDto dto);
        Task DeleteEquipmentAsync(Guid id);
    }
}
