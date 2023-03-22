using PC_Builder.Models;

namespace PC_Builder.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<CPU> CPUs { get; set; } = null!;
        public IEnumerable<Cooling> Coolings { get; set; } = null!;
        public IEnumerable<Motherboard> Motherboards { get; set; } = null!;
        public IEnumerable<RAM> RAMs { get; set; } = null!;
    }
}
