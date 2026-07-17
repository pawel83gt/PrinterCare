using PrinterCare.Server.Entities;

namespace PrinterCare.Server.DTOs.Manufacturer
{
    public class ManufacturerDto
    {
        public Guid Id { get; set; }
        public string Name {  get; set; } = string.Empty;
    }
}
