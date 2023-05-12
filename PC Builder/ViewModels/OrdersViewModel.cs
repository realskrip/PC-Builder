using PC_Builder.Models;

namespace PC_Builder.ViewModels
{
    public class OrdersViewModel
    {
        public IEnumerable<Order> Orders { get; set; } = null!;
    }
}
