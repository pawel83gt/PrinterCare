using Microsoft.EntityFrameworkCore;
using PrinterCare.Server.Data;       
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        // 2. Внедряем DbContext через конструктор
        private readonly ApplicationDbContext _context;

        public OrganizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // 3. Реализуем методы интерфейса

        public async Task<IEnumerable<Organization>> GetAllAsync()
        {
            // Получаем все организации из базы данных
            return await _context.Organizations.AsNoTracking().ToListAsync();
        }

        public async Task<Organization?> GetbyIdAsync(int id)
        {
            // Ищем организацию по Id, возвращаем null, если не найдена
            return await _context.Organizations
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddAsync(Organization organization)
        {
            // Добавляем организацию в контекст и сохраняем в БД
            await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Organization organization)
        {
            // Обновляем существующую организацию
            _context.Organizations.Update(organization);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Organization organization)
        {
            // Удаляем организацию из контекста и сохраняем в БД
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