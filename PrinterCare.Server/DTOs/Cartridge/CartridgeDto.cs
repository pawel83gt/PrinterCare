using PrinterCare.Server.Entities;

namespace PrinterCare.Server.DTOs.Cartridge
{
    public class CartridgeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
