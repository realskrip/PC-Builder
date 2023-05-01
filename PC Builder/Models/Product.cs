using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PC_Builder.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        public int ProductCounter { get; set; }
        public string? Name { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal? Price { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal? Subtotal { get; set; }
        public string? Category { get; set; }
        public string? UserLogin { get; set; }
    }
}
