using PrinterCare.Server.DTOs.Organization;
using PrinterCare.Server.Entities;

namespace PrinterCare.Server.Interfaces
{
    public interface IOrganizationService
    {
        Task<OrganizationDto?> CreateOrganizationAsync(CreateOrganizationDto dto);
        Task<IEnumerable<Organization>> GetAllOrganizationsAsync();
        Task<OrganizationDto?> GetByIdAsync(Guid id);
        Task<OrganizationDto> UpdateOrganizationAsync(OrganizationDto dto);
        Task DeleteOrganizationAsync(Guid id);
    }
}
