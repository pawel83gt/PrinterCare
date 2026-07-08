using System.ComponentModel.DataAnnotations;

namespace PrinterCare.Server.Entities
{
    public class Organization : BaseEntity
    {

        public string Name { get; set; } = string.Empty;

        public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    }
}
