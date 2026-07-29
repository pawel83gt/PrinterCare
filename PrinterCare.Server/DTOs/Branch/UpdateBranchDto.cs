namespace PrinterCare.Server.DTOs.Branch
{
    public class UpdateBranchDto
    {
        public string Name { get; set; } = string.Empty;
        public Guid OrganizationId { get; set; }
    }
}
