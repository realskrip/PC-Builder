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
        public IActionResult Index(int CPU_Manufacturer_Id)
        {
            List<CPU_Manufacturer> cpu_manufacturers = db.CPU_Manufacturers.ToList();
            List<CPU> cpus = db.CPUs.ToList();

            IndexViewModel viewModel = new IndexViewModel { CPU_Manufacturers = cpu_manufacturers, CPUs = cpus };

            if (CPU_Manufacturer_Id != 0)
            {
                viewModel.CPUs = db.CPUs.Where(c => c.Id_Manufacturer == CPU_Manufacturer_Id);
            }

            return View(viewModel);
        }
    }
}