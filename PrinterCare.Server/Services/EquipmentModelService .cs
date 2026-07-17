using Microsoft.EntityFrameworkCore;
using PrinterCare.Server.DTOs.EquipmentModel;
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Services
{
    public class EquipmentModelService : IEquipmentModelService
    {
        private readonly IEquipmentModelRepository _repository;

        public EquipmentModelService(IEquipmentModelRepository repository) => _repository = repository;

        public async Task<EquipmentModelDto?> CreateEquipmentModelAsync(CreateEquipmentModelDto dto)
        {
            // Логика проверки (существует ли уже модель оборудования с таким названием)
            if (await _repository.ExistsAsync(dto.Name))
                return null;
            var equipmentModel = new EquipmentModel
            {
                Name = dto.Name,
                ManufacturerId = dto.ManufacturerId,
            };
            await _repository.AddAsync(equipmentModel);
            return new EquipmentModelDto
            {
                Id = equipmentModel.Id,
                Name = equipmentModel.Name,
                ManufacturerId = equipmentModel.ManufacturerId,
            };
        }
        public async Task<IEnumerable<EquipmentModelDto>> GetAllEquipmentModelAsync()
        {
            var equipmentModels = await _repository.GetAllAsync();

            return equipmentModels.Select(m => new EquipmentModelDto
            {
                Id = m.Id,
                Name = m.Name,
                ManufacturerId = m.ManufacturerId,
            });

        }

        public async Task<EquipmentModelDto?> GetByIdAsync(Guid id)
        {
            var equipmentModel = await _repository.GetByIdAsync(id);
            if (equipmentModel == null)
                return null;
            return new EquipmentModelDto
            {
                Id = equipmentModel.Id,
                Name = equipmentModel.Name,
                ManufacturerId = equipmentModel.ManufacturerId,
            };
        }

        public async Task<EquipmentModelDto> UpdateEquipmentModelAsync(Guid id, UpdateEquipmentModelDto dto)
        {

            // 1. Бизнес-правило: нельзя обновить несуществующего производителя
            EquipmentModel existing = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Equipment Model with id {id} not found");
            // 2. Проверяем, изменилось ли имя
            bool nameChanged = existing.Name == dto.Name;
            bool manufacturerIdChanged = existing.ManufacturerId == dto.ManufacturerId;
            // 3. Если ничего не изменилось — возвращаем существующий объект
            if (nameChanged && manufacturerIdChanged)
            {
                return new EquipmentModelDto { Id = id, Name = dto.Name, ManufacturerId = dto.ManufacturerId}; // ✅ Ничего не обновляем
            }
            // 4. Если имя изменилось — проверяем, не занято ли оно другим производителем
            if (!nameChanged)
            {
                if (await _repository.ExistsAsync(dto.Name))
                {
                    throw new InvalidOperationException("Equipment Model with this name already exists");
                }
            }
            existing.Name = dto.Name;
            existing.ManufacturerId = dto.ManufacturerId;

            // 5. Вызов репозитория
            await _repository.UpdateAsync(existing);
            return new EquipmentModelDto { Id = id, Name = dto.Name, ManufacturerId = dto.ManufacturerId };
        }

        public async Task DeleteEquipmentModelAsync(Guid id)
        {
            // 1. Бизнес-правило: нельзя удалить несуществующую фирму производителя
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Equipment Model with id {id} not found");

            await _repository.DeleteAsync(existing.Id);
        }
    }
}
