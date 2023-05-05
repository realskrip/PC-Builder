using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace PC_Builder.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public string? FullName { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set;}
        public string? ApartmentNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Products { get; set; }
    }
}
