using Microsoft.AspNetCore.Mvc;

using NevulaForo.Models.DB;
using NevulaForo.Resources;
using NevulaForo.Services.Contract;
//using NevulaForo.Services.Implementation;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

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

            User user_created = await _userService.SaveUserAndRole(model, 1);

            if (user_created.Id > 0) return RedirectToAction("Login", "Access");
            else
            {
                ViewData["Message"] = "Error al intentar crear usuario";
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity != null)
            {
                if (claimUser.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, string remember)
        {

            try
            {
                if (email == null || password == null)
                {
                    throw new InvalidOperationException("Debe ingresar un email y una contraseña");
                }

                User user_found = await _userService.GetUser(email, Utilities.EncryptPassword(password));

                if(user_found != null)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim("Id", user_found.Id.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user_found.Username),
                        new Claim(ClaimTypes.Email, user_found.Email),
                        new Claim(ClaimTypes.Role, user_found.UserRoles.First().IdRole.ToString())
                
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true
                    };

                    if (remember != null)
                    {
                        properties.IsPersistent = true;
                        properties.ExpiresUtc = DateTime.Now.AddDays(20);   
                    } 

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        properties);

                    return RedirectToAction("Index", "Home");
                } else
                {
                    throw new InvalidOperationException("El correo y/o la contraseña es incorrecta. Compruebalo");
                }

            } catch (Exception ex)
            {
                //NullReferation
                ViewData["Message"] = ex.Message.ToString();
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Access");
        }
    }
}
