using PrinterCare.Server.Entities;

namespace PrinterCare.Server.Interfaces
{
    public interface IOrganizationService
    {
        Task<bool> CreateOrganizationAsync(Organization organization);
        Task<IEnumerable<Organization>> GetAllOrganizationsAsync();
        Task<Organization?> GetByIdAsync(int id);
    }
}
