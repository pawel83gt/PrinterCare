using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrinterCare.Server.Entities;

namespace PrinterCare.Server.Configuration
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.ToTable("Manufacturers");//Указываем имя таблицы в БД — Manufacturers

            builder.HasKey(m => m.Id); //Первичный ключ — Id (наследуется из BaseEntity)

            builder.Property(m => m.Name) 
                .IsRequired()
                .HasMaxLength(100); //Поле Name — обязательно, максимум 100 символов

            builder.Property(m => m.CreatedAt)
                .IsRequired();

            // Связь один ко многим: Manufacturer → Model
            builder.HasMany(m => m.EquipmentModels) //У одного производителя(Manufacture) может быть много моделей(Model)
                .WithOne(model => model.Manufacturer) //модель может быть одной марки производителя
                .HasForeignKey(model => model.ManufacturerId) //в таблице Model есть поле ManufactureId внешний ключ на производителя 
                .OnDelete(DeleteBehavior.NoAction); // При удалении марки удаляются все его модели
        }
    }
}
