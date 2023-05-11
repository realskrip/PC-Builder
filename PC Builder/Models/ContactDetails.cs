using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace PC_Builder.Models
{
    public class ContactDetails
    {
        [Key]
        public Guid ContactDetailsId { get; set; }
        public string? Login { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Address { get; set; }
        public string? Postcode { get; set; }
    }
}
