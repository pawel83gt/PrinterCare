using System.ComponentModel.DataAnnotations;

namespace PrinterCare.Server.Entities
{
    public class Organization
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    }
}
