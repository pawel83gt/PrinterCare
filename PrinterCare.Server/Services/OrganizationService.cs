using Microsoft.EntityFrameworkCore;
using PrinterCare.Server.DTOs.Organization;
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Services
{
    public class OrganizationService :IOrganizationService
    {
        private readonly IOrganizationRepository _repository;

        public OrganizationService(IOrganizationRepository repository) => _repository = repository;

        public async Task<OrganizationDto?> CreateOrganizationAsync(CreateOrganizationDto dto)
        {
            // Логика проверки (существует ли уже организация с таким названием)
            if (await _repository.ExistsAsync(dto.Name))
                return null;
            var organization = new Organization 
            {
                Name = dto.Name,
            };
            await _repository.AddAsync(organization);
            return new OrganizationDto
            {
                Id = organization.Id,
                Name = organization.Name,
            };
        }
        public async Task<IEnumerable<Organization>> GetAllOrganizationsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<OrganizationDto?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<OrganizationDto> UpdateOrganizationAsync(OrganizationDto dto)
        {
            // 1. Бизнес-правило: нельзя обновить несуществующую организацию
            OrganizationDto existing = await _repository.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException($"Organization with id {dto.Id} not found");

            // 2. Бизнес-правило: нельзя изменить имя на уже занятое
            if (await _repository.ExistsAsync(dto.Name))
                throw new InvalidOperationException("Organization with this name already exists");

            // 3. Вызов репозитория
            await _repository.UpdateAsync(dto);
            return existing;
        }

        public async Task DeleteOrganizationAsync(Guid id)
        {
            // 1. Бизнес-правило: нельзя удалить несуществующую организацию
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Organization with id {id} not found");

             await _repository.DeleteAsync(existing);
        }
    }
}
