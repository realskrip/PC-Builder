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
        public IActionResult Index(int CPU_Manufacturer_Id, int CoolingTypes_Id)
        {
            List<CPU_Manufacturer> cpu_manufacturers = db.CPU_Manufacturers.ToList();
            List<CPU> cpus = db.CPUs.ToList();
            List<CoolingType> coolingtypes = db.CoolingTypes.ToList();


            IndexViewModel viewModel = new IndexViewModel 
            { 
                CPU_Manufacturers = cpu_manufacturers, 
                CPUs = cpus,
                CoolingTypes = coolingtypes
            };
            
            cpu_manufacturers.Insert(0, new CPU_Manufacturer(0, "Не выбрано"));
            coolingtypes.Insert(0, new CoolingType(0, "Не выбрано"));

            if (CPU_Manufacturer_Id == 0)
            {
                viewModel.CPUs = db.CPUs.Where(c => c.Id_Manufacturer == 0);
            }
            else if (CPU_Manufacturer_Id != 0)
            {
                viewModel.CPUs = db.CPUs.Where(c => c.Id_Manufacturer == CPU_Manufacturer_Id);
            }

            if (CoolingTypes_Id == 0)
            {
                // условие сортировки
            }
            else if (CoolingTypes_Id != 0)
            {
                // условие сортировки
            }

            return View(viewModel);
        }
    }
}