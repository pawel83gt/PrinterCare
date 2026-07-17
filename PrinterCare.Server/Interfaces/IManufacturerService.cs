using PrinterCare.Server.DTOs.Manufacturer;

namespace PrinterCare.Server.Interfaces
{
    public interface IManufacturerService
    {
        Task<ManufacturerDto?> CreateManufacturerAsync(CreateManufacturerDto dto);
        Task<IEnumerable<ManufacturerDto>> GetAllManufacturerAsync();
        Task<ManufacturerDto?> GetByIdAsync(Guid id);
        Task<(ManufacturerDto Data, string Message)> UpdateManufacturerAsync(Guid id, UpdateManufacturerDto dto);
        Task DeleteManufacturerAsync(Guid id);
    }
}
