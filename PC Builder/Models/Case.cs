using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace PC_Builder.Models
{
    public class Case : Product
    {
        [Key]
        public Guid CaseId { get; set; }
    }
}
