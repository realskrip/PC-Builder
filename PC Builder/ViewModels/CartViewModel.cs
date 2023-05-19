using PC_Builder.Models;

namespace PC_Builder.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<ProductInCart> Products { get; set; } = null!;
        public decimal? Total { get; set; }
        public int? ProductCounter { get; set; }
    }
}
