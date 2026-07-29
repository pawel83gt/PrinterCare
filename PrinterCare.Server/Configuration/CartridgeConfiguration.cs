using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrinterCare.Server.Entities;

namespace PrinterCare.Server.Configuration
{
    public class CartridgeConfiguration : IEntityTypeConfiguration<Cartridge>
    {
        public void Configure(EntityTypeBuilder<Cartridge>builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            //связь один ко многим с Equipment
            builder.HasMany(c => c.Equipments) //один картридж может быть связан с несколькими оборудованием
                .WithOne(e => e.Cartridge) //описывает Навигационное свойство к картриджу
                .HasForeignKey(e => e.CartridgeId); //указывает внешний ключ в таблице Equipment, который ссылается на картридж
        }
    }
}
