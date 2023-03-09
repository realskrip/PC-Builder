using System.ComponentModel.DataAnnotations;

namespace PC_Builder.Models
{
    public class Cooling
    {
        [Key]
        public int Id_Cooling { get; set; }
        public int Id_CoolingType { get; set; }
        public string? CoolingName { get; set; }
    }
}
