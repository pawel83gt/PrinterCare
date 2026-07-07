using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PrinterCare.Server.Entities
{
    public class OrganizationConfiguration
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.CreatedAt)
                .IsRequired();
        }
    }
}
