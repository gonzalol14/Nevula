using Microsoft.AspNetCore.Mvc;

using NevulaForo.Models.DB;
using NevulaForo.Resources;
using NevulaForo.Services.Contract;
//using NevulaForo.Services.Implementation;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace NevulaForo.Controllers
{
    public class AccessController : Controller
    {
        private readonly IUserService _userService;

        public AccessController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Password = Utilities.EncryptPassword(model.Password);

            User user_created = await _userService.SaveUser(model);

            if(user_created.Id > 0)
                return RedirectToAction("Login", "Access");

            ViewData["Message"] = "Error al crear el usuario";
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            User user_found = null;
            
            if(email != null && password != password)
            {
                user_found = await _userService.GetUser(email, Utilities.EncryptPassword(password));
            }

            if(user_found == null)
            {
                ViewData["Message"] = "El correo y/o la contraseña es incorrecta. Compruebalo";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user_found.Username)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Access");
        }
    }
}
