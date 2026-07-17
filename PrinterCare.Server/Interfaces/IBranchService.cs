using PrinterCare.Server.DTOs.Branch;
using PrinterCare.Server.Entities;

namespace PrinterCare.Server.Interfaces
{
    public interface IBranchService
    {
        Task<BranchDto?> CreateBranchAsync(CreateBranchDto dto);
        Task<IEnumerable<BranchDto>> GetAllBranchAsync();
        Task<BranchDto?> GetByIdAsync(Guid id);
        Task<UpdateBranchDto> UpdateBranchAsync(UpdateBranchDto dto);
        Task DeleteBranchAsync(Guid id);
    }
}
