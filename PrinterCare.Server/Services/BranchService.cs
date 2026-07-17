using PrinterCare.Server.DTOs.Branch;
using PrinterCare.Server.DTOs.Organization;
using PrinterCare.Server.Entities;
using PrinterCare.Server.Interfaces;

namespace PrinterCare.Server.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _repository;

        public BranchService(IBranchRepository repository) => _repository = repository;

        public async Task<BranchDto?> CreateBranchAsync(CreateBranchDto dto)
        {
            Console.WriteLine($"OrganizationId в сервисе: {dto.OrganizationId}");
            // Логика проверки (существует ли уже организация с таким названием)
            if (await _repository.ExistsAsync(dto.Name))
                return null;
            var branch = new Branch
            {
                Name = dto.Name,
                OrganizationId = dto.OrganizationId,
            };

            await _repository.AddAsync(branch);

            return new BranchDto
            {
                Id = branch.Id,
                Name = branch.Name,
                OrganizationId = branch.OrganizationId,
            };
        }

        public async Task<IEnumerable<BranchDto>> GetAllBranchAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<BranchDto?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<UpdateBranchDto> UpdateBranchAsync(UpdateBranchDto dto)
        {
            // 1. Бизнес-правило: нельзя обновить несуществующую организацию
            BranchDto existing = await _repository.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException($"Branch with id {dto.Id} not found");

            // 2. Проверяем, изменилось ли имя
            bool nameChanged = existing.Name != dto.Name;

            // 3. Если имя изменилось — проверяем, не занято ли оно другим филиалом
            if (nameChanged)
            {
                if (await _repository.ExistsAsync(dto.Name))
                {
                    throw new InvalidOperationException("Branch with this name already exists");
                }
            }

            // 4. Проверяем, изменилась ли организация
            bool organizationChanged = existing.OrganizationId != dto.OrganizationId;

            // 5. Если ничего не изменилось — возвращаем существующий объект
            if (!nameChanged && !organizationChanged)
            {
                return dto; // ✅ Ничего не обновляем
            }

            var updateBranch = new UpdateBranchDto { Id = dto.Id, Name = dto.Name, OrganizationId = dto.OrganizationId };

            // 6. Вызов репозитория
            await _repository.UpdateAsync(updateBranch);
            return updateBranch;
        }

        public async Task DeleteBranchAsync(Guid id)
        {
            // 1. Бизнес-правило: нельзя удалить несуществующую организацию
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Organization with id {id} not found");

            await _repository.DeleteAsync(existing.Id);
        }
    }
}
