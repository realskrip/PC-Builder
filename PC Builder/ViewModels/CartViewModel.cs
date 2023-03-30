using PC_Builder.Models;

namespace PC_Builder.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<Product> Products { get; set; } = null!;
    }
}
