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
        public IActionResult Index(string category)
        {
            List<CPU> cpus = db.CPUs.ToList();
            List<Cooling> coolings = db.Coolings.ToList();


            IndexViewModel viewModel = new IndexViewModel() 
            { 
                CPUs = cpus,
                Coolings = coolings
            };

            if (category == "cpu")
            {
                viewModel.Coolings = db.Coolings.Where(c => c.Category == "out of category");
                return View(viewModel);
            }
            else if (category == "cooling")
            {
                viewModel.CPUs = db.CPUs.Where(c => c.Category == "out of category");
                return View(viewModel);
            }

            return View(viewModel);
        }
    }
}