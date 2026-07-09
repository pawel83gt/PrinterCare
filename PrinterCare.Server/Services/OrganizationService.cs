using Microsoft.EntityFrameworkCore;
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Services
{
    public class OrganizationService :IOrganizationService
    {
        private readonly IOrganizationRepository _repository;

        public OrganizationService(IOrganizationRepository repository)
        {
            _repository = repository;
        }

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
    }
}
