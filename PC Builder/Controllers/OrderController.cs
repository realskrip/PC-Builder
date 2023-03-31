using Microsoft.AspNetCore.Mvc;
using PC_Builder.Models;
using PC_Builder.ViewModels;

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
        public IActionResult ShowCheckout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            return View();
        }
    }
}