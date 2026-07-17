using Microsoft.EntityFrameworkCore;
using PrinterCare.Server.Data;
using PrinterCare.Server.DTOs.Branch;
using PrinterCare.Server.DTOs.Organization;
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        //приватное, неизменяемое поле для работы с БД
        private readonly ApplicationDbContext _context;
        //размещение реального контекста в ранее созданный _context
        public BranchRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<BranchDto>> GetAllAsync()
        {
            return await _context.Branches.AsNoTracking().OrderBy(b => b.Name)
                .Select(b => new BranchDto
                    {
                        Id = b.Id,
                        Name = b.Name,
                        OrganizationId = b.OrganizationId,
                        OrganizationName = b.Organization != null ? b.Organization.Name : "Неизвестно",
                }).ToListAsync();
         }

        public async Task <BranchDto?> GetByIdAsync(Guid id)
        {
            var branch = await _context.Branches.AsNoTracking().Include(b => b.Organization).FirstOrDefaultAsync(b =>  b.Id == id);

            if (branch == null)
                return null;

            return new BranchDto
            {
                Id = branch.Id,
                Name = branch.Name,
                OrganizationId = branch.OrganizationId,
                OrganizationName = branch.Organization?.Name ?? "Неизвестно",
            };
        }

        public async Task AddAsync(Branch branch)
        {
            await _context.Branches.AddAsync(branch);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateBranchDto dto)
        {
            var existing = await _context.Branches.FindAsync(dto.Id) ?? throw new KeyNotFoundException($"Organization with id {dto.Id} not found");

            // Обновляем только нужные поля (чтобы не перезаписать другие данные)
            _context.Entry(existing).CurrentValues.SetValues(dto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null)
                return;

            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string name)
        {
            // Проверяем, существует ли организация с таким названием
            return await _context.Branches
                .AnyAsync(o => o.Name == name);
        }
    }
}
