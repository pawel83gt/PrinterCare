namespace PrinterCare.Server.Entities
{
    public class Equipment :BaseEntity
    {
        public string Alias { get; set; } = string.Empty;
        public EquipmentType Type {  get; set; }
        //внешний ключ на филиал
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
        //внешний ключ на модель
        public Guid EquipmentModelId {  get; set; }
        //связь оборудование одной модели
        public EquipmentModel EquipmentModel { get; set; } = null!;
    }
}
