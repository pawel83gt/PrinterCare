using PrinterCare.Server.DTOs.Branch;
using PrinterCare.Server.Entities;

namespace PrinterCare.Server.Interfaces
{
    public interface IBranchRepository
    {
        Task <IEnumerable<BranchDto>> GetAllAsync();
        Task<BranchDto?> GetByIdAsync(Guid id);
        Task AddAsync(Branch branch);
        Task UpdateAsync(UpdateBranchDto dto);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(string name);
    }
}
