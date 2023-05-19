using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace PC_Builder.Models
{
    public class DataStorage : Product
    {
        [Key]
        public Guid DataStorageId { get; set; }
    }
}
