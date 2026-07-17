using PrinterCare.Server.Entities;

namespace PrinterCare.Server.DTOs.Equipment
{
    public class UpdateEquipmentDto
    {
        public string Alias {  get; set; } = string.Empty;
        public Guid BranchId { get; set; }
    }
}
