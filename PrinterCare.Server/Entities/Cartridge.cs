namespace PrinterCare.Server.Entities
{
    public class Cartridge :BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        // ✅ Обратная навигация: один картридж может быть во многих устройствах
        public ICollection<Equipment> Equipments { get; set; } = new List<Equipment>();
    }
}
