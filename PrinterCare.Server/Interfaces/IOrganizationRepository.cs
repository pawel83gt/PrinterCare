using PrinterCare.Server.Entities;

namespace PrinterCare.Server.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<Organization>> GetAllAsync();
        Task<Organization?> GetbyIdAsync(int id);
        Task AddAsync(Organization organization);
        Task UpdateAsync(Organization organization);
        Task DeleteAsync(Organization organization);
        Task<bool> ExistsAsync(string name);

    }
}
