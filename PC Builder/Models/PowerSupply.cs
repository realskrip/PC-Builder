using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace PC_Builder.Models
{
    public class PowerSupply : Product
    {
        [Key]
        public Guid PowerSupplyId { get; set; }
    }
}
