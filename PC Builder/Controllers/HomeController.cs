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
        public IActionResult Index(string category)
        {
            List<CPU> cpus = db.CPUs.ToList();
            List<Cooling> coolings = db.Coolings.ToList();
            List<Motherboard> motherboards = db.Motherboards.ToList();
            List<RAM> rams = db.RAMs.ToList();


            IndexViewModel viewModel = new IndexViewModel() 
            { 
                CPUs = cpus,
                Coolings = coolings,
                Motherboards = motherboards,
                RAMs = rams

            };

            if (category == "cpu")
            {
                viewModel.Coolings = db.Coolings.Where(c => c.Category == "out of category");
                viewModel.Motherboards = db.Motherboards.Where(m => m.Category == "out of category");
                viewModel.RAMs = db.RAMs.Where(r => r.Category == "out of category");
                return View(viewModel);
            }
            else if (category == "cooling")
            {
                viewModel.CPUs = db.CPUs.Where(c => c.Category == "out of category");
                viewModel.Motherboards = db.Motherboards.Where(m => m.Category == "out of category");
                viewModel.RAMs = db.RAMs.Where(r => r.Category == "out of category");
                return View(viewModel);
            }
            else if (category == "motherboard")
            {
                viewModel.CPUs = db.CPUs.Where(c => c.Category == "out of category");
                viewModel.Coolings = db.Coolings.Where(c => c.Category == "out of category");
                viewModel.RAMs = db.RAMs.Where(r => r.Category == "out of category");
                return View(viewModel);
            }
            else if (category == "ram")
            {
                viewModel.CPUs = db.CPUs.Where(c => c.Category == "out of category");
                viewModel.Coolings = db.Coolings.Where(c => c.Category == "out of category");
                viewModel.Motherboards = db.Motherboards.Where(m => m.Category == "out of category");
                return View(viewModel);
            }

            return View(viewModel);
        }
    }
}