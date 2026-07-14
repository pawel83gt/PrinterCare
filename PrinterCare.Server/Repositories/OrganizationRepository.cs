using Microsoft.EntityFrameworkCore;
using PrinterCare.Server.Data;
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

        public async Task<IEnumerable<Organization>> GetAllAsync()
        {
            // Получаем все организации из базы данных
            return await _context.Organizations.AsNoTracking().OrderBy(o => o.Name).ToListAsync();
        }

        public async Task<OrganizationDto?> GetByIdAsync(Guid id)
        {
            // Ищем организацию по Id, возвращаем null, если не найдена
            var result = await _context.Organizations.AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);
            if (result == null)
                return null;
            return new OrganizationDto
            {
                Id = result.Id,
                Name = result.Name,
            };
        }

        public async Task AddAsync(Organization organization)
        {
            // Добавляем организацию в контекст и сохраняем в БД
            await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrganizationDto dto)
        {
            var existing = await _context.Organizations.FindAsync(dto.Id) ?? throw new KeyNotFoundException($"Organization with id {dto.Id} not found");

            // Обновляем только нужные поля (чтобы не перезаписать другие данные)
            _context.Entry(existing).CurrentValues.SetValues(dto);
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