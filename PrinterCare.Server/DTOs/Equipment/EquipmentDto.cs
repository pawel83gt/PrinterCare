using PrinterCare.Server.Entities;

namespace PrinterCare.Server.DTOs.Equipment
{
    public class EquipmentDto
    {
        public Guid Id { get; set; }
        public string Alias { get; set; } = string.Empty;
        public int Type { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public Guid EquipmentModelId { get; set; }
        public string EquipmentModelName { get; set; } = string.Empty;
    }
}
