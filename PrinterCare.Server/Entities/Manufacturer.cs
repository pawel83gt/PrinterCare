namespace PrinterCare.Server.Entities
{
    public class Manufacturer :BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        //марка - множество моделей
        public ICollection<EquipmentModel> EquipmentModels { get; set; } = new List<EquipmentModel>();
    }
}
