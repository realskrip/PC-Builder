using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PC_Builder.Models;
using PC_Builder.ViewModels;
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
        public async Task<RedirectToActionResult> Exit()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult AuthenticationError()
        {
            ModelState.AddModelError("Login", "Некорректный логин или пароль");
            return View("Login");
        }


        public IActionResult ShowRegistrationForm()
        {
            return View("RegistrationForm");
        }

        public IActionResult Registration(User user)
        {
            if (user.Login == null || user.Password == null || user.Mail == null)
            {
                ModelState.AddModelError("Login", "Некорректные данные");
                return View("RegistrationForm");
            }

            string login = user.Login;
            string mail = user.Mail;

            User? existingUser = db.Users.FirstOrDefault(u => u.Login == login || u.Mail == mail);

            if (existingUser == null)
            {
                if (user.Login.Length < 4 || user.Password.Length < 6)
                {
                    if (user.Login.Length < 4)
                    {
                        ModelState.AddModelError("Login", "Логин должен содержать минимум 4 символа");
                    }
                    if (user.Password.Length < 6)
                    {
                        ModelState.AddModelError("Password", "Пароль должен содержать минимум 6 символов");
                    }
                    return View("RegistrationForm");
                }

                db.Users.Add(user);
                db.SaveChanges();
                return View("Login");
            }
            else
            {
                ModelState.AddModelError("Login", "Пользователь с таким логином или Email уже существует");
                return View("RegistrationForm");
            }
        }

        public async Task<RedirectToActionResult> UserAuthentication(User user)
        {
            if (user.Login == null || user.Password == null)
            {
                return RedirectToAction("AuthenticationError", "Account");
            }

            string login = user.Login;
            string password = user.Password;

            User? userLog = db.Users.FirstOrDefault(u => (u.Login == login || u.Mail == login) && u.Password == password);

            if (userLog == null)
            {
                return RedirectToAction("AuthenticationError", "Account");
            }
            else
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, userLog.Login) };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            User? userLog = db.Users.FirstOrDefault(u => u.Login == HttpContext.User.Identity.Name);

            ProfileViewModel profileViewModel = new ProfileViewModel()
            {
                Email = userLog.Mail,
                Login = userLog.Login,
            };

            return View(profileViewModel);
        }

        public async Task<RedirectToActionResult> RemoveAccount()
        {
            User? userLog = db.Users.FirstOrDefault(u => u.Login == HttpContext.User.Identity.Name);

            List<Product> products = db.Products.Where(p => p.UserLogin == HttpContext.User.Identity.Name).ToList();

            foreach (var item in products)
            {
                db.Products.Remove(item);
            }
            
            db.Users.Remove(userLog);
            db.SaveChanges();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }

        public IActionResult ShowFormEditAccount(string email, string login)
        {
            if (email != null && login != null)
            {
                HttpContext.Response.Cookies.Append("emailCookie", email);
                HttpContext.Response.Cookies.Append("loginCookie", login);
            }

            return View("FormEditAccount");
        }

        public async Task<IActionResult> EditAccount(string? Email, string? Login, string? Password)
        {
            User? userLog = db.Users.FirstOrDefault(u => u.Login == HttpContext.User.Identity.Name);

            if (userLog != null)
            {
                if (userLog.Mail == Email && userLog.Login == Login && (userLog.Password == Password || Password == null))
                {
                    return RedirectToAction("Profile", "Account");
                }
                else
                {
                    if (Email == null || Login == null)
                    {
                        if (Email == null)
                        {
                            ModelState.AddModelError("Email", "Введите Email");
                        }

                        if (Login == null)
                        {
                            ModelState.AddModelError("Login", "Введите логин");
                        }

                        return View("FormEditAccount");
                    }

                    if (Login.Length < 4 || (Password != null && Password.Length < 6))
                    {
                        if (Login.Length < 4)
                        {
                            ModelState.AddModelError("Login", "Логин должен содержать минимум 4 символа");
                        }

                        if (Password != null && Password.Length < 6)
                        {
                            ModelState.AddModelError("Password", "Пароль должен содержать минимум  символов");
                        }

                        return View("FormEditAccount");
                    }

                    
                    User? existingEmail = db.Users.FirstOrDefault(u => u.Mail == Email && u.Login != userLog.Login);
                    User? existingLogin = db.Users.FirstOrDefault(u => u.Login == Login && u.Mail != userLog.Mail);


                    if (existingEmail != null || existingLogin != null)
                    {
                        if (existingEmail != null)
                        {
                            ModelState.AddModelError("Email", "Пользователь с таким Email уже существует");
                        }

                        if (existingLogin != null)
                        {
                            ModelState.AddModelError("Login", "Пользователь с таким логином уже существует");
                        }

                        return View("FormEditAccount");
                    }


                    if (Email != null)
                    {
                        userLog.Mail = Email;
                    }

                    if (Login != null)
                    {
                        List<Product> products = db.Products.Where(p => p.UserLogin == HttpContext.User.Identity.Name).ToList();

                        if (products.Count > 0)
                        {
                            foreach (var item in products)
                            {
                                item.UserLogin = Login;
                                db.Products.Update(item);
                            }
                        }
                        
                        userLog.Login = Login;
                    }

                    if (Password != null)
                    {
                        userLog.Password = Password;
                    }

                    db.Users.Update(userLog);
                    db.SaveChanges();
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("Login", "Account");
                }
            }
            return RedirectToAction("Profile", "Account");
        }

        public IActionResult ShowContactDetails()
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

        public IActionResult ShowFormEditContactDetails(string? name, string? surname, string? phoneNumber, 
            string? country, string? region, string? city, string? address, string? postcode)
        {
            if (name != null && surname != null && phoneNumber != null && country != null 
                && region != null && city != null && address != null && postcode != null)
            {
                HttpContext.Response.Cookies.Append("nameCookie", name);
                HttpContext.Response.Cookies.Append("surnameCookie", surname);
                HttpContext.Response.Cookies.Append("phoneNumberCookie", phoneNumber);
                HttpContext.Response.Cookies.Append("countryCookie", country);
                HttpContext.Response.Cookies.Append("regionCookie", region);
                HttpContext.Response.Cookies.Append("cityCookie", city);
                HttpContext.Response.Cookies.Append("addressCookie", address);
                HttpContext.Response.Cookies.Append("postcodeCookie", postcode);
            }

            return View();
        }

        public IActionResult EditContactDetails(string? Name, string? Surname, string? PhoneNumber,
            string? Country, string? Region, string? City, string? Address, string? Postcode)
        {
            ContactDetails? userContactDetails = new ContactDetails() 
            {
                Login = HttpContext.User.Identity.Name,
                Name = Name,
                Surname = Surname,
                PhoneNumber = PhoneNumber,
                Country = Country,
                Region = Region,
                City = City,
                Address = Address,
                Postcode = Postcode
            };

            ContactDetails? userExists = db.contactDetails.FirstOrDefault(u => u.Login == HttpContext.User.Identity.Name);

            if (userExists == null)
            {
                db.contactDetails.Add(userContactDetails);
            }
            else
            {
                userExists.Login = userContactDetails.Login;
                userExists.Name = userContactDetails.Name;
                userExists.Surname = userContactDetails.Surname;
                userExists.PhoneNumber = userContactDetails.PhoneNumber;
                userExists.Country = userContactDetails.Country;
                userExists.Region = userContactDetails.Region;
                userExists.City = userContactDetails.City;
                userExists.Address = userContactDetails.Address;
                userExists.Postcode = userContactDetails.Postcode;

                db.contactDetails.Update(userExists);
            }
            
            db.SaveChanges();

            return RedirectToAction("ShowContactDetails");
        }
    }
}