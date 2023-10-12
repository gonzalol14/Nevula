using Microsoft.AspNetCore.Mvc;

using NevulaForo.Models.DB;
using NevulaForo.Resources;
using NevulaForo.Services.Contract;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using NevulaForo.Models.ViewModels;

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
        public async Task<JsonResult> RegisterApi([FromBody] User model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(x => x.Value.Errors.Any())
                               .ToDictionary(
                                   kvp => kvp.Key,
                                   kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                               );

                return Json(new { success = false, errors = errors });
            }

            model.Password = Utilities.EncryptPassword(model.Password);

            User user_created = await _userService.SaveUserAndRole(model, 1);

            if (user_created.Id > 0) return Json(new { success = true, redirectUrl = "/Access/Login" });
            else
            {
                return Json(new { success = false, errorGeneral = "Error al intentar crear usuario" });
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
        public async Task<JsonResult> LoginApi([FromBody] LoginVM viewmodel)
        {
            try
            {
                if (viewmodel.Email == null || viewmodel.Password == null)
                {
                    throw new InvalidOperationException("Debe ingresar un email y una contraseña.");
                }

                User user_found = await _userService.GetUser(viewmodel.Email, Utilities.EncryptPassword(viewmodel.Password));

                if(user_found != null)
                {
                    if(user_found.DeletedAt != null)
                    {
                        //Si el DeletedAt no es null, significa que la cuenta fue desactivada hace menos de 30 (se verifica en el GetUser) y la esta queriendo reactivar
                        user_found.DeletedAt = null;
                        await _userService.UpdateUser(user_found);
                    }

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

                    if (viewmodel.Remember == true)
                    {
                        properties.IsPersistent = true;
                        properties.ExpiresUtc = DateTime.Now.AddDays(20);   
                    } 

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        properties);

                    return Json(new { success = true, redirectUrl = "/Home/Index" });

                } else
                {
                    throw new InvalidOperationException("El correo y/o la contraseña es incorrecta. Compruebalo");
                }

            } catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message });
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Access");
        }
    }
}
