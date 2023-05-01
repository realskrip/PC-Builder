using Microsoft.AspNetCore.Mvc;
using PC_Builder.Models;
using PC_Builder.ViewModels;

namespace PC_Builder.Components
{
    public class CartWidget : ViewComponent
    {
        ApplicationContext db;
        public CartWidget(ApplicationContext context)
        {
            db = context;
        }

        public IViewComponentResult Invoke()
        {
            decimal? total = 0;
            int? productCounter = 0;

            List<Product> products = db.Products.Where(u => u.UserLogin == HttpContext.User.Identity.Name).ToList();

            foreach (var item in products)
            {
                total += item.Subtotal;
                productCounter += item.ProductCounter;
            }

            CartViewModel cartViewModel = new CartViewModel()
            {
                Total = total,
                ProductCounter = productCounter
            };

            return View(cartViewModel);
        }
    }
}
