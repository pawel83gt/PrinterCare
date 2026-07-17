namespace PrinterCare.Server.DTOs.Branch
{
    public class CreateBranchDto
    {
        public string Name { get; set; } = string.Empty;
        public Guid OrganizationId { get; set; }
    }
}
