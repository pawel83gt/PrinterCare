using PrinterCare.Server.DTOs.Cartridge;

namespace PrinterCare.Server.Interfaces
{
    public interface ICartridgeService
    {
        Task<CartridgeDto?> CreateCartridgeAsync(CreateCartridgeDto dto);
        Task<IEnumerable<CartridgeDto>> GetAllCartridgeAsync();
        Task<CartridgeDto?> GetByIdAsync(Guid id);
        Task<(CartridgeDto Data, string Message)> UpdateCartridgeAsync(Guid id, UpdateCartridgeDto dto);
        Task DeleteCartridgeAsync(Guid id);
    }
}
