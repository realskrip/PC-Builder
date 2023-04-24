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
            decimal? total = 0;

            List<Product> products = db.Products.ToList();

            foreach (var item in products)
            {
                total += item.Subtotal;
            }

            CartViewModel cartViewModel = new CartViewModel()
            {
                Products = products,
                Total = total
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

        [HttpPost]
        public IActionResult PlusItem(Guid ProductId)
        {
            Product? product = db.Products.FirstOrDefault(p => p.ProductId == ProductId);

            if (product != null)
            {
                product.ProductCounter++;
                product.Subtotal = product.ProductCounter * product.Price;
                db.Products.Update(product);
                db.SaveChanges();
            }

            return RedirectToAction("ShowCart");
        }

        [HttpPost]
        public IActionResult MinusItem(Guid ProductId)
        {
            Product? product = db.Products.FirstOrDefault(p => p.ProductId == ProductId);

            if (product != null)
            {
                product.ProductCounter--;
                product.Subtotal = product.ProductCounter * product.Price;
                db.Products.Update(product);

                if (product.ProductCounter == 0)
                {
                    db.Products.Remove(product);
                }

                db.SaveChanges();
            }

            return RedirectToAction("ShowCart");
        }
    }
}