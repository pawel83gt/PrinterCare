using Microsoft.EntityFrameworkCore;
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Services
{
    public class OrganizationService :IOrganizationService
    {
        private readonly IOrganizationRepository _repository;

        public OrganizationService(IOrganizationRepository repository) => _repository = repository;

        public async Task<bool> CreateOrganizationAsync(Organization organization)
        {
            // Логика проверки (существует ли уже организация с таким названием)
            if (await _repository.ExistsAsync(organization.Name))
                return false;

            await _repository.AddAsync(organization);
            return true;
        }
        public async Task<IEnumerable<Organization>> GetAllOrganizationsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Organization?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Organization> UpdateOrganizationAsync(Organization organization)
        {
            // 1. Бизнес-правило: нельзя обновить несуществующую организацию
            Organization existing = await _repository.GetByIdAsync(organization.Id) ?? throw new KeyNotFoundException($"Organization with id {organization.Id} not found");

            // 2. Бизнес-правило: нельзя изменить имя на уже занятое
            if (await _repository.ExistsAsync(organization.Name))
                throw new InvalidOperationException("Organization with this name already exists");

            // 3. Вызов репозитория
            await _repository.UpdateAsync(organization);
            return organization;
        }

        public async Task DeleteOrganizationAsync(int id)
        {
            // 1. Бизнес-правило: нельзя удалить несуществующую организацию
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Organization with id {id} not found");

             await _repository.DeleteAsync(existing);
        }
    }
}
