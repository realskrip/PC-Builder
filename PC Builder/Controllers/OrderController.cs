using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PC_Builder.Models;
using PC_Builder.ViewModels;
using System.Text.Json;

namespace PC_Builder.Controllers
{
    public class OrderController : Controller
    {
        ApplicationContext db;
        public OrderController(ApplicationContext context)
        {
            db = context;
        }

        //[HttpGet]
        [Authorize]
        public IActionResult CheckContactDetails(decimal total)
        {
            if (total == 0)
            {
                return View("ErrorEmtyCart");
            }
            else
            {
                ContactDetails? userContactDetails = db.contactDetails.FirstOrDefault(u => u.Login == HttpContext.User.Identity.Name);

                if (userContactDetails == null)
                {
                    return RedirectToAction("ShowFormEditContactDetails", "Account");
                }

                return RedirectToAction("Checkout", "Order");
            }
        }

        public IActionResult Checkout()
        {
            ContactDetails? userContactDetails = db.contactDetails.FirstOrDefault(u => u.Login == HttpContext.User.Identity.Name);

            ContactDetailsViewModel contactDetailsViewModel = new ContactDetailsViewModel();

            if (userContactDetails != null)
            {
                contactDetailsViewModel.Name = userContactDetails.Name;
                contactDetailsViewModel.Surname = userContactDetails.Surname;
                contactDetailsViewModel.PhoneNumber = userContactDetails.PhoneNumber;
                contactDetailsViewModel.Country = userContactDetails.Country;
                contactDetailsViewModel.Region = userContactDetails.Region;
                contactDetailsViewModel.City = userContactDetails.City;
                contactDetailsViewModel.Address = userContactDetails.Address;
                contactDetailsViewModel.Postcode = userContactDetails.Postcode;
            }

            return View(contactDetailsViewModel);
        }

        //[HttpPost]
        public IActionResult Order()
        {
            string productsToJSON;
            List<Product> products = db.Products.Where(u => u.UserLogin == HttpContext.User.Identity.Name).ToList();
            User? userProfile = db.Users.FirstOrDefault(u => u.Login == HttpContext.User.Identity.Name);
            ContactDetails? userContactDetails = db.contactDetails.FirstOrDefault(u => u.Login == HttpContext.User.Identity.Name);
            Order order = new Order();

            productsToJSON = JsonSerializer.Serialize(products);

            order.Mail = userProfile.Mail;
            order.Login = userProfile.Login;
            order.Name = userContactDetails.Name;
            order.Surname = userContactDetails.Surname;
            order.PhoneNumber = userContactDetails.PhoneNumber;
            order.Country = userContactDetails.Country;
            order.Region = userContactDetails.Region;
            order.City = userContactDetails.City;
            order.Address = userContactDetails.Address;
            order.Postcode = userContactDetails.Postcode;
            order.Products = productsToJSON;

            db.Orders.Add(order);

            foreach (var item in products)
            {
                db.Products.Remove(item);
            }

            db.SaveChanges();
            return View();
        }
    }
}