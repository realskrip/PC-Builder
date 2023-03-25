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
            List<PowerSupply> powerSupplies = db.PowerSupplies.ToList();

            IndexViewModel indexViewModel = new IndexViewModel() 
            { 
                CPUs = cpus,
                Coolings = coolings,
                Motherboards = motherboards,
                RAMs = rams,
                GPUs = gpus,
                DataStorages = dataStorages,
                Cases = cases,
                PowerSupplies = powerSupplies,
            };

            indexViewModel.CPUs = db.CPUs.Where(c => c.Category == category);
            indexViewModel.Coolings = db.Coolings.Where(c => c.Category == category);
            indexViewModel.Motherboards = db.Motherboards.Where(m => m.Category == category);
            indexViewModel.RAMs = db.RAMs.Where(r => r.Category == category);
            indexViewModel.GPUs = db.GPUs.Where(g => g.Category == category);
            indexViewModel.DataStorages = db.DataStorages.Where(d => d.Category == category);
            indexViewModel.Cases = db.Cases.Where(c => c.Category == category);
            indexViewModel.PowerSupplies = db.PowerSupplies.Where(p => p.Category == category);

            return View(indexViewModel);
        }


        [HttpPost]
        public IActionResult AddBasket(string name, string category, decimal price)
        {
            Product product = new Product()
            {
                Name = name,
                Category = category,
                Price = price
            };

            db.Products.Add(product);
            db.SaveChanges();

            return RedirectToAction("Basket", "Basket");
        }
    }
}