using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace PC_Builder.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
    }
}
