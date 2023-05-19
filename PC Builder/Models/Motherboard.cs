using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace PC_Builder.Models
{
    public class Motherboard : Product
    {
        [Key]
        public Guid MotherboardId { get; set; }
    }
}
