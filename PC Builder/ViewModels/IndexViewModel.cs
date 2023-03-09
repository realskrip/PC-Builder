using PC_Builder.Models;

namespace PC_Builder.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<CPU_Manufacturer> CPU_Manufacturers { get; set; } = null!;
        public IEnumerable<CPU> CPUs { get; set; } = null!;
        public IEnumerable<CoolingType> CoolingTypes { get; set; } = null!;
        public IEnumerable<Cooling> Coolings { get; set; } = null!;
    }
}
