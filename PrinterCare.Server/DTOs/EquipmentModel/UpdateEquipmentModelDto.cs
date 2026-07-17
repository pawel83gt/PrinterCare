using PrinterCare.Server.Entities;

namespace PrinterCare.Server.DTOs.EquipmentModel
{
    public class UpdateEquipmentModelDto
    {
        public string Name { get; set; } = string.Empty;
        public Guid ManufacturerId { get; set; }
    }
}
