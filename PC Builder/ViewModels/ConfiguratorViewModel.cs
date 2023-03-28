using PC_Builder.Models;

namespace PC_Builder.ViewModels
{
    public class ConfiguratorViewModel
    {
        public IEnumerable<CPU> CPUs { get; set; } = null!;
        public IEnumerable<Cooling> Coolings { get; set; } = null!;
        public IEnumerable<Motherboard> Motherboards { get; set; } = null!;
        public IEnumerable<RAM> RAMs { get; set; } = null!;
        public IEnumerable<GPU> GPUs { get; set; } = null!;
        public IEnumerable<DataStorage> DataStorages { get; set; } = null!;
        public IEnumerable<Case> Cases { get; set; } = null!;
        public IEnumerable<PowerSupply> PowerSupplies { get; set; } = null!;
    }
}
