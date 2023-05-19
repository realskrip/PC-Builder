using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace PC_Builder.Models
{
    public class CPU : Product
    {
        [Key]
        public Guid CPUId { get; set; }
    }
}
