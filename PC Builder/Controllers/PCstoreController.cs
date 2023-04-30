using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PC_Builder.Models;
using PC_Builder.ViewModels;

namespace PC_Builder.Controllers
{
    public class PCstoreController : Controller
    {
        ApplicationContext db;

        static string? categorySave;

        public PCstoreController(ApplicationContext context)
        {
            db = context;
        }

        //[HttpGet]
        [Authorize]
        public IActionResult CompletePC(string category)
        {
            string? sortOrder;
            PCstoreViewModel pcstoreViewModel = new PCstoreViewModel();

            if (category != null)
            {
                categorySave = category;
            }

            sortOrder = Request.Cookies["sortOrder"];

            switch (sortOrder)
            {
                case "NameSortAscending":
                    {
                        pcstoreViewModel.CPUs = db.CPUs.Where(c => c.Category == categorySave).OrderBy(n => n.Name);
                        pcstoreViewModel.Coolings = db.Coolings.Where(c => c.Category == categorySave).OrderBy(n => n.Name);
                        pcstoreViewModel.Motherboards = db.Motherboards.Where(c => c.Category == categorySave).OrderBy(n => n.Name);
                        pcstoreViewModel.RAMs = db.RAMs.Where(c => c.Category == categorySave).OrderBy(n => n.Name);
                        pcstoreViewModel.GPUs = db.GPUs.Where(c => c.Category == categorySave).OrderBy(n => n.Name);
                        pcstoreViewModel.DataStorages = db.DataStorages.Where(c => c.Category == categorySave).OrderBy(n => n.Name);
                        pcstoreViewModel.Cases = db.Cases.Where(c => c.Category == categorySave).OrderBy(n => n.Name);
                        pcstoreViewModel.PowerSupplies = db.PowerSupplies.Where(c => c.Category == categorySave).OrderBy(n => n.Name);
                    }
                    break;
                case "NameSortDescending":
                    {
                        pcstoreViewModel.CPUs = db.CPUs.Where(c => c.Category == categorySave).OrderByDescending(n => n.Name);
                        pcstoreViewModel.Coolings = db.Coolings.Where(c => c.Category == categorySave).OrderByDescending(n => n.Name);
                        pcstoreViewModel.Motherboards = db.Motherboards.Where(c => c.Category == categorySave).OrderByDescending(n => n.Name);
                        pcstoreViewModel.RAMs = db.RAMs.Where(c => c.Category == categorySave).OrderByDescending(n => n.Name);
                        pcstoreViewModel.GPUs = db.GPUs.Where(c => c.Category == categorySave).OrderByDescending(n => n.Name);
                        pcstoreViewModel.DataStorages = db.DataStorages.Where(c => c.Category == categorySave).OrderByDescending(n => n.Name);
                        pcstoreViewModel.Cases = db.Cases.Where(c => c.Category == categorySave).OrderByDescending(n => n.Name);
                        pcstoreViewModel.PowerSupplies = db.PowerSupplies.Where(c => c.Category == categorySave).OrderByDescending(n => n.Name);
                    }
                    break;
                case "PriceSortAscending":
                    {
                        pcstoreViewModel.CPUs = db.CPUs.Where(c => c.Category == categorySave).OrderBy(p => p.Price);
                        pcstoreViewModel.Coolings = db.Coolings.Where(c => c.Category == categorySave).OrderBy(p => p.Price);
                        pcstoreViewModel.Motherboards = db.Motherboards.Where(c => c.Category == categorySave).OrderBy(p => p.Price);
                        pcstoreViewModel.RAMs = db.RAMs.Where(c => c.Category == categorySave).OrderBy(p => p.Price);
                        pcstoreViewModel.GPUs = db.GPUs.Where(c => c.Category == categorySave).OrderBy(p => p.Price);
                        pcstoreViewModel.DataStorages = db.DataStorages.Where(c => c.Category == categorySave).OrderBy(p => p.Price);
                        pcstoreViewModel.Cases = db.Cases.Where(c => c.Category == categorySave).OrderBy(p => p.Price);
                        pcstoreViewModel.PowerSupplies = db.PowerSupplies.Where(c => c.Category == categorySave).OrderBy(p => p.Price);
                    }
                    break;
                case "PriceSortDescending":
                    {
                        pcstoreViewModel.CPUs = db.CPUs.Where(c => c.Category == categorySave).OrderByDescending(p => p.Price);
                        pcstoreViewModel.Coolings = db.Coolings.Where(c => c.Category == categorySave).OrderByDescending(p => p.Price);
                        pcstoreViewModel.Motherboards = db.Motherboards.Where(c => c.Category == categorySave).OrderByDescending(p => p.Price);
                        pcstoreViewModel.RAMs = db.RAMs.Where(c => c.Category == categorySave).OrderByDescending(p => p.Price);
                        pcstoreViewModel.GPUs = db.GPUs.Where(c => c.Category == categorySave).OrderByDescending(p => p.Price);
                        pcstoreViewModel.DataStorages = db.DataStorages.Where(c => c.Category == categorySave).OrderByDescending(p => p.Price);
                        pcstoreViewModel.Cases = db.Cases.Where(c => c.Category == categorySave).OrderByDescending(p => p.Price);
                        pcstoreViewModel.PowerSupplies = db.PowerSupplies.Where(c => c.Category == categorySave).OrderByDescending(p => p.Price);
                    }
                    break;
                default:
                    {
                        pcstoreViewModel.CPUs = db.CPUs.Where(c => c.Category == categorySave).OrderBy(n => n.Name);
                        pcstoreViewModel.Coolings = db.Coolings.Where(c => c.Category == categorySave).OrderBy(n => n.Name);
                        pcstoreViewModel.Motherboards = db.Motherboards.Where(c => c.Category == categorySave).OrderBy(n => n.Name);
                        pcstoreViewModel.RAMs = db.RAMs.Where(c => c.Category == categorySave).OrderBy(n => n.Name);
                        pcstoreViewModel.GPUs = db.GPUs.Where(c => c.Category == categorySave).OrderBy(n => n.Name);
                        pcstoreViewModel.DataStorages = db.DataStorages.Where(c => c.Category == categorySave).OrderBy(n => n.Name);
                        pcstoreViewModel.Cases = db.Cases.Where(c => c.Category == categorySave).OrderBy(n => n.Name);
                        pcstoreViewModel.PowerSupplies = db.PowerSupplies.Where(c => c.Category == categorySave).OrderBy(n => n.Name);
                    }
                    break;
            }
            
            return View(pcstoreViewModel);
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
