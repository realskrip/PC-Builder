using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PC_Builder.Models;
using PC_Builder.ViewModels;

namespace PC_Builder.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }
    }
}