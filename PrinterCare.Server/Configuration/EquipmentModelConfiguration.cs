using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrinterCare.Server.Entities;

namespace PrinterCare.Server.Configuration
{
    public class EquipmentModelConfiguration : IEntityTypeConfiguration<EquipmentModel>
    {
        public void Configure(EntityTypeBuilder<EquipmentModel> builder)
        {
            builder.ToTable("EquipmentModels");

            builder.HasKey(em => em.Id);

            builder.Property(em => em.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(em => em.CreatedAt)
                .IsRequired();

            // Связь многие к одному: Model → Manufacture
            builder.HasOne(em => em.Manufacturer)
                .WithMany(manuf => manuf.EquipmentModels)
                .HasForeignKey(em => em.ManufacturerId)
                .OnDelete(DeleteBehavior.NoAction);

            // Связь один ко многим: Model → Equipment
            builder.HasMany(em => em.Equipments)
                .WithOne(e => e.EquipmentModel)
                .HasForeignKey(e => e.EquipmentModelId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
