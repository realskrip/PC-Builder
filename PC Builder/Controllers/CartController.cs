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
            bool match = false;
            List<Product> productsInCart = db.Products.ToList();

            Product product = new Product();

            if (productsInCart.Count != 0)
            {
                foreach (var item in productsInCart)
                {
                    if (item.Name == name)
                    {
                        item.ProductCounter++;
                        item.Subtotal = item.Price * item.ProductCounter;

                        db.Products.Update(item);

                        match = true;
                        break;
                    }
                }
            }

            if (productsInCart.Count == 0 || match == false)
            {
                product.ProductCounter = 1;
                product.Name = name;
                product.Price = price;
                product.Subtotal = price;
                product.Category = category;

                db.Products.Add(product);
            }

            db.SaveChanges();

            return RedirectToAction("ShowCart", "Cart");
        }

        //[HttpGet]
        [Authorize]
        public IActionResult ShowCart()
        {
            decimal? total = 0;
            IOrderedEnumerable<Product>? result;

            List<Product> products = db.Products.ToList();

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