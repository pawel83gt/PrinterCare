namespace PrinterCare.Server.Entities
{
    public class EquipmentModel :BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        //внешний ключ Manufacture
        public Guid ManufacturerId { get; set; }

        //модель только одной марки 
        public Manufacturer Manufacturer { get; set; } = null!;

        //модель - множество оборудования на его основе
        public ICollection<Equipment> Equipments { get; set; } = new List<Equipment>();
    }
}
