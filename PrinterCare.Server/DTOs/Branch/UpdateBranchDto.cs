namespace PrinterCare.Server.DTOs.Branch
{
    public class UpdateBranchDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid OrganizationId { get; set; }
    }
}
