using System.ComponentModel.DataAnnotations;

namespace PC_Builder.Models
{
    public class CPU
    {
        [Key]
        public int Id_CPU { get; set; }
        public int Id_Manufacturer { get; set; }
        public string? CPU_name { get; set; }
    }
}
