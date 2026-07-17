using PrinterCare.Server.Entities;

namespace PrinterCare.Server.DTOs.Equipment
{
    public class UpdateEquipmentDto
    {
        public string Alias {  get; set; } = string.Empty;
        public int Type { get; set; }
        public Guid BranchId { get; set; }
        public Guid EquipmentModelId {  get; set; }
    }
}
