using System.ComponentModel.DataAnnotations;

namespace PC_Builder.Models
{
    public class CoolingType
    {
        [Key]
        public int Id_CoolingType { get; set; }
        public string? Name_CoolingType { get; set; }

        public CoolingType() 
        { 
        }

        public CoolingType(int Id, string Name)
        {
            Id_CoolingType = Id;
            Name_CoolingType = Name;
        }
    }
}
