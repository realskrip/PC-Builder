using Microsoft.AspNetCore.Mvc;
using PC_Builder.Models;
using PC_Builder.ViewModels;

namespace PC_Builder.Controllers
{
    public class CartController : Controller
    {
        ApplicationContext db;
        public CartController(ApplicationContext context)
        {
            db = context;
        }


        [HttpGet]
        public IActionResult ShowCart()
        {
            List<Product> products = db.Products.ToList();

            CartViewModel cartViewModel = new CartViewModel()
            {
                Products = products
            };

            return View(cartViewModel);
        }

        [HttpPost]
        public IActionResult RemoveCart(Guid? ProductId)
        {
            if (ProductId != null)
            {
                Product? product = db.Products.FirstOrDefault(p => p.ProductId == ProductId);

                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("ShowCart");
        }
    }
}
