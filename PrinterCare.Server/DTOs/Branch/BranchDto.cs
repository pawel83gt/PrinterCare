namespace PrinterCare.Server.DTOs.Branch
{
    public class BranchDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
    }
}
