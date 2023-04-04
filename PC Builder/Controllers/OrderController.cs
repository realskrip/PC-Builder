using Microsoft.AspNetCore.Mvc;
using PC_Builder.Models;
using PC_Builder.ViewModels;
using System.Text.Json;

namespace PC_Builder.Controllers
{
    public class OrderController : Controller
    {
        ApplicationContext db;
        public OrderController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Order(Order order)
        {
            string productsToJSON;
            List<Product> products = db.Products.ToList();

            productsToJSON = JsonSerializer.Serialize(products);

            order.Products = productsToJSON;

            db.Orders.Add(order);
            
            foreach (var item in products)
            {
                db.Products.Remove(item);
            }

            db.SaveChanges();
            return View();
        }
    }
}