using System.ComponentModel.DataAnnotations;

namespace PC_Builder.Models
{
    public class CPU_Manufacturer
    {
        [Key]
        public int Id_Manufacturer { get; set; }
        public string? Manufacturer { get; set; }

        public CPU_Manufacturer()
        {
        }

        public CPU_Manufacturer(int Id, string manufacturer) 
        {
            Id_Manufacturer = Id;
            Manufacturer = manufacturer;
        }
    }
}
