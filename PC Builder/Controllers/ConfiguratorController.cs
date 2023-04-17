using Microsoft.AspNetCore.Mvc;
using PC_Builder.Models;
using PC_Builder.ViewModels;

namespace PC_Builder.Controllers
{
    public class ConfiguratorController : Controller
    {
        ApplicationContext db;

        public ConfiguratorController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult CompletePC(string category)
        {
            List<CPU> cpus = db.CPUs.ToList();
            List<Cooling> coolings = db.Coolings.ToList();
            List<Motherboard> motherboards = db.Motherboards.ToList();
            List<RAM> rams = db.RAMs.ToList();
            List<GPU> gpus = db.GPUs.ToList();
            List<DataStorage> dataStorages = db.DataStorages.ToList();
            List<Case> cases = db.Cases.ToList();
            List<PowerSupply> powerSupplies = db.PowerSupplies.ToList();

            ConfiguratorViewModel configuratorViewModel = new ConfiguratorViewModel()
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

            configuratorViewModel.CPUs = db.CPUs.Where(c => c.Category == category);
            configuratorViewModel.Coolings = db.Coolings.Where(c => c.Category == category);
            configuratorViewModel.Motherboards = db.Motherboards.Where(m => m.Category == category);
            configuratorViewModel.RAMs = db.RAMs.Where(r => r.Category == category);
            configuratorViewModel.GPUs = db.GPUs.Where(g => g.Category == category);
            configuratorViewModel.DataStorages = db.DataStorages.Where(d => d.Category == category);
            configuratorViewModel.Cases = db.Cases.Where(c => c.Category == category);
            configuratorViewModel.PowerSupplies = db.PowerSupplies.Where(p => p.Category == category);
            
            return View(configuratorViewModel);
        }


        [HttpPost]
        public IActionResult AddCart(string name, decimal price, string category)
        {
            bool match = false;
            List<Product> productsInCart = db.Products.ToList();

            Product product = new Product();
            
            if (productsInCart.Count != 0)
            {
                foreach (var item in productsInCart)
                {
                    if (item.Name == name)
                    {
                        item.ProductCounter++;
                        item.Subtotal = item.Price * item.ProductCounter;

                        db.Products.Update(item);

                        match = true;
                        break;
                    }
                }
            }
            
            if (productsInCart.Count == 0 || match == false)
            {
                product.ProductCounter = 1;
                product.Name = name;
                product.Price = price;
                product.Subtotal = price;
                product.Category = category;

                db.Products.Add(product);
            }

            db.SaveChanges();

            return RedirectToAction("ShowCart", "Cart");
        }
    }
}
