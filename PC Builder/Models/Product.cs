using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PC_Builder.Models
{
    public class Product
    {
        public string? Name { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal? Price { get; set; }
        public string? Manufacturer { get; set; }
        public string? Category { get; set; }
    }
}
