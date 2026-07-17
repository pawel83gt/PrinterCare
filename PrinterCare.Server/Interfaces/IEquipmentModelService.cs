using PrinterCare.Server.DTOs.EquipmentModel;

namespace PrinterCare.Server.Interfaces
{
    public interface IEquipmentModelService
    {
        Task<EquipmentModelDto?> CreateEquipmentModelAsync(CreateEquipmentModelDto dto);
        Task<IEnumerable<EquipmentModelDto>> GetAllEquipmentModelAsync();
        Task<EquipmentModelDto?> GetByIdAsync(Guid id);
        Task<EquipmentModelDto> UpdateEquipmentModelAsync(Guid id, UpdateEquipmentModelDto dto);
        Task DeleteEquipmentModelAsync(Guid id);
    }
}
