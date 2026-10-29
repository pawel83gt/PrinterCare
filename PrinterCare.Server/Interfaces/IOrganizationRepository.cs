using PrinterCare.Server.DTOs.Organization;
using PrinterCare.Server.Entities;

namespace PrinterCare.Server.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<Organization?> FindAsync(Guid id);
        Task<IEnumerable<OrganizationDto>> GetAllAsync();
        Task<OrganizationDto?> GetByIdAsync(Guid id);
        Task AddAsync(Organization organization);
        Task UpdateAsync(Organization organization);
        Task DeleteAsync(Organization organization);
        Task<bool> ExistsAsync(string name, Guid? excludeId = null);

    }
}
