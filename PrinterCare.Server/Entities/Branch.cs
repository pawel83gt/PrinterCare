using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrinterCare.Server.Entities
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public Guid OrganizationId { get; set; }

        public Organization Organization { get; set; } = null!;
        public ICollection<Equipment> Equipments { get; set; } = new List<Equipment>();
    }
}
