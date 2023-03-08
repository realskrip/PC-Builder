using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Index() 
        {
            var cpu_manufacturers = db.CPU_Manufacturers.ToList();
            var cpus = db.CPUs.ToList();
            var model = new IndexViewModel { CPU_Manufacturers = cpu_manufacturers, CPUs = cpus };
            return View(model);
        }
    }
}