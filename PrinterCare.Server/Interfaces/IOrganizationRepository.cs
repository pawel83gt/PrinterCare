using PrinterCare.Server.DTOs.Organization;
using PrinterCare.Server.Entities;

namespace PrinterCare.Server.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<Organization>> GetAllAsync();
        Task<OrganizationDto?> GetByIdAsync(Guid id);
        Task AddAsync(Organization organization);
        Task UpdateAsync(OrganizationDto dto);
        Task DeleteAsync(OrganizationDto dto);
        Task<bool> ExistsAsync(string name);

    }
}
