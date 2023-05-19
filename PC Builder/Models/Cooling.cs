using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace PC_Builder.Models
{
    public class Cooling : Product
    {
        [Key]
        public Guid CoolingId { get; set; }
    }
}
