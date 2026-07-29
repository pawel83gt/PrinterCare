using PrinterCare.Server.DTOs.Branch;

namespace PrinterCare.Server.DTOs.Organization
{
    public class OrganizationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<BranchDto> Branches { get; set; } = [];
    }
}
