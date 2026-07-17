using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrinterCare.Server.Entities;

namespace PrinterCare.Server.Configuration
{
    public class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.ToTable("Equipments");
            builder.HasKey(e => e.Id); //настройка первичного ключа
            builder.Property(e => e.Alias).IsRequired().HasMaxLength(50); //поле обязательно, длинна максимум 50 символов
            builder.Property(e => e.Type).IsRequired().HasConversion<int>(); //enum как int в БД

            builder.Property(e => e.CreatedAt)
                .IsRequired();

            //Связь одно оборудование один филиал
            builder.HasOne(e => e.Branch)// Оборудование имеет один филиал
                .WithMany(b => b.Equipments)// Филиал имеет много оборудования
                .HasForeignKey(e => e.BranchId);// Внешний ключ в Equipment

            //Связь многие к одному Оборудование => Модель
            builder
                .HasOne(e => e.EquipmentModel) //Каждый экземпляр Equipment ссылается на одну конкретную модель (Model).
                .WithMany(em => em.Equipments) //Одна модель может быть использована во множестве единиц оборудования.
                .HasForeignKey(e => e.EquipmentModelId) //В таблице Equipments будет столбец ModelId, который ссылается на первичный ключ таблицы Models.
                .OnDelete(DeleteBehavior.Cascade); //Если запись Model будет удалена, то все связанные с ней записи Equipment будут автоматически удалены каскадно.
        }
    }
}
