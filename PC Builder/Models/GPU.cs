using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace PC_Builder.Models
{
    public class GPU : Product
    {
        [Key]
        public Guid GPUId { get; set; }
    }
}
