using PC_Builder.Models;

namespace PC_Builder.ViewModels
{
    public class BasketViewModel
    {
        public IEnumerable<Product> Products { get; set; } = null!;
    }
}
