using System.ComponentModel.DataAnnotations;

namespace PC_Builder.Models
{
    public class CPU_Manufacturer
    {
        [Key] 
        public int Id_Manufacturer { get; set; }
        public string? Manufacturer { get; set; }
    }
}
