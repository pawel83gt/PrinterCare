using PrinterCare.Server.Entities;

namespace PrinterCare.Server.DTOs.EquipmentModel
{
    public class EquipmentModelDto
    {
        public Guid Id { get; set; }
        public string Name {  get; set; } = string.Empty;
        public Guid ManufacturerId { get; set; }
    }
}
