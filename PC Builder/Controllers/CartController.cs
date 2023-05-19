using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        public IActionResult AddCart(string name, decimal price, string category)
        {
            bool counterUpdated = false;
            List<ProductInCart> productsInCart = db.ProductsInCart.ToList();

            ProductInCart product = new ProductInCart();

            if (productsInCart.Count != 0)
            {
                foreach (var item in productsInCart)
                {
                    if (item.Name == name && item.UserLogin == HttpContext.User.Identity.Name)
                    {
                        item.ProductCounter++;
                        item.Subtotal = item.Price * item.ProductCounter;

                        db.ProductsInCart.Update(item);

                        counterUpdated = true;
                        break;
                    }
                }
            }

            if (productsInCart.Count == 0 || counterUpdated == false)
            {
                product.ProductCounter = 1;
                product.Name = name;
                product.Price = price;
                product.Subtotal = price;
                product.Category = category;
                product.UserLogin = HttpContext.User.Identity.Name;

                db.ProductsInCart.Add(product);
            }

            db.SaveChanges();

            return RedirectToAction("ShowCart", "Cart");
        }

        //[HttpGet]
        [Authorize]
        public IActionResult ShowCart()
        {
            decimal? total = 0;
            IOrderedEnumerable<ProductInCart>? result;

            List<ProductInCart> products = db.ProductsInCart.Where(u => u.UserLogin == HttpContext.User.Identity.Name).ToList();

            string? sortOrder = Request.Cookies["sortOrder"];

            switch (sortOrder)
            {
                case "NameSortAscending":
                    {
                        result = products.OrderBy(n => n.Name);
                    }
                    break;
                case "NameSortDescending":
                    {
                        result = products.OrderByDescending(n => n.Name);
                    }
                    break;
                case "PriceSortAscending":
                    {
                        result = products.OrderBy(p => p.Price);
                    }
                    break;
                case "PriceSortDescending":
                    {
                        result = products.OrderByDescending(p => p.Price);
                    }
                    break;
                case "CategorySortAscending":
                    {
                        result = products.OrderBy(c => c.Category);
                    }
                    break;
                case "CategorySortDescending":
                    {
                        result = products.OrderByDescending(c => c.Category);
                    }
                    break;
                case "ProductCounterSortAscending":
                    {
                        result = products.OrderBy(p => p.ProductCounter);
                    }
                    break;
                case "ProductCounterSortDescending":
                    {
                        result = products.OrderByDescending(p => p.ProductCounter);
                    }
                    break;
                case "SubtotalSortAscending":
                    {
                        result = products.OrderBy(s => s.Subtotal);
                    }
                    break;
                case "SubtotalSortDescending":
                    {
                        result = products.OrderByDescending(s => s.Subtotal);
                    }
                    break;
                default:
                    {
                        result = products.OrderBy(n => n.Name);
                    }
                    break;
            }

            foreach (var item in products)
            {
                total += item.Subtotal;
            }

            CartViewModel cartViewModel = new CartViewModel()
            {
                Products = result,
                Total = total
            };

            return View(cartViewModel);
        }

        [HttpPost]
        public IActionResult RemoveCart(Guid? ProductId)
        {
            if (ProductId != null)
            {
                ProductInCart? product = db.ProductsInCart.FirstOrDefault(p => p.ProductId == ProductId);

                if (product != null)
                {
                    db.ProductsInCart.Remove(product);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("ShowCart");
        }

        [HttpPost]
        public IActionResult PlusItem(Guid ProductId)
        {
            ProductInCart? product = db.ProductsInCart.FirstOrDefault(p => p.ProductId == ProductId);

            if (product != null)
            {
                product.ProductCounter++;
                product.Subtotal = product.ProductCounter * product.Price;
                db.ProductsInCart.Update(product);
                db.SaveChanges();
            }

            return RedirectToAction("ShowCart");
        }

        [HttpPost]
        public IActionResult MinusItem(Guid ProductId)
        {
            ProductInCart? product = db.ProductsInCart.FirstOrDefault(p => p.ProductId == ProductId);

            if (product != null)
            {
                product.ProductCounter--;
                product.Subtotal = product.ProductCounter * product.Price;
                db.ProductsInCart.Update(product);

                if (product.ProductCounter == 0)
                {
                    db.ProductsInCart.Remove(product);
                }

                db.SaveChanges();
            }

            return RedirectToAction("ShowCart");
        }
    }
}