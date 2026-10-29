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

        public async Task<Organization?> FindAsync(Guid id) => await _context.Organizations.FindAsync(id);

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
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Organization organization)
        {
            // Добавляем организацию в контекст и сохраняем в БД
            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Organization organization)
        {
            // сущность уже отслеживается (загружена через FindAsync),
            // EF сам знает, что изменилось, поэтому достаточно сохранить
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Organization organization)
        {
            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string name, Guid? excludeId = null)
        {
            var query = _context.Organizations.Where(o => o.Name == name);

            if (excludeId is { } id) //если значение передано, положи его в id и добавь условие Id != id
                query = query.Where(o => o.Id != id);

            return await query.AnyAsync();
        }
    }
}