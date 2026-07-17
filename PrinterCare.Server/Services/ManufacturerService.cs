using PrinterCare.Server.DTOs.Branch;
using PrinterCare.Server.DTOs.Manufacturer;
using PrinterCare.Server.DTOs.Organization;
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Services
{
    public class ManufacturerService :IManufacturerService
    {
        private readonly IManufacturerRepository _repository;

        public ManufacturerService(IManufacturerRepository repository) => _repository = repository;

        public async Task<ManufacturerDto?> CreateManufacturerAsync(CreateManufacturerDto dto)
        {
            // Логика проверки (существует ли уже организация с таким названием)
            if (await _repository.ExistsAsync(dto.Name))
                return null;
            var manufacturer = new Manufacturer
            {
                Name = dto.Name,
            };
            await _repository.AddAsync(manufacturer);
            return new ManufacturerDto
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name,
            };
        }
        public async Task<IEnumerable<ManufacturerDto>> GetAllManufacturerAsync()
        {
            var manufacturers = await _repository.GetAllAsync();

            return manufacturers.Select(m => new ManufacturerDto
            {
                Id = m.Id,
                Name = m.Name
            });

        }

        public async Task<ManufacturerDto?> GetByIdAsync(Guid id)
        {
            var manufacturer = await _repository.GetByIdAsync(id);
            if (manufacturer == null)
                return null;
            return new ManufacturerDto
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name,
            };
        }

        public async Task<(ManufacturerDto Data, string Message)> UpdateManufacturerAsync(Guid id, UpdateManufacturerDto dto)
        {
            
            // 1. Бизнес-правило: нельзя обновить несуществующего производителя
            Manufacturer existing = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Manufacturer with id {id} not found");
            // 2. Проверяем, изменилось ли имя
            bool nameChanged = existing.Name == dto.Name;
            // 3. Если ничего не изменилось — возвращаем существующий объект
            if (nameChanged)
            {
                return (new ManufacturerDto { Id = id, Name = dto.Name }, "No changes were made."); // ✅ Ничего не обновляем
            }
            // 4. Если имя изменилось — проверяем, не занято ли оно другим производителем
            if (!nameChanged)
            {
                if (await _repository.ExistsAsync(dto.Name))
                {
                    throw new InvalidOperationException("Manufacturer with this name already exists");
                }
            }
            
            existing.Name = dto.Name;

            // 5. Вызов репозитория
            await _repository.UpdateAsync(existing);
            return (new ManufacturerDto { Id = id, Name = dto.Name }, "Updated successfully.");
        }

        public async Task DeleteManufacturerAsync(Guid id)
        {
            // 1. Бизнес-правило: нельзя удалить несуществующую фирму производителя
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Manufacturer with id {id} not found");

            await _repository.DeleteAsync(existing.Id);
        }
    }
}
