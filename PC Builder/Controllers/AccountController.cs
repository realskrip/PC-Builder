using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PC_Builder.Models;
using System.Security.Claims;

namespace PC_Builder.Controllers
{
    public class AccountController : Controller
    {
        ApplicationContext db;
        public AccountController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpGet]
        public IActionResult AuthenticationError()
        {
            return View("AuthenticationError");
        }

        public async Task<RedirectToActionResult> UserAuthentication(User user)
        {
            if (user.Login == null || user.Password == null)
            {
                return RedirectToAction("AuthenticationError", "Account");
            }

            string login = user.Login;
            string password = user.Password;

            User? userLog = db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

            if (userLog == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, userLog.Login) };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            }

            return RedirectToAction("Index", "Home");
        }
    }
}