using PrinterCare.Server.DTOs.Organization;
using PrinterCare.Server.Entities;

namespace PrinterCare.Server.Interfaces
{
    public interface IOrganizationService
    {
        Task<OrganizationDto?> CreateOrganizationAsync(CreateOrganizationDto dto);
        Task<IEnumerable<OrganizationDto>> GetAllOrganizationsAsync();
        Task<OrganizationDto?> GetByIdAsync(Guid id);
        Task<(OrganizationDto Data, string Message)> UpdateOrganizationAsync(Guid id, UpdateOrganizationDto dto);
        Task DeleteOrganizationAsync(Guid id);
    }
}
