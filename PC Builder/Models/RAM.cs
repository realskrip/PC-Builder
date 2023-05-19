using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace PC_Builder.Models
{
    public class RAM : Product
    {
        [Key]
        public Guid RAMId { get; set; }
    }
}
