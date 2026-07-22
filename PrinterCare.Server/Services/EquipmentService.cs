using Microsoft.EntityFrameworkCore;
using PrinterCare.Server.DTOs.Equipment;
using PrinterCare.Server.DTOs.Manufacturer;
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _repository;

        public EquipmentService(IEquipmentRepository repository) => _repository = repository;

        public async Task<EquipmentDto?> CreateEquipmentAsync(CreateEquipmentDto dto)
        {
            // Логика проверки (существует ли уже модель оборудования с таким названием)
            if (await _repository.ExistsAsync(dto.Alias))
                return null;
            var equipment = new Equipment
            {
                Alias = dto.Alias,
                Type = (EquipmentType)dto.Type,
                BranchId = dto.BranchId,
                EquipmentModelId = dto.EquipmentModelId,
            };
            await _repository.AddAsync(equipment);
            return new EquipmentDto
            {
                Alias = dto.Alias,
                Type = dto.Type,
                BranchId = dto.BranchId,
                EquipmentModelId = dto.EquipmentModelId,
            };
        }
        public async Task<IEnumerable<EquipmentDto>> GetAllEquipmentAsync()
        {
            var equipments = await _repository.GetAllAsync();

            return equipments.Select(e => new EquipmentDto
            {
                Type = (int)e.Type,
                Alias = e.Alias,
                BranchId = e.BranchId,
                BranchName = e.Branch?.Name ?? "Неизвестно",
                EquipmentModelId = e.EquipmentModelId,
                EquipmentModelName = e.EquipmentModel.Name ?? "Неизвестно",

            });

        }

        public async Task<EquipmentDto?> GetByIdAsync(Guid id)
        {
            var equipment = await _repository.GetByIdAsync(id);
            if (equipment == null)
                return null;
            return new EquipmentDto
            {
                Type = (int)equipment.Type,
                Alias = equipment.Alias,
                BranchId = equipment.BranchId,
                BranchName = equipment.Branch?.Name ?? "Неизвестно",
                EquipmentModelId = equipment.EquipmentModelId,
                EquipmentModelName = equipment.EquipmentModel.Name ?? "Неизвестно",
            };
        }

        public async Task<(EquipmentDto Data, string Message)> UpdateEquipmentAsync(Guid id, UpdateEquipmentDto dto)
        {

            // 1. Бизнес-правило: нельзя обновить несуществующего производителя
            Equipment existing = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Equipment with id {id} not found");
            // 2. Проверяем, изменилcя ли Type
            bool typeUnchanged = (int)existing.Type == dto.Type;
            bool branchIdUnchanged = existing.BranchId == dto.BranchId;
            bool EquipmentModelIdUnchanged = existing.EquipmentModelId == dto.EquipmentModelId;
            // 3. Если ничего не изменилось — возвращаем существующий объект
            if (typeUnchanged && branchIdUnchanged && EquipmentModelIdUnchanged)
            {
                return (new EquipmentDto {
                    Type = (int)existing.Type,
                    Alias = existing.Alias,
                    BranchId = existing.BranchId,
                    BranchName = existing.Branch?.Name ?? "Неизвестно",
                    EquipmentModelId = existing.EquipmentModelId,
                    EquipmentModelName = existing.EquipmentModel.Name ?? "Неизвестно",
                }, "No changes were made."); // ✅ Ничего не обновляем
            }
            
            existing.Type = (EquipmentType)dto.Type;
            existing.Alias = existing.Alias;
            existing.BranchId = existing.BranchId;
            existing.EquipmentModelId = existing.EquipmentModelId;

            // 5. Вызов репозитория
            await _repository.UpdateAsync(existing);
            return (new EquipmentDto {
                Id = id,
                Type = (int)existing.Type,
                Alias = existing.Alias,
                BranchId = existing.BranchId,
                BranchName = existing.Branch?.Name ?? "Неизвестно",
                EquipmentModelId = existing.EquipmentModelId,
                EquipmentModelName = existing.EquipmentModel.Name ?? "Неизвестно",
            }, "Updated successfully.");
        }

        public async Task DeleteEquipmentAsync(Guid id)
        {
            // 1. Бизнес-правило: нельзя удалить несуществующую фирму производителя
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Equipment with id {id} not found");

            await _repository.DeleteAsync(id);
        }
    }
}
