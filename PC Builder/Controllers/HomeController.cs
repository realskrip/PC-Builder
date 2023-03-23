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
            List<GPU> gpus = db.GPUs.ToList();
            List<DataStorage> dataStorages = db.DataStorages.ToList();
            List<Case> cases = db.Cases.ToList();

            IndexViewModel viewModel = new IndexViewModel() 
            { 
                CPUs = cpus,
                Coolings = coolings,
                Motherboards = motherboards,
                RAMs = rams,
                GPUs = gpus,
                DataStorages = dataStorages,
                Cases = cases
            };

            if (category != null)
            {
                viewModel.CPUs = db.CPUs.Where(c => c.Category == category);
                viewModel.Coolings = db.Coolings.Where(c => c.Category == category);
                viewModel.Motherboards = db.Motherboards.Where(m => m.Category == category);
                viewModel.RAMs = db.RAMs.Where(r => r.Category == category);
                viewModel.GPUs = db.GPUs.Where(g => g.Category == category);
                viewModel.DataStorages = db.DataStorages.Where(g => g.Category == category);
                viewModel.Cases = db.Cases.Where(g => g.Category == category);
            }

            return View(viewModel);
        }
    }
}