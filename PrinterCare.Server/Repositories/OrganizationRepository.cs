using Microsoft.EntityFrameworkCore;
using PrinterCare.Server.Data;
using PrinterCare.Server.DTOs.Branch;
using PrinterCare.Server.DTOs.Organization;
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        // 2. Внедряем DbContext через конструктор
        private readonly ApplicationDbContext _context;

        public OrganizationRepository(ApplicationDbContext context) => _context = context;

        // 3. Реализуем методы интерфейса

        public async Task<IEnumerable<OrganizationDto>> GetAllAsync()
        {
            return await _context.Organizations
                .AsNoTracking()
                .OrderBy(o => o.Name)
                .Select(o => new OrganizationDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    Branches = o.Branches
                        .OrderBy(b => b.Name)
                        .Select(b => new BranchDto
                        {
                            Id = b.Id,
                            Name = b.Name,
                            OrganizationId = b.OrganizationId,
                            OrganizationName = o.Name
                        })
                        .ToList()
                })
                .ToListAsync();
        }

        public async Task<OrganizationDto?> GetByIdAsync(Guid id)
        {
            return await _context.Organizations
                .AsNoTracking()
                .Where(o => o.Id == id)
                .Select(o => new OrganizationDto
                {
                    Id = o.Id,
                    Name = o.Name,

                    Branches = o.Branches
                        .OrderBy(b => b.Name)
                        .Select(b => new BranchDto
                        {
                            Id = b.Id,
                            Name = b.Name,
                            OrganizationId = b.OrganizationId,
                            OrganizationName = o.Name
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync(); ;
        }

        public async Task AddAsync(Organization organization)
        {
            // Добавляем организацию в контекст и сохраняем в БД
            await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Organization organization)
        {
            // Обновляем только нужные поля (чтобы не перезаписать другие данные)
            _context.Entry(organization).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(OrganizationDto dto)
        {
            var organization = await _context.Organizations.FindAsync(dto.Id);
            if (organization == null)
                return;
            
            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string name)
        {
            // Проверяем, существует ли организация с таким названием
            return await _context.Organizations
                .AnyAsync(o => o.Name == name);
        }
    }
}