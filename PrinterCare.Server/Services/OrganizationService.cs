using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Services
{
    public class OrganizationService
    {
        private readonly IOrganizationRepository _repository;

        public OrganizationService(IOrganizationRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateOrganizationWithValidationAsync(Organization organization)
        {
            // Логика проверки (существует ли уже организация с таким названием)
            if (await _repository.ExistsAsync(organization.Name))
                return false;

            await _repository.AddAsync(organization);
            return true;
        }
    }
}
