using Microsoft.AspNetCore.Mvc;
using PC_Builder.Models;
using PC_Builder.ViewModels;

namespace PC_Builder.Controllers
{
    public class BasketController : Controller
    {
        ApplicationContext db;
        public BasketController(ApplicationContext context)
        {
            db = context;
        }


        [HttpGet]
        public IActionResult Basket()
        {
            List<Product> products = db.Products.ToList();

            BasketViewModel basketViewModel = new BasketViewModel()
            {
                Products = products
            };

            return View(basketViewModel);
        }
    }
}
