using PrinterCare.Server.DTOs.Cartridge;
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Services
{
    public class CartridgeService : ICartridgeService
    {
        private readonly ICartridgeRepository _repository;

        public CartridgeService(ICartridgeRepository repository) => _repository = repository;

        public async Task<CartridgeDto?> CreateCartridgeAsync(CreateCartridgeDto dto)
        {
            // Логика проверки (существует ли уже модель оборудования с таким названием)
            if (await _repository.ExistsAsync(dto.Name))
                return null;
            var cartridge = new Cartridge
            {
                Name = dto.Name,
            };
            await _repository.AddAsync(cartridge);
            return new CartridgeDto
            {
                Id = cartridge.Id,
                Name = cartridge.Name,
            };
        }
        public async Task<IEnumerable<CartridgeDto>> GetAllCartridgeAsync()
        {
            var cartridges = await _repository.GetAllAsync();

            return cartridges.Select(c => new CartridgeDto
            {
                Id = c.Id,
                Name = c.Name,
            });

        }

        public async Task<CartridgeDto?> GetByIdAsync(Guid id)
        {
            var cartridge = await _repository.GetByIdAsync(id);
            if (cartridge == null)
                return null;
            return new CartridgeDto
            {
                Name = cartridge.Name,
            };
        }

        public async Task<(CartridgeDto Data, string Message)> UpdateCartridgeAsync(Guid id, UpdateCartridgeDto dto)
        {

            // 1. Бизнес-правило: нельзя обновить несуществующий картридж
            Cartridge existing = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Cartridge with id {id} not found");
            // 2. Проверяем, изменилcя ли Name
            bool NameUnchanged = existing.Name == dto.Name;
            // 3. Если ничего не изменилось — возвращаем существующий объект
            if (NameUnchanged)
            {
                return (new CartridgeDto {
                    Name = dto.Name,
                }, "No changes were made."); // ✅ Ничего не обновляем
            }
            
            existing.Name = dto.Name;

            // 4. Вызов репозитория
            await _repository.UpdateAsync(existing);
            return (new CartridgeDto {
                Id = id,
                Name = dto.Name,
            }, "Updated successfully.");
        }

        public async Task DeleteCartridgeAsync(Guid id)
        {
            // 1. Бизнес-правило: нельзя удалить несуществующий картридж
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Cartridge with id {id} not found");

            await _repository.DeleteAsync(id);
        }
    }
}
