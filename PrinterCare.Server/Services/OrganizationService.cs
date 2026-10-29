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
            var name = dto.Name.Trim();
            // Логика проверки (существует ли уже организация с таким названием)
            if (await _repository.ExistsAsync(name))
                return null;
            var organization = new Organization 
            {
                Name = name,
            };
            await _repository.AddAsync(organization);
            return new OrganizationDto
            {
                Id = organization.Id,
                Name = organization.Name,
            };
        }
        public async Task<IEnumerable<OrganizationDto>> GetAllOrganizationsAsync()
        {  
            return await _repository.GetAllAsync();
        }

        public async Task<OrganizationDto?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<(OrganizationDto Data, string Message)> UpdateOrganizationAsync(Guid id, UpdateOrganizationDto dto)
        {
            var name = dto.Name.Trim();
            // 1. Бизнес-правило: нельзя обновить несуществующую организацию
            var organization = await _repository.FindAsync(id)
                ?? throw new KeyNotFoundException($"Organization with id {id} not found");

            // 2. Бизнес-правило: нельзя изменить имя на уже занятое
            if (await _repository.ExistsAsync(name, excludeId: id))
                throw new InvalidOperationException("Organization with this name already exists");

            // 3. Меняем поле у загруженной сущности и сохраняем
            organization.Name = name;
            await _repository.UpdateAsync(organization);

            return (new OrganizationDto { Id = id, Name = dto.Name }, "Updated successfully.");
        }

        public async Task DeleteOrganizationAsync(Guid id)
        {
            // 1. Бизнес-правило: нельзя удалить несуществующую организацию
            var organization = await _repository.FindAsync(id)
                ?? throw new KeyNotFoundException($"Organization with id {id} not found");

            await _repository.DeleteAsync(organization);
        }
    }
}
